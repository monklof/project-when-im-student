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
	if (sc->localAddress().toString().toStdString() != ip) //����
	{
		sc->close();
		return;
	}

	metux_Operating.lock();
	//
	//�½���Ӧ���ͷ�����Դ
	//

	//Ѱ�ҿ���id
	/*
	 * �㷨��ע�⣬����v_HttpRespoderIds�е�id�Ŷ��ǴӴ�С���еģ��������:
	 * v_HttpResponderIds[i] >= i
	 * 
	 */
	unsigned int id = 0;
	for (id = 0; id < v_HttpResponderIds.size(); id++)
	{
		if (v_HttpResponderIds[id] > id)//index = id �ɲ�����id
		{
			break;
		}
	}

	//�½���Ӧ��
	HttpResponder *res = new HttpResponder(this, id, sc, webPath);
	connect(res, SIGNAL(newRequestResult(bool , unsigned int , bool , string )),
		this, SLOT(On_NewRecordArrived(bool , unsigned int , bool , string )));
	//��������б�
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
	//���ͼ�¼
	if (hasNewRecords)
	{
		emit newRecord(records);
	}

	//������Դ
	if (!isAlive)
	{
		metux_Operating.lock();
		//�������
		v_finishedTcpId.push_back(cntId);
		metux_Operating.unlock();
	}
}

void CntMgr::On_timerCheck_Ticked()
{
	metux_Operating.lock();
	/*
	 * �㷨��
	 * �������ң�����ɾ��(��ʱ���ö��ֲ���)
	 */
	while(v_finishedTcpId.size() != 0)
	{
		unsigned int i = 0;
		for (i = 0; i < v_HttpResponderIds.size(); i++)
		{
			if (v_HttpResponderIds[i] == v_finishedTcpId.front())//�ҵ�
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