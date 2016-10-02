#include "WebMgr.h"
#include <QMessageBox>


WebMgr::WebMgr(QObject *parent):
	QObject(parent),
	cntMgr(new CntMgr(this)),
	ip("222.20.103.232"),
	port(80),
	webPath("C:\\httpTest"),
	setPath("sets.txt"),
	logPath("log.txt")
{
	connect(cntMgr, SIGNAL(newRecord(string)), this, SLOT(On_NewRecord_Arrived(string)));

	//加载设置
	QFile confFile;
	confFile.setFileName(setPath.data());
	if (confFile.exists())
	{
		if (confFile.open(QFile::ReadOnly | QFile::Text))
		{
			QTextStream in(&confFile);
			ip = in.readLine().toStdString();
			port = in.readLine().toUInt();
			webPath = in.readLine().toStdString();
			confFile.close();
		}
	}
}

WebMgr::~WebMgr()
{
	if (logFile.isOpen())
	{
		logStream->flush();
		logFile.close();
		delete logStream;
	}
}

bool WebMgr::StartServer()
{
	cntMgr->StartServer(ip, port, webPath);
	return true;
}
bool WebMgr::StopServer()
{
	cntMgr->StopServer();
	return true;
}
void WebMgr::GetServerSet(string &ip, unsigned int &port, string &webPath)
{
	ip = this->ip;
	port = this->port;
	webPath = this->webPath;
}
void WebMgr::SetServer(string ip, unsigned int port, string webPath)
{
	//保存设置
	QFile confFile;
	confFile.setFileName(setPath.data());
	if (confFile.open(QFile::WriteOnly | QFile::Text))
	{
		QTextStream out(&confFile);
		out << ip.data() << endl;
		out << port << endl;
		out << webPath.data();
		out.flush();
		confFile.close();


		this->ip = ip;
		this->port = port;
		this->webPath = webPath;
	}
	else
	{
		QMessageBox::information(NULL, "Note", "Set Not Saved!");
	}
}

string WebMgr::GetLogPath()
{
	return logPath;
}
void WebMgr::On_NewRecord_Arrived(string re)
{
	if (!logFile.isOpen())
	{
		logFile.setFileName(logPath.data());
		if (logFile.open(QFile::WriteOnly | QFile::Append | QFile::Text))
		{
			logStream = new QTextStream(&logFile);
			*logStream << re.data() << endl;
			logStream->flush();
		}
	}
	else
	{
		*logStream << re.data() << endl;
		logStream->flush();
	}

	emit newRecord(re);
}