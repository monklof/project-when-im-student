#ifndef CNTMGR_H
#define CNTMGR_H

#include <QObject>
#include <QtNetwork/QtNetwork>
#include <QtNetwork/QTcpServer>
#include <QMutex>
#include <Qtimer>

#include "GlobalHead.h"
#include "HttpResponder.h"

class CntMgr : public QObject
{
	Q_OBJECT

public:
	CntMgr(QObject *parent);
	~CntMgr();

	bool StartServer(string ip, unsigned int port, string path);
	bool StopServer();

signals:
	void newRecord(string records);

private:
	
	//资源管理用
	//

	//http处理器集合
	vector<HttpResponder *> v_HttpResponders;
	//http处理器的Id，与v_HttpResponder一一对应
	vector<unsigned int> v_HttpResponderIds;
	//完成通信了的Tcp链接id容器，等待释放HttpResponser资源
	vector<unsigned int> v_finishedTcpId;
	//释放资源锁定
	QMutex metux_Operating;

	//服务器
	//

	//服务器目录
	string webPath;
	//服务器是否处于监听状态
	string ip;
	bool isOn;
	//服务器类
	QTcpServer *server;
	//最大连接数
	const static int maxConnections = 1000;//此处maxConnection必须得比65535小
	QTimer timerCheck;


private slots:
	//新建连接
	void On_NewClients_Connected();
	//有链接完成通信
	void On_NewRecordArrived(bool isAlive, unsigned int cntId, bool hasNewRecords, string records);
	//检查是否有链接完成通信
	void On_timerCheck_Ticked();
};

#endif // CNTMGR_H
