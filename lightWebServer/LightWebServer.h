#ifndef LIGHTWEBSERVER_H
#define LIGHTWEBSERVER_H
#include "GlobalHead.h"

#include <QtWidgets/QMainWindow>
#include "ui_LightWebServer.h"

#include "Dialog_SetConf.h"
#include "WebMgr.h"

class LightWebServer : public QMainWindow
{
	Q_OBJECT

public:
	LightWebServer(QWidget *parent = 0);
	~LightWebServer();

public slots:
	void On_NewRequest_Done(string result);

private:
	Ui::LightWebServer_UI ui;
	WebMgr *sMgr;
	Dialog_SetConf *dialogConf;


private slots:
	void On_btn_Start_Clicked();
	void On_btn_Stop_Clicked();
	void On_btn_Set_Clicked();
	void On_btn_ViewLog_Clicked();

	void On_ConfigureChanged(QString ip, unsigned int port, QString path);
};

#endif // LIGHTWEBSERVER_H
