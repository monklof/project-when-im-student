/********************************************************************************
** Form generated from reading UI file 'Dialog_SetConf.ui'
**
** Created by: Qt User Interface Compiler version 5.1.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DIALOG_SETCONF_H
#define UI_DIALOG_SETCONF_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QDialog>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QSpinBox>

QT_BEGIN_NAMESPACE

class Ui_UI_DialogSetConf
{
public:
    QLabel *label;
    QLabel *label_2;
    QLabel *label_3;
    QSpinBox *spinBox_IP3;
    QSpinBox *spinBox_IP2;
    QSpinBox *spinBox_IP1;
    QSpinBox *spinBox_IP0;
    QSpinBox *spinBox_Port;
    QLineEdit *lineEdit_WebPath;
    QLabel *label_Msg;
    QPushButton *btn_OK;
    QPushButton *btn_Cancel;

    void setupUi(QDialog *UI_DialogSetConf)
    {
        if (UI_DialogSetConf->objectName().isEmpty())
            UI_DialogSetConf->setObjectName(QStringLiteral("UI_DialogSetConf"));
        UI_DialogSetConf->resize(400, 300);
        label = new QLabel(UI_DialogSetConf);
        label->setObjectName(QStringLiteral("label"));
        label->setGeometry(QRect(80, 71, 54, 12));
        label_2 = new QLabel(UI_DialogSetConf);
        label_2->setObjectName(QStringLiteral("label_2"));
        label_2->setGeometry(QRect(80, 110, 54, 12));
        label_3 = new QLabel(UI_DialogSetConf);
        label_3->setObjectName(QStringLiteral("label_3"));
        label_3->setGeometry(QRect(80, 150, 54, 12));
        spinBox_IP3 = new QSpinBox(UI_DialogSetConf);
        spinBox_IP3->setObjectName(QStringLiteral("spinBox_IP3"));
        spinBox_IP3->setGeometry(QRect(150, 71, 31, 22));
        spinBox_IP3->setButtonSymbols(QAbstractSpinBox::NoButtons);
        spinBox_IP3->setMaximum(255);
        spinBox_IP2 = new QSpinBox(UI_DialogSetConf);
        spinBox_IP2->setObjectName(QStringLiteral("spinBox_IP2"));
        spinBox_IP2->setGeometry(QRect(190, 71, 31, 22));
        spinBox_IP2->setButtonSymbols(QAbstractSpinBox::NoButtons);
        spinBox_IP2->setMaximum(255);
        spinBox_IP1 = new QSpinBox(UI_DialogSetConf);
        spinBox_IP1->setObjectName(QStringLiteral("spinBox_IP1"));
        spinBox_IP1->setGeometry(QRect(230, 71, 31, 22));
        spinBox_IP1->setButtonSymbols(QAbstractSpinBox::NoButtons);
        spinBox_IP1->setMaximum(255);
        spinBox_IP0 = new QSpinBox(UI_DialogSetConf);
        spinBox_IP0->setObjectName(QStringLiteral("spinBox_IP0"));
        spinBox_IP0->setGeometry(QRect(270, 71, 31, 22));
        spinBox_IP0->setButtonSymbols(QAbstractSpinBox::NoButtons);
        spinBox_IP0->setMaximum(255);
        spinBox_Port = new QSpinBox(UI_DialogSetConf);
        spinBox_Port->setObjectName(QStringLiteral("spinBox_Port"));
        spinBox_Port->setGeometry(QRect(150, 110, 51, 22));
        spinBox_Port->setButtonSymbols(QAbstractSpinBox::NoButtons);
        spinBox_Port->setMaximum(65535);
        spinBox_Port->setValue(80);
        lineEdit_WebPath = new QLineEdit(UI_DialogSetConf);
        lineEdit_WebPath->setObjectName(QStringLiteral("lineEdit_WebPath"));
        lineEdit_WebPath->setGeometry(QRect(150, 150, 191, 20));
        label_Msg = new QLabel(UI_DialogSetConf);
        label_Msg->setObjectName(QStringLiteral("label_Msg"));
        label_Msg->setGeometry(QRect(83, 190, 261, 21));
        btn_OK = new QPushButton(UI_DialogSetConf);
        btn_OK->setObjectName(QStringLiteral("btn_OK"));
        btn_OK->setGeometry(QRect(110, 230, 75, 23));
        btn_Cancel = new QPushButton(UI_DialogSetConf);
        btn_Cancel->setObjectName(QStringLiteral("btn_Cancel"));
        btn_Cancel->setGeometry(QRect(220, 230, 75, 23));

        retranslateUi(UI_DialogSetConf);

        QMetaObject::connectSlotsByName(UI_DialogSetConf);
    } // setupUi

    void retranslateUi(QDialog *UI_DialogSetConf)
    {
        UI_DialogSetConf->setWindowTitle(QApplication::translate("UI_DialogSetConf", "Set Configure", 0));
        label->setText(QApplication::translate("UI_DialogSetConf", "IP", 0));
        label_2->setText(QApplication::translate("UI_DialogSetConf", "PORT", 0));
        label_3->setText(QApplication::translate("UI_DialogSetConf", "Web Path", 0));
        spinBox_IP3->setSuffix(QString());
        spinBox_IP2->setSuffix(QString());
        spinBox_IP1->setSuffix(QString());
        spinBox_IP0->setSuffix(QString());
        spinBox_Port->setSuffix(QString());
        label_Msg->setText(QString());
        btn_OK->setText(QApplication::translate("UI_DialogSetConf", "OK", 0));
        btn_Cancel->setText(QApplication::translate("UI_DialogSetConf", "Cancel", 0));
    } // retranslateUi

};

namespace Ui {
    class UI_DialogSetConf: public Ui_UI_DialogSetConf {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DIALOG_SETCONF_H
