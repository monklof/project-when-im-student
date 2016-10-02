from merryweb.webbase import BaseHandler
import merryweb.session as session

class NamiBaseHandler(BaseHandler):
    def prepare(self):
        session_id = self.get_cookie("NamiSessionId")
        if not session_id:
            # 创建新的session
            self.session = session.MongoSession(create=True, session_collection_name="NamiSessions",  init_data={"user":None})
            self.set_cookie("NamiSessionId", self.session.get_session_id())
        else:
            try:
                self.session = session.MongoSession(session_collection_name="NamiSessions",session_id=session_id)
            except session.SessionNotExits as e:
                self.clear_cookie("NamiSessionId")
                # 创建新的session
                self.session = session.MongoSession(create=True, session_collection_name="NamiSessions",  init_data={"user":None})
                self.set_cookie("NamiSessionId", self.session.get_session_id())
            except Exception as e:
                raise e

    def get_current_user(self):
        return self.session.get_session_value()["user"]
    def unset_current_user(self):
        self.clear_cookie("NamiSessionId")
        self.session = None
    def set_current_user(self, username):
        values = self.session.get_session_value()
        values["user"] = username
        self.session.set_session_value(values)
