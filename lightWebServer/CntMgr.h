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
	
	//��Դ������
	//

	//http����������
	vector<HttpResponder *> v_HttpResponders;
	//http��������Id����v_HttpResponderһһ��Ӧ
	vector<unsigned int> v_HttpResponderIds;
	//���ͨ���˵�Tcp����id�������ȴ��ͷ�HttpResponser��Դ
	vector<unsigned int> v_finishedTcpId;
	//�ͷ���Դ����
	QMutex metux_Operating;

	//������
	//

	//������Ŀ¼
	string webPath;
	//�������Ƿ��ڼ���״̬
	string ip;
	bool isOn;
	//��������
	QTcpServer *server;
	//���������
	const static int maxConnections = 1000;//�˴�maxConnection����ñ�65535С
	QTimer timerCheck;


private slots:
	//�½�����
	void On_NewClients_Connected();
	//���������ͨ��
	void On_NewRecordArrived(bool isAlive, unsigned int cntId, bool hasNewRecords, string records);
	//����Ƿ����������ͨ��
	void On_timerCheck_Ticked();
};

#endif // CNTMGR_H
