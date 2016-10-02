#ifndef SERVERMGR_H
#define SERVERMGR_H

#include <QObject>
#include <QFile>
#include <QTextStream>

#include "CntMgr.h"
#include "GlobalHead.h"

class WebMgr : public QObject
{
	Q_OBJECT

public:
	WebMgr(QObject *parent);
	~WebMgr();

	bool StartServer();
	bool StopServer();
	void GetServerSet(string &ip, unsigned int &port, string &webPath);
	void SetServer(string ip, unsigned int port, string webPath);
	string GetLogPath();

signals:
	void newRecord(string re);

public slots:
	void On_NewRecord_Arrived(string re);

private:
	//server sets
	string ip;
	unsigned int port;
	string webPath;
	const string setPath;
	const string logPath;

	CntMgr *cntMgr;

	QFile logFile;
	QTextStream *logStream;
	
};


#endif // SERVERMGR_H
