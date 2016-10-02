import time
import os

last_check_data = {"cpu":[], "processes":[]} # 上次检查时的总cpu、和进程cpu时间（用来计算总体cpu和进程cpu利用率）

total_mem = 0
with open("/proc/meminfo") as f:
    for line in f:
        name = line.split(":")[0]
        value = line.split(":")[1]
        if name == "MemTotal":
            total_mem = int(value.split()[0])



JEFFIES_UNIT = 100

def get_system_info():
    """
    获取静态系统信息，包括cpu和系统版本信息
    """
    # 获取cpu信息
    cpu_name = ""
    cpu_core = ""
    with open("/proc/cpuinfo") as f:
        for line in f:
            if line.strip() and len(line.strip().split(":")) == 2:
                key = line.strip().split(":")[0].strip()
                value = line.strip().split(":")[1].strip()
                if key == "model name":
                    cpu_name = value
                elif key == "cpu cores":
                    cpu_core = value

    # 获取版本信息
    version = ""
    with open("/proc/version") as f:
        version = f.read()
    

    return dict(cpu=dict(name=cpu_name,core= cpu_core), version=version)

def get_realtime_resources_usage():
    """
    获取系统资源使用情况信息，包括cpu、内存、网络实时使用情况
    """

    # cpu rate
    cpu_rates = []
    with open("/proc/stat") as f:
        index = 0
        for line in f:
            head = line[0:4]
            if not head.startswith("cpu"):
                continue
            counters = line.split()[1:]
            total = 0
            for i in range(4):# user, nice, system, idle
                total += int(counters[i])
            idle = int(counters[3])
            
            if len(last_check_data["cpu"]) < index + 1:
                cpu_rate = 0
                last_check_data["cpu"].append({"total":total, "idle":idle})
            else:
                cpu_rate = float("%0.1f" % (100- (idle-last_check_data["cpu"][index]["idle"])*100/(total - last_check_data["cpu"][index]["total"])))
                last_check_data["cpu"][index]["total"] = total
                last_check_data["cpu"][index]["idle"] = idle

            cpu_rates.append([time.time()*1000, cpu_rate])
            index += 1
    # 内存和交换区存储信息， 并且计算内存使用率
    mem_info = {}
    with open("/proc/meminfo") as f:
        for line in f:
            name = line.split(":")[0]
            value = line.split(":")[1]
            if name == "MemTotal":
                total = int(value.split()[0])
            elif name == "MemFree":
                free = int(value.split()[0])
            elif name == "Buffers":
                buffers = int(value.split()[0])
            elif name == "Cached":
                cached = int(value.split()[0])
            elif name == "SwapTotal":
                swapTotal = int(value.split()[0])
            elif name == "SwapFree":
                swapFree = int(value.split()[0])
    used = total - free
    usage = float("%0.1f" % (100* used/ total))
    swapUsed = swapTotal - swapFree
    swapUsage = float("%0.1f" % (swapUsed / swapTotal))
    return dict(cpu_rates=cpu_rates,
                mem_info = dict(
            total=total,
            used=total,
            usage=[time.time()*1000, usage],
            swapTotal = swapTotal,
            swapUsed = swapUsed,
            swapUsage = swapUsage
            ))

def get_realtime_processes_status():
    """
    获取系统进程使用状态
    """


    # 获取系统时间，计算进程运行时间时需要使用
    with open("/proc/uptime") as f:
        uptime = float(f.readline().split()[0])
        
    # 获取进程信息， 并且统计进程总体：总进、正在运行、睡眠
    process_info_list = []
    running_process_num = 0
    for filename in os.listdir("/proc/"):
        file_full_name = "/proc/{0}".format(filename)
        if not filename.isdigit() or not os.path.isdir(file_full_name):
            continue
        cpu_time = 0
        with open(file_full_name+"/stat") as f:
            line = f.readlines()[0]
            values = line.split()
            process_pid = int(values[0])
            process_name = values[1][1:-1]
            if values[2] == "R":
                state = "运行中"
                running_process_num += 1
            elif values[2] in ("S", "D"):
                state = "睡眠中"
            elif values[2] in ("Z"):
                state = "僵死"
            elif values[2] in ("T"):
                state = "暂停"
            elif values[2] in ("W"):
                state = "换页"
            else:
                state = "o(╯□╰)o"
            process_state = state
            process_pr = values[17]
            process_runtime = uptime - int(values[21])/JEFFIES_UNIT
            # 计算进程cpu利用率
            utime = int(values[13])
            stime = int(values[14])
            cpu_time = utime + stime
            process_cpuRate = 0

            now_cpu_time = 0
            with open("/proc/stat") as f:
                line = f.readline()
                counters = line.split()[1:]
                for i in range(4):# user, nice, system, idle
                    now_cpu_time += int(counters[i])
            found_process = False
            for process in last_check_data["processes"]:
                if process["pid"] == process_pid:
                    process_cpuRate = float("%0.1f" % (100* (cpu_time - process["cpu_time"])/(now_cpu_time - process["check_cpu_time"])))
                    found_process = True
                    process["check_cpu_time"] = now_cpu_time
                    process["cpu_time"] = cpu_time
                    break
            # 更新进程检查cpu时间状态
            if not found_process:
                last_check_data["processes"].append({"pid":process_pid, "cpu_time":cpu_time, "check_cpu_time":now_cpu_time})

            # 进程内存信息
            with open(file_full_name+"/statm") as f:
                    values = f.readlines()[0].split()
                    process_memSize = int(values[0])*4
                    process_memResident = int(values[1])*4
                    process_memRate = float("%0.1f" % (100*process_memResident/total_mem))

            process_info_list.append(dict(
                    pid=process_pid,
                    name=process_name,
                    state=process_state,
                    pr=process_pr,
                    runtime=process_runtime,
                    cpuRate=process_cpuRate,
                    memSize = process_memSize,
                    memResident = process_memResident,
                    memRate = process_memRate
                    ))

    return dict(
        processInfo = process_info_list,
        total=len(process_info_list),
        running=running_process_num,
        sleeping=len(process_info_list) -running_process_num
        )
