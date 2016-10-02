import tornado.web

class BaseHandler(tornado.web.RequestHandler):
    """
    Handler的通用基类，用以处理响应请求的通用操作和一些基本工具。
    三种响应：
       错误请求: send_error(status_code, **kargs)[tornado自带，直接响应响应的HTTP错误头，如果你需要自定义错误Page的话，重写write_error(status_code, **kargs)]
       请求失败：send_fail(fail_code, fail_text)[返回JSON数据格式: {"success":False, "code":fail_code, "text":fail_text}]
       请求成功: send_success(**kwargs)[返回JSON数据格式:{"success":True, **kwargs}]
    请求处理：
       check_arguments:获取请求里的参数数据，并存在self.args里
    """

    def check_arguments(*request_arguments):
        """
        @Decorator( request_arguments:string(...) )
        这个装饰器可以配合 Handler 进行参数的检查
        每一个参数是一个字符串，形如 name[:type][?]
        type是类型，可以为 int，str等，? 代表参数是否可选
        参数会从请求的url中解析，或从post的body中以json的方式寻找
        """
        def func_wrapper(method):
            def wrapper(self,*args,**kwargs):
                if self.request.method == "POST":
                    obj = self.__json_parse(self.request.body) or {}
                    for name in request_arguments:
                        if name.count(':'):
                            Type = name.split(":")[1]
                            name = name.split(":")[0]
                        else:
                            Type = "all"
                        if name.count('?') == 0 and Type.count("?") == 0:
                            if name not in obj:
                                try:
                                    obj[name] = self.get_argument(name)
                                except:
                                    return self.send_error(400)
                        name = name.replace("?",'',1)
                        if name in obj:
                            obj[name] = self.__check_type(obj[name],Type)
                            if obj[name] is False:
                                return self.send_error(400)
                else:
                    obj = {}
                    for name in request_arguments:
                        if name.count(':'):
                            Type = name.split(":")[1]
                            name = name.split(":")[0]
                        else:
                            Type = "all"
                        if name.count('?') > 0 or Type.count("?") > 0:
                            name = name.replace('?','',1)
                            try:obj[name] = self.get_argument(name)
                            except:pass
                        else:
                            try:obj[name] = self.get_argument(name)
                            except: return self.send_error(400)
                        if name in obj:
                            obj[name] = self.__check_type(obj[name],Type)
                            if obj[name] is False:
                                return self.send_error(400)
                self.args = obj
                return method(self,*args,**kwargs)
            return wrapper
        return func_wrapper

    def send_success(self,**kwargs):
        obj = {"success":True}
        for k in kwargs:
            obj[k] = kwargs[k]
        self.write(obj)
    def send_fail(self,error_code=None, error_text = None):
        if type(error_code) == int:
            res = {"success":False, "errorCode":error_code,"errorText":error_text}
        else:
            res = {"success":False, "errorText": error_text}
        self.set_header("Content-Type", 'utf-8')
        self.write(res)

    def __json_parse(self,string):
        try:
            data = tornado.escape.json_decode(string)
            return data
        except:
            return None
    def __check_type(self,value,Type):
        if not Type or Type == "all":
            return value
        if (Type == "str" or Type == "string") and type(value) != str:
            try:
                return str(value)
            except:
                return False
        if Type == "int" and type(value) != int:
            try:
                return int(value)
            except:
                return False
        if Type == "float" and type(value) != float:
            try:
                return float(value)
            except:
                return False
        if Type == "number" and type(value) != int and type(value) != float:
            try: return int(value)
            except: pass
            try: return float(value)
            except: pass
            return False
        if (Type == "list" or Type == "array") and type(value) != list:
            try:
                return list(value)
            except:
                return False
        if (Type == "dict") and type(value) != dict:
            try:
                return dict(value)
            except:
                return False
        return value


