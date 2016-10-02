#ifndef HTTPANALYST_H
#define HTTPANALYST_H

#include <QThread>
#include <QTextStream>
#include <QFile>
#include <QByteArray>
#include <QDataStream>


#include "GlobalHead.h"
class HttpAnalyst : public QThread
{
	Q_OBJECT

	void run() Q_DECL_OVERRIDE
	{
		//
		//查找请求行和Connection首部
		//

		string rstLine = rstData.substr(0, rstData.find_first_of('\r'));

		//解析请求行
		//获取方法，路径和协议版本
		//

		string method = "";
		string route = "";
		string httpVersion = "";

		int spaceNum = 0;
		int lastIndex = 0;
		for (int i = 0; i < rstLine.length(); i++)
		{
			if (rstLine[i] == ' ')
			{
				spaceNum++;
				switch(spaceNum)
				{
				case 1:
					method = rstLine.substr(lastIndex, i - lastIndex);
					lastIndex = i + 1;
					break;
				case 2:
					route = rstLine.substr(lastIndex, i - lastIndex);
					lastIndex = i + 1;
					break;
				default:
					break;
				}
			}
		}

		if (spaceNum != 2)
		{
			emit finishedAnalyse(true , rstLine, 400, "Bad Request", 
				(QString("HTTP/1.1 400 Bad Request\r\nConnection: ") + 
				"Keep-Alive").toStdString().data());
			return;
		}

		httpVersion = rstLine.substr(lastIndex, rstLine.length() - lastIndex).data();

		if (method != "GET")
		{
			emit finishedAnalyse(true , rstLine, 400, "Bad Request", 
				(QString("HTTP/1.1 400 Bad Request\r\nConnection: ") + 
				"Keep-Alive").toStdString().data());
			return;
		}

		if (httpVersion != "HTTP/1.1")
		{
			emit finishedAnalyse(true , rstLine, 505, "HTTP Version Not Supported", 
				(QString("HTTP/1.1 505 HTTP Version Not Supported\r\nConnection: ") + 
				"Keep-Alive").toStdString().data());
			return;
		}

		//
		//解析首部行，下面都是GET方法下的解析
		//

		bool keepAlive = false;
		string refRoute = "";

		
		unsigned int nextHeadIndex = rstData.find_first_of('\n');//下个首部的index
		
		if ((nextHeadIndex != string::npos) && (nextHeadIndex != rstData.length() - 1))//有首部行
		{
			//获取剩下的首部行(去掉末尾空行的\r\n)
			string headLines = rstData.substr(nextHeadIndex + 1, rstData.length() - nextHeadIndex -1 -2);
			bool firstLine = true;

			OutputDebugStringA("Head->Value:\n");

			//还存在首部行就继续
			while ((nextHeadIndex != string::npos) && (nextHeadIndex != headLines.length() - 1))
			{
				//获取剩下的首部
				if (firstLine)
				{
					firstLine = false;
				}
				else
					headLines = headLines.substr(nextHeadIndex + 1);

				//获取该行首部
				unsigned int hI = headLines.find_first_of(':');
				unsigned int vI = headLines.find_first_of('\r');
				
				if (hI == string::npos || vI == string::npos || vI <= (hI + 1))//没有找到首部
				{
					emit finishedAnalyse(true, rstLine, 400, "Bad Request", "HTTP/1.1 400 Bad Request\r\nConnection: Keep-Alive");
					return;
				}
				
				string head = headLines.substr(0, hI);
				string value = headLines.substr(hI + 2, vI - hI - 2);

				OutputDebugStringA(head.data());
				OutputDebugStringA("->");
				OutputDebugStringA(value.data());
				OutputDebugStringA("\n");

				if (head == "Connection")
				{
					if (value == "Close" || value == "close")
					{
						keepAlive = false;
					}
					else
					{
						keepAlive = true;
					}
				}
				else if (head == "Referer")
				{
					unsigned int index = value.find(ip);
					if (index != string::npos && (index + ip.length() + 1 != value.length()))
					{
						refRoute = value.substr(index + ip.length() + 1);
					}
					else
						refRoute = "";
				}
				
				nextHeadIndex = headLines.find_first_of('\n') ;
			}
		}
		
		string connectionPart = keepAlive == true?"Keep-Alive":"close";
		
		//http解析完毕，现在提取文件
		QFile file;

		//主文件
		if (route == "/")
		{
			file.setFileName(QString::fromStdString(webMainPath) + "\\default.html");
			if (file.exists())
			{
				file.open(QFile::ReadOnly | QFile::Text);
				QTextStream in(&file);
				QString rpd = QString("HTTP/1.1 200 OK\r\nConnection: ") + connectionPart.data() + "\r\nContent-Length: " + 
					QString::number(file.size())+ 
					"\r\nContent-Type: text/html\r\n\r\n" + in.readAll();
				emit finishedAnalyse(keepAlive, rstLine, 200, "OK", QByteArray(rpd.toStdString().data()));
				file.close();
			}
			else
			{
				emit finishedAnalyse(keepAlive, rstLine, 404, "Not Found", 
					(QString("HTTP/1.1 404 Not Found\r\nConnection: ") +  
					connectionPart.data()).toStdString().data());
			}

			return;
		}
		//
		//其他文件类型响应
		//

		string absRoute = webMainPath;
		string fileType = "";

		//获取文件绝对路径
		for (int i = 0; i < refRoute.length(); i++)
		{
			if (refRoute[i] == '/')
			{
				absRoute += "\\";
			}
			else
				absRoute += refRoute[i];
		}

		for (int i = 0; i < route.length(); i++)
		{
			if (route[i] == '/')
			{
				absRoute += "\\";
			}
			else
			{
				absRoute += route[i];
			}
		}
		
		//判断文件是否存在
		file.setFileName(QString::fromStdString(absRoute));
		if (!file.exists())
		{
			emit finishedAnalyse(keepAlive, rstLine, 404, "Not Found", 
				(QString("HTTP/1.1 404 Not Found\r\nConnection: ") +  
				connectionPart.data()).toStdString().data());
			return;
		}

		//获取文件后缀
		for (int i = route.length() - 1; i >= 0; i--)//文件后缀
		{
			if (route[i] == '.')
			{
				if (i == route.length() - 1)
				{
					emit finishedAnalyse(keepAlive , rstLine, 400, "Bad Request", 
						(QString("HTTP/1.1 400 Bad Request\r\nConnection: ") + 
						connectionPart.data()).toStdString().data());
					return;
				}
				
				fileType = route.substr(i + 1, route.length() - i - 1);
				
				break;
			}

			if (route[i] == '/')
			{
				emit finishedAnalyse(keepAlive , rstLine, 400, "Bad Request", 
					(QString("HTTP/1.1 400 Bad Request\r\nConnection: ") + 
					connectionPart.data()).toStdString().data());
				return;
			}
		}


		//fileType转化为小写
		for (int i = 0; i < fileType.length(); i++)
		{
			if (fileType[i] > 'z')
			{
				fileType[i] -= ('z' - 'a');
			}
		}

		if (fileType == "html")
		{
			file.open(QFile::ReadOnly | QFile::Text);
			QTextStream in(&file);
			QString rpd = QString("HTTP/1.1 200 OK\r\nConnection: ") + connectionPart.data() + "\r\nContent-Length: " + 
				QString::number(file.size())+ 
				"\r\nContent-Type: text/html\r\n\r\n" + in.readAll();
			emit finishedAnalyse(keepAlive, rstLine, 200, "OK", QByteArray(rpd.toStdString().data()));
			file.close();
			return;
		}
		else if (fileType == "txt")
		{
			file.open(QFile::ReadOnly | QFile::Text);
			QTextStream in(&file);
			QString rpd = QString("HTTP/1.1 200 OK\r\nConnection: ") + connectionPart.data() + "\r\nContent-Length: " + 
				QString::number(file.size())+ 
				"\r\nContent-Type: text/plain\r\n\r\n" + in.readAll();
			emit finishedAnalyse(keepAlive, rstLine, 200, "OK", QByteArray(rpd.toStdString().data()));
			file.close();
			return;
		}
		else if (fileType == "jpg")
		{
			file.open(QFile::ReadOnly);
			QDataStream in(&file);
			QString data0 = QString("HTTP/1.1 200 OK\r\nConnection: ") + connectionPart.data() + "\r\nContent-Length: " + 
				QString::number(file.size()) +
				"\r\nContent-Type: image/jpeg\r\n\r\n";
			char *data1 = new char[file.size()];
			in.readRawData(data1, file.size());

			QByteArray data = QByteArray(data0.toStdString().data()) + 
				QByteArray::fromRawData(data1, file.size()) ;//必须用fromRawData，否则会以'\0'为结束符
			emit finishedAnalyse(keepAlive, rstLine, 200, "OK", data);
			delete [] data1;
			file.close();
			return;
		}
		else//未知文件类型
		{
			emit finishedAnalyse(keepAlive, rstLine, 403, "Forbidden", (QString("HTTP/1.1 403 Forbidden\r\nConnection: ") + connectionPart.data()).toStdString().data());
			return;
		}
	}

public:
	HttpAnalyst(QObject *parent, string ipAdr, string webPath, string requestData);
	~HttpAnalyst();

	const string ip;
	const string webMainPath;
	const string rstData;

signals:
	void finishedAnalyse(bool isKeepAlive, string rstLine, int statusCode, string phrase, QByteArray responseData);
private:
	
};

#endif // HTTPANALYST_H
