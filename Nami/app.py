#!/usr/bin/env python3


import tornado.web
import tornado.ioloop
import tornado.websocket
from tornado.options import define, options

import os
import logging

from Base import NamiBaseHandler
from monitor import Monitor

define("port", default=2223, help="run on the given port", type=int)
define("realtime_interval", default=3, help="refresh realtime time(second)", type=int)
define("processes_interval", default=30, help="refresh processes time(second)", type=int)

class Application(tornado.web.Application):
    def __init__(self):
        handlers = [
            (r"/Nami/", HomeHandler),
            (r"/Nami/monitor", MonitorHandler),
            (r"/Nami/realtimesocket", RealtimeSocketHandler),
#            (r"/Nami/processsocket", ProcessSocketHandler),
            (r"/Nami/login", LoginHandler),
            (r"/Nami/logout", LogoutHandler)
            ]
        settings = dict(
            debug=True,
            login_url=r"/Nami/login",
            static_path=os.path.join(os.path.dirname(__file__),"static"),
            )
        tornado.web.Application.__init__(self, handlers, **settings)

class HomeHandler(NamiBaseHandler):
    def get(self):
        #self.render("templates/monitor.html")
        pass

class MonitorHandler(NamiBaseHandler):
    @tornado.web.authenticated
    def get(self):
        self.render("templates/monitor.html", data = Monitor.get_all_status())
    @tornado.web.authenticated
    @NamiBaseHandler.check_arguments("pid:int")
    def post(self):
        if Monitor.kill_process(self.args["pid"]):
            return self.send_success()
        else:
            return self.send_fail()

class RealtimeSocketHandler(tornado.websocket.WebSocketHandler):
    waiters = set()
    def allow_draft76(self):
        # for ios 5.0 Safari
        return True

    def open(self):
        RealtimeSocketHandler.waiters.add(self)
    def on_close(self):
        RealtimeSocketHandler.waiters.remove(self)

    @classmethod
    def handle_realtime_data(cls, data):
        """
        data: dict(cpu_rates=cpu_rates,
                mem_info = dict(
            total=total,
            used=total,
            usage=[time.time()*1000, usage],
            swapTotal = swapTotal,
            swapUsed = swapUsed,
            swapUsage = swapUsage
            )
        """
        
        print("realtime data: ", data)
        logging.info("sending realtime data to front end, there's {0} link now".format(len(cls.waiters)))
        for waiter in cls.waiters:
            try:
                waiter.write_message(data)
            except:
                logging.error("Error sending realtime data", exc_info=True)

    def on_message(self):
        # 这里没有用到
        pass

class ProcessSocketHandler(tornado.websocket.WebSocketHandler):
    waiters = set()
    def allow_draft76(self):
        # for ios 5.0 Safari
        return True

    def open(self):
        ProcessSocketHandler.waiters.add(self)
    def on_close(self):
        ProcessSocketHandler.waiters.remove(self)

    @classmethod
    def handle_process_data(cls, data):
        print("processes data: ", data)
        """
        logging.info("sending process data to front end, there's {0} link now".format(len(cls.waiters)))
        for waiter in cls.waiters:
            try:
                waiter.write_message(data)
            except:
                logging.error("Error sending process data", exc_info=True)
        
        """
    def on_message(self):
        # 这里没有用到
        pass

class LoginHandler(NamiBaseHandler):
    def get(self):
        self.render("templates/login.html")
    @NamiBaseHandler.check_arguments("username", "password")
    def post(self):
        if self.args["username"] == "monk" and self.args["password"] == "b7e5ca5010ae06fac525cec9888a62f90e7bcaf22ebdb5f6f3d8ed3e604d5ca8":
            self.set_current_user("monk")
            return self.send_success(next = "/Nami/")
        else:
            self.send_fail()

class LogoutHandler(NamiBaseHandler):
    @tornado.web.authenticated
    def get(self):
        self.unset_current_user()
        self.redirect("/Nami/")

def main():
    tornado.options.parse_command_line()
    print("starting the monitor, refresh realtime @ {0}'s, processes @ {1}'s...".format(
            options.realtime_interval, options.processes_interval))
    Monitor.start(options.realtime_interval, options.processes_interval)
    Monitor.add_realtime_handler(RealtimeSocketHandler.handle_realtime_data)
    Monitor.add_process_handler(ProcessSocketHandler.handle_process_data)

    print("start Nami @ port {0}".format(options.port))
    app = Application()
    app.listen(options.port)
    tornado.ioloop.IOLoop.instance().start()


if __name__ == "__main__":
    main()
