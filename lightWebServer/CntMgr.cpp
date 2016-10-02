#include "CntMgr.h"
#include <QtNetwork/QTcpSocket>
#include "HttpResponder.h"

CntMgr::CntMgr(QObject *parent):
	QObject(parent),
	isOn(false),
	server(new QTcpServer(this))
{
	connect(server, SIGNAL(newConnection()), this, SLOT(On_NewClients_Connected()));
	connect(&timerCheck, SIGNAL(timeout()), this, SLOT(On_timerCheck_Ticked()));
	timerCheck.start(2);
}

CntMgr::~CntMgr()
{

}

bool CntMgr::StartServer(string ip, unsigned int port, string path)
{
	if (server->isListening())
	{
		return false;
	}

	this->ip = ip;
	this->webPath = path;

	server->listen(QHostAddress::Any, port);
	server->setMaxPendingConnections(maxConnections);

	return true;
}

bool CntMgr::StopServer()
{
	if (server->isListening())
	{
		server->close();
		return true;
	}
	return false;
}

void CntMgr::On_NewClients_Connected()
{
	QTcpSocket *sc = server->nextPendingConnection();
	if (sc->localAddress().toString().toStdString() != ip) //过滤
	{
		sc->close();
		return;
	}

	metux_Operating.lock();
	//
	//新建响应器和分配资源
	//

	//寻找可用id
	/*
	 * 算法，注意，所有v_HttpRespoderIds中的id号都是从大到小排列的，因此满足:
	 * v_HttpResponderIds[i] >= i
	 * 
	 */
	unsigned int id = 0;
	for (id = 0; id < v_HttpResponderIds.size(); id++)
	{
		if (v_HttpResponderIds[id] > id)//index = id 可插入新id
		{
			break;
		}
	}

	//新建响应器
	HttpResponder *res = new HttpResponder(this, id, sc, webPath);
	connect(res, SIGNAL(newRequestResult(bool , unsigned int , bool , string )),
		this, SLOT(On_NewRecordArrived(bool , unsigned int , bool , string )));
	//归入管理列表
	if (id == v_HttpResponderIds.size())
	{
		v_HttpResponderIds.push_back(id);
		v_HttpResponders.push_back(res);
	}
	else
	{
		v_HttpResponderIds.insert(v_HttpResponderIds.begin(), id, id);
		v_HttpResponders.insert(v_HttpResponders.begin(), id, res);
	}
	metux_Operating.unlock();
}

void CntMgr::On_NewRecordArrived(bool isAlive, unsigned int cntId, bool hasNewRecords, string records)
{
	//发送记录
	if (hasNewRecords)
	{
		emit newRecord(records);
	}

	//管理资源
	if (!isAlive)
	{
		metux_Operating.lock();
		//按序插入
		v_finishedTcpId.push_back(cntId);
		metux_Operating.unlock();
	}
}

void CntMgr::On_timerCheck_Ticked()
{
	metux_Operating.lock();
	/*
	 * 算法：
	 * 便利查找，挨个删除(有时间用二分查找)
	 */
	while(v_finishedTcpId.size() != 0)
	{
		unsigned int i = 0;
		for (i = 0; i < v_HttpResponderIds.size(); i++)
		{
			if (v_HttpResponderIds[i] == v_finishedTcpId.front())//找到
			{
				v_HttpResponderIds.erase(v_HttpResponderIds.begin() + i);
				delete v_HttpResponders[i];
				v_HttpResponders.erase(v_HttpResponders.begin() + i);
				break;
			}
		}

		v_finishedTcpId.erase(v_finishedTcpId.begin());
	}

	metux_Operating.unlock();
}