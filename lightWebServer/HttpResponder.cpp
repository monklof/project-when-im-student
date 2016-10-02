#include "HttpResponder.h"
#include <QDateTime>

HttpResponder::HttpResponder(QObject *parent, unsigned int cntId,QTcpSocket *clientConnection, string path):
	connectionId(cntId),
	cConnection(clientConnection),
	ip(clientConnection->localAddress().toString().toStdString()),
	webPath(path),
	needDisconnected(false)
{
	//���Ӻ��׽���
	connect(cConnection, SIGNAL(readyRead()),this, SLOT(On_NewData_Arrived()));
	connect(cConnection, SIGNAL(disconnected()),this, SLOT(On_Tcp_Disconnected()));

	//������ʱ��
	connect(&timerCheck, SIGNAL(timeout()), this, SLOT(On_timerCheck_Ticked()));
	connect(&timerOverTime, SIGNAL(timeout()), this, SLOT(On_timerOverTime_Ticked()));

	timerCheck.start(1);
	timerOverTime.start(5000);//�־��������5s
}

HttpResponder::~HttpResponder()
{
	timerCheck.stop();
}

void HttpResponder::On_NewData_Arrived()
{
	OutputDebugStringA("In HttpResponder: New Data Arrived!\n");

	//�ȴ��Ͽ�����
	if (needDisconnected)
		return;

	//��ȡ����
	string rstData = cConnection->readAll().constData();

	OutputDebugStringA(rstData.data());
	OutputDebugStringA("\n");

	//�½��̷߳���
	HttpAnalyst *httpAnalyst = new HttpAnalyst(this, ip, webPath, rstData);
	connect(httpAnalyst, SIGNAL(finishedAnalyse(bool, string, int , string , QByteArray )), 
		this, SLOT(On_Analyse_Finished(bool, string, int, string, QByteArray)));
	httpAnalyst->start();
}

void HttpResponder::On_Tcp_Disconnected()
{
	OutputDebugStringA("In HttpResponder: Connectiond Breaked!\n");

	mutex_Operating.lock();
	rstData.clear();
	rstCmdLine.clear();
	statusCode.clear();
	phrase.clear();
	mutex_Operating.unlock();

	emit newRequestResult(false, connectionId, false, "");
}

void HttpResponder::On_Analyse_Finished(bool isKeepAlive, string rstLine, int statusCode, string phrase, QByteArray responseData)
{
	mutex_Operating.lock();
	if (!isKeepAlive)
	{
		this->needDisconnected = true;
	}
	this->statusCode.push_back(statusCode);
	this->phrase.push_back(phrase);
	this->rstData.push_back(responseData);
	this->rstCmdLine.push_back(rstLine);
	mutex_Operating.unlock();
}

void HttpResponder::On_timerCheck_Ticked()
{
	mutex_Operating.lock();
	while (statusCode.size() != 0)
	{
		//������Ӧ����
		cConnection->write(rstData.front());

		//���ɱ���
		//���ɼ�¼
		QString record = QDateTime::currentDateTime().toString("yyyy/MM/dd hh:mm:ss:zzz") + ":\n" + 
			cConnection->peerAddress().toString() + ":" +
			QString::number(cConnection->peerPort()) + " " + QString(rstCmdLine.front().data())+ " " +
			QString::number(statusCode.front()) + " " + QString(phrase.front().data());


		emit newRequestResult(true, connectionId, true, record.toStdString());
		rstData.erase(rstData.begin());
		rstCmdLine.erase(rstCmdLine.begin());
		statusCode.erase(statusCode.begin());
		phrase.erase(phrase.begin());
	}
	mutex_Operating.unlock();
	if (needDisconnected == true && cConnection->isOpen())
	{
		cConnection->close();
	}
}

void HttpResponder::On_timerOverTime_Ticked()
{
	timerOverTime.stop();
	needDisconnected = true;
}