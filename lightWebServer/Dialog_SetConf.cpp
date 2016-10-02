#include "Dialog_SetConf.h"
#include <QDir>
#include <QMessageBox>

Dialog_SetConf::Dialog_SetConf(QWidget *parent, QString ip, unsigned int port, QString path)
	: QDialog(parent)
{
	ui.setupUi(this);
	
	int j, index2, index1, index0, ip3, ip2, ip1, ip0;
	j = 0;

	for (int i = 0; i < ip.length(); i++)
	{
		if (ip[i] == '.')
		{
			j++;
			switch (j)
			{
			case 1:
				index2 = i;
				break;
			case 2:
				index1 = i;
				break;
			case 3:
				index0 = i;
				break;
			default:
				break;
			}
		}
	}
	string ipStr = ip.toStdString();
	ip3 = QString(ipStr.substr(0, index2).data()).toInt();
	ip2 = QString(ipStr.substr(index2 + 1, index1 - index2 - 1).data()).toInt();
	ip1 = QString(ipStr.substr(index1 + 1, index0 - index1 - 1).data()).toInt();
	ip0 = QString(ipStr.substr(index0 + 1, ipStr.length() - index0 - 1).data()).toInt();

	ui.spinBox_IP3->setValue(ip3);
	ui.spinBox_IP2->setValue(ip2);
	ui.spinBox_IP1->setValue(ip1);
	ui.spinBox_IP0->setValue(ip0);

	ui.spinBox_Port->setValue(port);
	ui.lineEdit_WebPath->setText(path);

	connect(ui.btn_OK, SIGNAL(clicked()), this, SLOT(On_btn_Ok_Clicked()));
	connect(ui.btn_Cancel, SIGNAL(clicked()), this, SLOT(On_btn_Cancel_Clicked()));
	connect(ui.lineEdit_WebPath, SIGNAL(textChanged(QString)), this, SLOT(On_lineEdit_WebPath_TextChanged(QString)));
}

Dialog_SetConf::~Dialog_SetConf()
{

}


void Dialog_SetConf::On_btn_Ok_Clicked()
{
	QString path = ui.lineEdit_WebPath->text();
	QDir dir(path);
	if (path.isEmpty() || path.isNull() || !dir.exists())
	{
		QMessageBox::information(this, "warning", "this path not exits!");
	}
	else
	{
		QString ip;
		QString path;
		unsigned int port;

		ip = ui.spinBox_IP3->text() + "." + 
			ui.spinBox_IP2->text() + "." +
			ui.spinBox_IP1->text() + "." +
			ui.spinBox_IP0->text();
		path = ui.lineEdit_WebPath->text();
		port = ui.spinBox_Port->value();

		if (path[path.length() - 1] != '\\')
		{
			path += '\\';
		}

		emit configChanged(ip, port, path);
		emit accept();
	}
	
}

void Dialog_SetConf::On_btn_Cancel_Clicked()
{
	emit reject();
}

void Dialog_SetConf::On_lineEdit_WebPath_TextChanged(QString text)
{
	QDir dir(text);
	if (!dir.exists())
	{
		ui.label_Msg->setText(
			QString("<html><head/><body><p><span style=\" color:#ff0000;\">") + 
			"PATH NOT EXITS!" + "</span></p></body></html>");
	}
	else
	{
		ui.label_Msg->clear();
	}
}