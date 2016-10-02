#ifndef HTTPRESPONSE_H
#define HTTPRESPONSE_H

#include <QtNetwork/QtNetwork>
#include <QTimer>
#include <QByteArray>

#include "GlobalHead.h"
#include "HttpAnalyst.h"

//��Ӧ�����ͻ����󣬴�����ɺ��Զ��ر����ӣ���֪ͨCntMgr��
//���ͱ���������ͷ���Դ

class HttpResponder:public QObject
{
	Q_OBJECT

public:
	HttpResponder(QObject *parent,
		unsigned int cntId, QTcpSocket *clientConnection, string path);
	~HttpResponder();

signals:
	//���ͱ����CntMgr
	void newRequestResult(bool isAlive, unsigned int cntId, bool hasNewRecords, string records);

private:
	QMutex mutex_Operating;

	//������Ϣ
	const unsigned int connectionId;
	QTcpSocket *const cConnection;
	const string webPath;
	const string ip;

	//�����ж��Ƿ�������׽��ֽ�������
	bool needDisconnected;

	//���ڼ���̷߳��ؽ��
	QTimer timerCheck;
	QTimer timerOverTime;


	//
	//���ɼ�¼Ҫ��
	//

	//http��������
	vector<string> rstCmdLine;
	//״̬��
	vector<int> statusCode;
	//����
	vector<string> phrase;
	vector<QByteArray> rstData;
	
	//������Ӧ����
	
private slots:
	void On_NewData_Arrived();
	void On_Tcp_Disconnected();
	void On_Analyse_Finished(bool isKeepAlive, string rstLine, int statusCode, string phrase, QByteArray responseData);
	void On_timerCheck_Ticked();
	void On_timerOverTime_Ticked();
};

#endif // HTTPRESPONSE_H
