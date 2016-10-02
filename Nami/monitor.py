"""
监控系统运行状态
       * 系统信息： CPU、版本、详细
       * 进程管理： 进程概括信息（进程总数、进程数目）、单个进程信息、结束进程操作
       * 系统运行状态： CPU使用率、内存使用率（详细）、交换区使用率（详细）、系统负载

需要解析的文件：
/proc/cpuinfo: 获取cpu信息, 系统启动的时解析
/proc/version: 版本信息，系统启动的时解析
/proc/stat: cpu使用状态， 动态解析
/proc/[pid]/stat: 进程状态，动态解析
/proc/uptime：获取系统已经运行的时间，用来计算实时网速
/proc/meminfo
"""

import time
import threading
import os
import pymongo
import utils
import logging
import random
import copy

class Monitor:
    system_info = utils.get_system_info()
    cpu_usage = {"db":{"realtime":[], "onehour":[], "oneday":[], "oneweek":[], "onemonth":[]}, "cached":[]}#cached 按顺序缓存每个cpu的history数据
    memory_usage = {"db":{"realtime":[], "onehour":[], "oneday":[], "oneweek":[], "onemonth":[]}, "cached":[], "statistics":{}}
    processes = {}

    FLUSH_TIME = 5*60 #'s
    last_flush_time = time.time()

    # 当有新数据时，处理器
    process_handlers = set()
    realtime_handlers = set()


    # some configs 
    time_confs = dict(
        realtime=60*1000,
        onehour = 60*60*1000,
        oneday = 24*60*60*1000,
        oneweek= 7*24*60*60*1000,
        onemonth = 30*24*60*60*1000,
        )
    data_bucket = pymongo.Connection()["Nami"]["cpu_memory"]
    need_refresh_processes = False

    USAGE_TIMEOUT = 0.5
    PROCESSES_TIMEOUT = 5
    
    __started = False
    __cache_mutex = False
    @classmethod
    def start(cls, usage_timeout, processes_timeout):
        if cls.__started:
            logging.error("Monitor already started!")
        cls.USAGE_TIMEOUT = usage_timeout
        cls.PROCESSES_TIMEOUT = processes_timeout


        # 获取System Info
        print("getting system info...")
        cls.system_info = utils.get_system_info()
        
        # 获取数据库历史usage信息
        print("getting realtime usage...")
        cpu_rc = cls.data_bucket.find_one({"name":"cpu"})
        if cpu_rc:
            # 获取分片信息
            indexes = {}
            for period in cls.time_confs:
                indexes[period]=cls.get_stamp_index(cpu_rc["value"][0]["history"], cls.time_confs[period])
            for cpu_data in cpu_rc["value"]:
                for period in cls.cpu_usage["db"]:
                    cls.cpu_usage["db"][period].append({
                            "processor":cpu_data["processor"],
                            "history":cpu_data["history"][indexes[period]:]
                            })

        mem_rc = cls.data_bucket.find_one({"name":"memory"})
        if mem_rc:
            
            for period in cls.time_confs:
                slice_index = cls.get_stamp_index(mem_rc["value"], cls.time_confs[period])
                cls.memory_usage["db"][period] = mem_rc["value"][slice_index:]
            
        # 获取usage
        usage = utils.get_realtime_resources_usage()
        for cpu_rate in usage["cpu_rates"]:
            cls.cpu_usage["cached"].append([cpu_rate])
        cls.memory_usage["cached"] = [usage["mem_info"]["usage"]]

        # 获取processes
        print("getting processes data")
        cls.processes = utils.get_realtime_processes_status()

        # 启动线程
        t_usage = threading.Timer(usage_timeout, cls.t_refresh_usage)
        t_usage.start()
        t_processes = threading.Timer(processes_timeout, cls.t_refresh_processes)
        t_processes.start()

        print("monitor started!")
        cls.__started = True
    
    def kill_process(pid):
        if os.system("kill {0}".format(pid)) != 0:
            return False
        else:
            return True
    
    @classmethod
    def get_all_status(cls):
        tmp_resources = {}
        
        for period in cls.cpu_usage["db"]:
            tmp_resources[period] = {"cpu":[], "mem":[], "net":[]}

            tmp_resources[period]["cpu"]= copy.deepcopy(cls.cpu_usage["db"][period])
            for i in range(len(cls.cpu_usage["cached"])):
                tmp_resources[period]["cpu"][i]["history"] += cls.cpu_usage["cached"][i]
            mem_history = copy.deepcopy(cls.memory_usage["db"][period])
            mem_history += cls.memory_usage["cached"]

            tmp_resources[period]["mem"].append({"name":"memory", "history":mem_history})
            
        # todo, 添加截断时间
        return dict(
            systemInfo = cls.system_info,
            resources = tmp_resources,
            processes = cls.processes
            )

    @classmethod
    def add_realtime_handler(cls, handler):
        cls.realtime_handlers.add(handler)
    @classmethod
    def add_process_handler(cls, handler):
        cls.process_handlers.add(handler)

    @classmethod
    def set_process_refresh_flag(cls, flag):
        if type(flag) != bool:
            raise Exception("flag must be bool type")
        cls.need_refresh_processes = flag

    @classmethod
    def t_refresh_usage(cls):
        # 获取usage
        while True:
            # 获取数据
            usage = utils.get_realtime_resources_usage()
            cls._lock_cache()
            try:
                for i in range(len(usage["cpu_rates"])):
                    if len(cls.cpu_usage["cached"]) < i + 1:
                        cls.cpu_usage["cached"].append([usage["cpu_rates"][i]])
                    else:
                        cls.cpu_usage["cached"][i].append(usage["cpu_rates"][i])
            
                cls.memory_usage["cached"].append(usage["mem_info"]["usage"])
            except Exception as e:
                cls._unlock_cache()
                raise e
            else:
                cls._unlock_cache()
            # 发送实时数据
            for handler in cls.realtime_handlers:
                handler(usage)
            
            # flush
            if time.time() - cls.last_flush_time > cls.FLUSH_TIME:
                cls.update_cache()
                cls.last_flush_time = time.time()
            

            time.sleep(cls.USAGE_TIMEOUT)

    @classmethod
    def t_refresh_processes(cls):
        while True:
            if cls.need_refresh_processes:
                cls.processes = utils.get_realtime_processes_status()
                for handler in cls.process_handlers:
                    handler(cls.processes)
            time.sleep(cls.PROCESSES_TIMEOUT)
    @classmethod
    def update_cache(cls):
        logging.info("flushing data to db")
        cls._lock_cache()
        try:
            cached_data = cls.cpu_usage["cached"]
            # 刷新到数据库
            cpu_rc = cls.data_bucket.find_one({"name":"cpu"})
            if not cpu_rc:
                tmp_data = []
                for i in range(len(cached_data)):
                    tmp_data.append({"processor":i-1, "history":cached_data[i]})
                cls.data_bucket.insert({"name":"cpu", "value":tmp_data})
                del tmp_data
            else:
                for i in range(len(cached_data)):
                    cpu_rc["value"][i]["history"] += cached_data[i]
                cls.data_bucket.update({"name":"cpu"}, {"$set":{"value":cpu_rc["value"]}})

            mem_rc = cls.data_bucket.find_one({"name":"memory"})
            if not mem_rc:
                cls.data_bucket.insert({"name":"memory", "value":cls.memory_usage["cached"]})
            else:
                mem_rc["value"] += cls.memory_usage["cached"]
                cls.data_bucket.update({"name":"memory"}, {"$set":{"value":mem_rc["value"]}})

            # 更新usage db data
            for period in cls.cpu_usage["db"]:
                period_data = cls.cpu_usage["db"][period]
                for index in range(len(cached_data)):
                    period_data[index]["history"] += cached_data[index]
        
            for period in cls.memory_usage["db"]:
                cls.memory_usage["db"][period] += cls.memory_usage["cached"]

            # 清除cached
            cls.cpu_usage["cached"] = []
            cls.memory_usage["cached"] = []
        except Exception as e:
            cls._unlock_cache()
            raise e
        else:
            cls._unlock_cache()

    def get_stamp_index(origin_data, distance_time):
        """
        找到最近distance_time数据的下表
        """
        max_time = time.time()*1000
        length = len(origin_data)
        if length == 0:
            return 0

        index = int(length/2)
        start_index = 0
        end_index = length -1
        while (end_index != start_index ):
            if origin_data[index][0] > (max_time - distance_time): # 往前找
                end_index = index
                index = int((start_index + end_index)/2)
                if index == start_index: # 找到
                    break
            elif origin_data[index][0] < (max_time - distance_time): # 往后找
                start_index = index
                index = int((start_index + end_index + 1)/2)
                if index == end_index: # 找到
                    break
            else:
                break # 找到
        return index

    @classmethod
    def _lock_cache(cls):
        while cls.__cache_mutex:
            time.sleep(random.random(0.1))
        cls.__cache_mutex = True
        
    @classmethod
    def _unlock_cache(cls):
        cls.__cache_mutex = False
    
