#include "LightWebServer.h"
#include <QMessageBox>

LightWebServer::LightWebServer(QWidget *parent)
	: QMainWindow(parent),
	sMgr(new WebMgr(this)),
	dialogConf(NULL)
{
	ui.setupUi(this);
	ui.statusBar->showMessage("Server Stopped");


	//connect to ui response
	connect(ui.btn_Start, SIGNAL(clicked()), this, SLOT(On_btn_Start_Clicked()));
	connect(ui.btn_Stop, SIGNAL(clicked()), this, SLOT(On_btn_Stop_Clicked()));
	connect(ui.btn_Set, SIGNAL(clicked()), this, SLOT(On_btn_Set_Clicked()));
	connect(ui.btn_ViewLog, SIGNAL(clicked()), this, SLOT(On_btn_ViewLog_Clicked()));

	//ServerMgr => UI			:Request Processing Done
	connect(sMgr, SIGNAL(newRecord(string )), this ,SLOT(On_NewRequest_Done(string )));
}

LightWebServer::~LightWebServer()
{
	delete sMgr;
}

void LightWebServer::On_NewRequest_Done(string result)
{
	ui.textBrowser_HttpView->append(QString::fromStdString(result));
}

void LightWebServer::On_btn_Start_Clicked()
{
	sMgr->StartServer();
	ui.btn_Start->setEnabled(false);
	ui.btn_Stop->setEnabled(true);
	string ip, path;
	unsigned int port;

	sMgr->GetServerSet(ip, port, path);

	QString msg = QString("Server Running ") + ip.data()  + ":" + QString::number(port);
	ui.statusBar->showMessage(msg);
}
void LightWebServer::On_btn_Stop_Clicked()
{
	sMgr->StopServer();
	ui.btn_Start->setEnabled(true);
	ui.btn_Stop->setEnabled(false);
	ui.statusBar->showMessage("Server Stopped");
}
void LightWebServer::On_btn_Set_Clicked()
{
	string ip;
	unsigned int port;
	string path;

	sMgr->GetServerSet(ip, port, path);

	if (dialogConf)
	{
		delete dialogConf;
	}

	dialogConf = new Dialog_SetConf(this, QString::fromStdString(ip), port, QString::fromStdString(path));
	connect(dialogConf, SIGNAL(configChanged(QString , unsigned int , QString )), this, SLOT(On_ConfigureChanged(QString , unsigned int , QString )));

	dialogConf->exec();
}
void LightWebServer::On_btn_ViewLog_Clicked()
{
	string path = sMgr->GetLogPath();
	QFile file(path.data());
	if (file.exists())
	{
		QString cmd = QString("notepad.exe ") + path.data();
		WinExec(cmd.toStdString().data(), SW_SHOW);
	}
	else
	{
		QMessageBox::information(this, "info", "No Record Now");
	}
}

void LightWebServer::On_ConfigureChanged(QString ip, unsigned int port, QString path)
{
	sMgr->SetServer(ip.toStdString(), port, path.toStdString());
}