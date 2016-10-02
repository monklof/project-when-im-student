/********************************************************************************
** Form generated from reading UI file 'LightWebServer.ui'
**
** Created by: Qt User Interface Compiler version 5.1.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_LIGHTWEBSERVER_H
#define UI_LIGHTWEBSERVER_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QTextBrowser>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_LightWebServer_UI
{
public:
    QWidget *centralWidget;
    QTextBrowser *textBrowser_HttpView;
    QPushButton *btn_Start;
    QPushButton *btn_Set;
    QPushButton *btn_ViewLog;
    QPushButton *btn_Stop;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *LightWebServer_UI)
    {
        if (LightWebServer_UI->objectName().isEmpty())
            LightWebServer_UI->setObjectName(QStringLiteral("LightWebServer_UI"));
        LightWebServer_UI->resize(686, 561);
        centralWidget = new QWidget(LightWebServer_UI);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        textBrowser_HttpView = new QTextBrowser(centralWidget);
        textBrowser_HttpView->setObjectName(QStringLiteral("textBrowser_HttpView"));
        textBrowser_HttpView->setGeometry(QRect(0, 0, 691, 481));
        textBrowser_HttpView->setAutoFillBackground(false);
        btn_Start = new QPushButton(centralWidget);
        btn_Start->setObjectName(QStringLiteral("btn_Start"));
        btn_Start->setGeometry(QRect(90, 500, 75, 23));
        btn_Set = new QPushButton(centralWidget);
        btn_Set->setObjectName(QStringLiteral("btn_Set"));
        btn_Set->setGeometry(QRect(450, 500, 75, 23));
        btn_ViewLog = new QPushButton(centralWidget);
        btn_ViewLog->setObjectName(QStringLiteral("btn_ViewLog"));
        btn_ViewLog->setGeometry(QRect(350, 500, 75, 23));
        btn_Stop = new QPushButton(centralWidget);
        btn_Stop->setObjectName(QStringLiteral("btn_Stop"));
        btn_Stop->setEnabled(false);
        btn_Stop->setGeometry(QRect(190, 500, 75, 23));
        LightWebServer_UI->setCentralWidget(centralWidget);
        statusBar = new QStatusBar(LightWebServer_UI);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        LightWebServer_UI->setStatusBar(statusBar);

        retranslateUi(LightWebServer_UI);

        QMetaObject::connectSlotsByName(LightWebServer_UI);
    } // setupUi

    void retranslateUi(QMainWindow *LightWebServer_UI)
    {
        LightWebServer_UI->setWindowTitle(QApplication::translate("LightWebServer_UI", "LightWebServer", 0));
        btn_Start->setText(QApplication::translate("LightWebServer_UI", "Start", 0));
        btn_Set->setText(QApplication::translate("LightWebServer_UI", "Set", 0));
        btn_ViewLog->setText(QApplication::translate("LightWebServer_UI", "View Log", 0));
        btn_Stop->setText(QApplication::translate("LightWebServer_UI", "Stop", 0));
    } // retranslateUi

};

namespace Ui {
    class LightWebServer_UI: public Ui_LightWebServer_UI {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_LIGHTWEBSERVER_H
