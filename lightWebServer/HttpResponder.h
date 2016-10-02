#ifndef HTTPRESPONSE_H
#define HTTPRESPONSE_H

#include <QtNetwork/QtNetwork>
#include <QTimer>
#include <QByteArray>

#include "GlobalHead.h"
#include "HttpAnalyst.h"

//响应单条客户请求，处理完成后，自动关闭链接，并通知CntMgr：
//发送报告和请求释放资源

class HttpResponder:public QObject
{
	Q_OBJECT

public:
	HttpResponder(QObject *parent,
		unsigned int cntId, QTcpSocket *clientConnection, string path);
	~HttpResponder();

signals:
	//发送报告给CntMgr
	void newRequestResult(bool isAlive, unsigned int cntId, bool hasNewRecords, string records);

private:
	QMutex mutex_Operating;

	//链接信息
	const unsigned int connectionId;
	QTcpSocket *const cConnection;
	const string webPath;
	const string ip;

	//用以判断是否继续从套接字接受数据
	bool needDisconnected;

	//用于检测线程返回结果
	QTimer timerCheck;
	QTimer timerOverTime;


	//
	//生成记录要用
	//

	//http的请求行
	vector<string> rstCmdLine;
	//状态码
	vector<int> statusCode;
	//短语
	vector<string> phrase;
	vector<QByteArray> rstData;
	
	//发送响应报文
	
private slots:
	void On_NewData_Arrived();
	void On_Tcp_Disconnected();
	void On_Analyse_Finished(bool isKeepAlive, string rstLine, int statusCode, string phrase, QByteArray responseData);
	void On_timerCheck_Ticked();
	void On_timerOverTime_Ticked();
};

#endif // HTTPRESPONSE_H
