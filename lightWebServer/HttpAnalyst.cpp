#include "HttpAnalyst.h"

HttpAnalyst::HttpAnalyst(QObject *parent,string ipAdr, string webPath, string requestData)
	: QThread(parent),
	ip(ipAdr),
	webMainPath(webPath),
	rstData(requestData)
{

}

HttpAnalyst::~HttpAnalyst()
{

}
