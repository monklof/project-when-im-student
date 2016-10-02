#ifndef DIALOG_SETCONF_H
#define DIALOG_SETCONF_H

#include <QDialog>
#include "ui_Dialog_SetConf.h"
#include "GlobalHead.h"

class Dialog_SetConf : public QDialog
{
	Q_OBJECT

public:
	Dialog_SetConf(QWidget *parent, QString ip, unsigned int port, QString path);
	~Dialog_SetConf();

signals:
	void configChanged(QString ip, unsigned int port, QString path);

private:
	Ui::UI_DialogSetConf ui;

private slots:
	void On_btn_Ok_Clicked();
	void On_btn_Cancel_Clicked();
	void On_lineEdit_WebPath_TextChanged(QString text);
};

#endif // DIALOG_SETCONF_H
