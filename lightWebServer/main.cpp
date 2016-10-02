#include "LightWebServer.h"
#include <QtWidgets/QApplication>

int main(int argc, char *argv[])
{
	qRegisterMetaType<string>("string");
	QApplication a(argc, argv);
	LightWebServer w;
	w.show();
	return a.exec();
}
