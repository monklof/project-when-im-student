using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.IO.Ports;
namespace HelloWorld
{

    public partial class frmHelloWorld : Form
    {
        #region 锁定按钮
        //开始发送按钮锁定
        private void Lock_BeginSend(bool lockIt)
        {
            if (lockIt)
            {
                button_serialOpen.Enabled = false;
                checkBox_sendRound.Enabled = false;
            }
            else
            {
                button_serialOpen.Enabled = true;
                checkBox_sendRound.Enabled = true;
            }
        }
        //开始推按钮锁定
        private void Lock_BeginTui(bool lockIt)
        {
            if (lockIt)
            {
                button_serialOpen.Enabled = false;//不能关闭串口
                numericUpDown_Interval.Enabled = false;

                textBox_BeginCh.Enabled = false;
                textBox_EndCh.Enabled = false;

            }
            else
            {
                button_serialOpen.Enabled = true;
                numericUpDown_Interval.Enabled = true;

                textBox_EndCh.Enabled = true;
                textBox_BeginCh.Enabled = true;
            }
        }
        //打开串口锁定
        private void Lock_OpenCom(bool lockIt)
        {
            if (lockIt)
            {
                comboBox_serialName.Enabled = false;
                comboBox_Bault.Enabled = false;
                comboBox_checkBit.Enabled = false;
                comboBox_statusBit.Enabled = false;
                comboBox_stopBit.Enabled = false;
            }
            else
            {
                comboBox_serialName.Enabled = true;
                comboBox_Bault.Enabled = true;
                comboBox_checkBit.Enabled = true;
                comboBox_statusBit.Enabled = true;
                comboBox_stopBit.Enabled = true;
            }
        }

        //开始Ctrl锁定
        private void Lock_BeginCtrl(bool lockIt)
        {
            if (lockIt)
            {

                button_serialOpen.Enabled = false;//不能关闭串口
                numericUpDown_Ctrl.Enabled = false;//间隔不准再变

                textBox_BeginCh.Enabled = false;//开始标志不准再变
                textBox_EndCh.Enabled = false;//结束标志不准再变
            }
            else
            {
                button_serialOpen.Enabled = true;//关闭串口
                numericUpDown_Ctrl.Enabled = true;//间隔可以再变

                textBox_BeginCh.Enabled = true;//开始标志可以再变
                textBox_EndCh.Enabled = true;//结束标志可以再变
            }
        }

        #endregion

        #region 配置相关
        //设置默认配置
        private void SetDefault()
        {
            secondClassSet[0] = new String[6] { "SerialName", "Bault", "CheckBit", "StatusBit", "StopBit", "DisplayType" };
            secondClassSet[1] = new String[6] { "RoundEnable", "RoundInterval", "TuiInterval", "CtrlInterval", "DefaultBeginCh", "DefaultEndCh" };
            secondClassSet[2] = new String[1];
            secondClassSet[3] = new String[1];

            secondClassValue[2] = new String[1];
            secondClassValue[3] = new String[1];
            secondClassValue[0] = new String[6] { "COM1", "9600", "None", "8", "One", "Char" };
            secondClassValue[1] = new String[6] { "False", "500", "2", "2", "@", "#" };

            secondClassSet2Pic = new String[1];
            comboBox_serialName.SelectedIndex = 2;//默认com3
            comboBox_Bault.SelectedIndex = 6;//默认波特率9600
            comboBox_checkBit.SelectedIndex = 0;//默认校验位为none
            comboBox_statusBit.SelectedIndex = 0;//默认校验位为8
            comboBox_stopBit.SelectedIndex = 0;//默认停止位为one
        }

        ///加载串口配置
        public void LoadSerialSet()
        {

            //防止读错误，先实例化
            secondClassSet[0] = new String[6] { "SerialName", "Bault", "CheckBit", "StatusBit", "StopBit", "DisplayType" };
            secondClassSet[1] = new String[6] { "RoundEnable", "RoundInterval", "TuiInterval", "CtrlInterval", "DefaultBeginCh", "DefaultEndCh" };
            secondClassSet[2] = new String[1];
            secondClassSet[3] = new String[1];


            secondClassValue[0] = new String[6] { "COM1", "9600", "None", "8", "One", "Char" };
            secondClassValue[1] = new String[6] { "False", "500", "2", "2", "@", "#" };

            secondClassValue[2] = new String[1];
            secondClassValue[3] = new String[1];

            secondClassSet2Pic = new String[1];
            //如果有配置 则读取配置文件

            if (File.Exists(Application.StartupPath + @"\SysSet.txt"))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(Application.StartupPath + @"\SysSet.txt", Encoding.UTF8))//从文件流中读取配置文件
                    {
                        int Index = 0;//记录SecondClassSet[2]和secondClassSet[3]的下标
                        int k = -1;//记录大类的下标
                        bool mark = false;
                        #region 读取配置文件
                        while (!sr.EndOfStream)
                        {
                            String temp = sr.ReadLine();//用来读取一行数据
                            mark = false;//用来标志该行是不是一个大类的标志行
                            //判断该行是不是一个大类
                            for (int i = 0; i < topClassSet.GetLength(0); i++)//注意不要写成topClassSet.Length，这样表示的是维数
                            {
                                String temp2;
                                if (temp.Length >= 3)
                                {
                                    temp2 = temp.Trim().Substring(1, temp.Length - 2);

                                    if (temp2 == topClassSet[i])
                                    {
                                        k = i;
                                        secondClassSet[i] = new String[1];//实例化string
                                        secondClassValue[i] = new String[1];//
                                        mark = true;//这是一个大类，跳过
                                        break;
                                    }
                                }
                            }
                            if (mark)
                            {
                                Index = 0;
                                continue;
                            }
                            if (k == -1)
                            {
                                MessageBox.Show("配置有错！");
                                SetDefault();
                                return;
                            }
                            else
                            {
                                if (temp == null)
                                    continue;
                                if (temp.Split('=').GetLength(0) == 3 && k == 2)
                                {
                                    AddPic(Index, temp.Split('=')[2].Trim());
                                    if (!String.IsNullOrWhiteSpace(temp.Split('=')[0]) && String.IsNullOrWhiteSpace(temp.Split('=')[1]) && String.IsNullOrWhiteSpace(temp.Split('=')[2]))
                                        AddString(k, Index, null, null);
                                    else
                                        AddString(k, Index, temp.Split('=')[0].Trim(), temp.Split('=')[1].Trim());
                                }
                                else if (temp.Split('=').GetLength(0) == 2)
                                {
                                    if (!String.IsNullOrWhiteSpace(temp.Split('=')[0]) && String.IsNullOrWhiteSpace(temp.Split('=')[1]))
                                        AddString(k, Index, null, null);
                                    else
                                        AddString(k, Index, temp.Split('=')[0].Trim(), temp.Split('=')[1].Trim());
                                }
                                else
                                {
                                    AddString(k, Index, temp.Split('=')[0].Trim(), temp.Split('=')[1].Trim());
                                }
                                Index++;
                            }
                        }

                        #endregion

                        comboBox_serialName.SelectedItem = secondClassValue[0][0];//串口号
                        comboBox_Bault.SelectedItem = secondClassValue[0][1];//波特率
                        comboBox_checkBit.SelectedItem = secondClassValue[0][2];//校验位
                        comboBox_statusBit.SelectedItem = secondClassValue[0][3];//数据位
                        comboBox_stopBit.SelectedItem = secondClassValue[0][4];//停止位
                        radioButton_receiveInChar.Checked = (secondClassValue[0][5] == "Char" ? true : false);


                        checkBox_sendRound.Checked = (secondClassValue[1][0] == "True" ? true : false);
                        numericUpDown1.Value = Decimal.Parse(secondClassValue[1][1]);
                        numericUpDown_Interval.Value = Decimal.Parse(secondClassValue[1][2]);
                        numericUpDown_Ctrl.Value = Decimal.Parse(secondClassValue[1][3]);
                        textBox_BeginCh.Text = secondClassValue[1][4];
                        textBox_EndCh.Text = secondClassValue[1][5];

                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("加载配置出错！" + err.Message);
                    SetDefault();
                }


            }
            else/*没有配置文件*/
            {
                SetDefault();
            }
            setChanged = false;
        }

        public static void SaveBufferSet()
        {
            using (StreamWriter sw = new StreamWriter(Application.StartupPath + @"\SysSetTemp.txt", false, Encoding.UTF8))
            {

                for (int i = 0; i < topClassSet.Length; i++)
                {
                    sw.WriteLine("[" + topClassSet[i] + "]");
                    for (int j = 0; j < secondClassSet[i].Length && j < secondClassValue[i].Length; j++)
                    {
                        if (secondClassSet[i][j] == null)
                            continue;
                        else if (i == 2 && j < secondClassSet2Pic.Length && secondClassSet2Pic[j] != null && secondClassSet2Pic[j] != "")
                            sw.WriteLine(secondClassSet[i][j] + " = " + secondClassValue[i][j] + " = " + secondClassSet2Pic[j]);
                        else
                            sw.WriteLine(secondClassSet[i][j] + " = " + secondClassValue[i][j]);
                    }
                }
                sw.Flush();

            }
            if (File.Exists(Application.StartupPath + @"\SysSet.txt"))
                File.Delete(Application.StartupPath + @"\SysSet.txt");
            File.Move(Application.StartupPath + @"\SysSetTemp.txt", Application.StartupPath + @"\SysSet.txt");

            MessageBox.Show("com配置已成功保存在SysSet.txt文件里 下次程序启动后会自动读取配置");
            SaveSystemLog("com配置已成功保存在SysSet.txt文件里");
            Thread.Sleep(100);
            setChanged = false;
        }

        /// <summary>
        /// 在secondClassSet和secondClassValue中给指定的元素以数值
        /// </summary>
        /// <param name="setClass">表示哪个大类</param>
        /// <param name="setIndex">表示数组中的那个元素的下标</param>
        /// <param name="setName">secondClassSet</param>
        /// <param name="setValue">secondClassValue</param>
        public static void AddString(int setClass, int setIndex, String setName, String setValue)
        {
            try
            {
                if (setIndex >= secondClassSet[setClass].Length)//
                {
                    //扩大Value数组
                    String[] temp4 = secondClassValue[setClass];
                    secondClassValue[setClass] = new String[setIndex + 1];
                    temp4.CopyTo(secondClassValue[setClass], 0);

                    //扩大Set数组
                    String[] temp3 = secondClassSet[setClass];
                    secondClassSet[setClass] = new String[setIndex + 1];
                    temp3.CopyTo(secondClassSet[setClass], 0);

                    //赋值
                    secondClassSet[setClass][setIndex] = setName;
                    secondClassValue[setClass][setIndex] = setValue;

                }
                else
                {
                    secondClassSet[setClass][setIndex] = setName;
                    secondClassValue[setClass][setIndex] = setValue;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("error occured in function AddString:" + err.Message);
            }
        }


        /// <summary>
        /// 给tui，加上picture
        /// </summary>
        /// <param name="i">加给参数的下标</param>
        /// <param name="path">图片路径</param>
        public static void AddPic(int i, String path)
        {
            if (i >= secondClassSet2Pic.GetLength(0))
            {
                String[] tempStr = new String[i + 1];
                Array.Copy(secondClassSet2Pic, tempStr, secondClassSet2Pic.GetLength(0));
                secondClassSet2Pic = new String[i + 1];
                Array.Copy(tempStr, secondClassSet2Pic, tempStr.GetLength(0) - 1);

                secondClassSet2Pic[i] = path;
            }
            else
            {
                secondClassSet2Pic[i] = path;
            }
        }

        //设置微博的起始标志和终结标志
        public void CommunicationSet()
        {
            if (textBox_BeginCh.Text == "")
            {
                textBox_BeginCh.Text = "@";
            }
            if (textBox_EndCh.Text == "")
            {
                textBox_EndCh.Text = "#";
            }
            beginCh = textBox_BeginCh.Text.ToCharArray()[0];
            endCh = textBox_EndCh.Text.ToCharArray()[0];
        }

        #endregion

        #region 串口、微博信息提取
        /// <summary>
        ///查找串口信息中的命令关键字 
        /// </summary>
        /// <param name="str">信息字符串</param>
        /// <returns></returns>
        public static String FindSetName(String str)
        {
            return str.Split(',')[0].Trim();
        }

        /// <summary>
        /// 查找微博参数出现的位置以及次数
        /// </summary>
        /// <param name="str">查找字符串</param>>
        /// <param name="beginIndex">用来存储该参数字符串所处的首位置</param>
        /// <param name="endIndex">用来存储该参数字符串所处的末位置</param>
        /// <returns>返回查找到的参数个数</returns>
        public static int FindArgsInWB(String str, ref int[] beginIndex, ref int[] endIndex)
        {
            int maxI = -1;//最大的i

            beginIndex = new int[1];//先初始化
            endIndex = new int[1];

            beginIndex[0] = -1;//首先初始化第一个元素
            endIndex[0] = -1;
            foreach (String a in str.Split('{'))
            {
                try
                {
                    if (a.IndexOf('}') != -1)
                    {
                        int i = -1;
                        if (int.TryParse(a.Split('}')[0].ToString(), out i))
                        {
                            if (i > maxI)//如果比目前为止最大的下标还大，扩大数组
                            {
                                int[] tempBeginIndex = new int[i + 1];
                                int[] tempEndIndex = new int[i + 1];
                                
                                Array.Copy(beginIndex, tempBeginIndex, beginIndex.Length);
                                Array.Copy(endIndex, tempEndIndex, endIndex.Length);

                                for (int j = maxI + 1; j < i; j++)
                                {
                                    tempBeginIndex[j] = -1;//初始化为-1
                                    tempEndIndex[j] = -1;
                                }

                                beginIndex = new int[i + 1];
                                endIndex = new int[i + 1];

                                Array.Copy(tempBeginIndex, beginIndex, tempBeginIndex.Length);
                                Array.Copy(tempEndIndex, endIndex, tempEndIndex.Length);

                            }
                            if (i > maxI)
                                maxI = i;

                            String strTemp = "{" + i + "}";
                            beginIndex[i] = str.IndexOf(strTemp);
                            endIndex[i] = str.IndexOf(strTemp) + strTemp.Length;
                        }
                        else
                        {
                            maxI = -1;
                            beginIndex = null;
                            endIndex = null;
                            MessageBox.Show("发送的微博有语法错误：{}里面应该是数值");
                        }
                    }
                
                }
                catch (Exception err)
                {
                    maxI = -1;
                    beginIndex = null;
                    endIndex = null;
                    MessageBox.Show("发送的微博有语法错误：" + err.Message);
                }
                
            }
            return maxI + 1;
        }

        /// <summary>
        /// 查找命令参数的个数以及内容
        /// </summary>
        /// <param name="str">查找字符串</param>
        /// <param name="argsValue">用来存储参数的字符串数组</param>
        /// <returns>参数个数</returns>
        public static int FindArgsInCmd(String str, ref String[] argsValue)
        {

            argsValue = new String[1] { "" };

            int count = -1;//第一个是命令参数，不读
            foreach (String a in str.Split(','))
            {
                if (count == 0)
                    argsValue[count] = a.Trim();
                else if (count > 0)
                {
                    String[] tempArgs = new String[count + 1];
                    Array.Copy(argsValue, tempArgs, argsValue.Length);

                    argsValue = new String[count + 1];
                    Array.Copy(tempArgs, argsValue, tempArgs.Length);
                    argsValue[count] = a.Trim();
                }
                count++;
            }

            return count;
        }

        /// <summary>
        /// 将命令转换成WB赋值给WBMessage,如果带参数转换成功，返回true，否则返回false，同时WBMessage = ""
        /// </summary>
        /// <param name="WBMessage">用来存储转换后的微博</param>
        /// <param name="command">输入串口来的命令</param>
        /// <param name="setValue">输入串口来的命令对应的转换法则（secondClassValue[2]）</param>
        /// <returns>成功返回true</returns>
        public static bool ConvertMessage(ref  String WBMessage, String command, String setValue)
        {
            try
            {
                String[] argsValue = new String[1];
                int[] beginIndex = new int[1];
                int[] endIndex = new int[1];

                int argsInCmdCount = 0;
                int argsInWBCount = 0;
                if ((argsInWBCount = FindArgsInWB(setValue, ref  beginIndex, ref endIndex)) > 0)//wb里面有参数
                {
                    WBMessage = "";
                    argsInCmdCount = FindArgsInCmd(command, ref argsValue);
                    bool startIndex = true;
                    int lastEndIndex = 0;
                    for (int i = 0; i < argsInWBCount; i++)
                    {
                        if (beginIndex[i] >= 0 && endIndex[i] >= 0)//如果第i个参数能够索引到setValue
                        {
                            //参数前面的+参数+参数后面的（如果是末尾）
                            if (startIndex)
                            {
                                lastEndIndex = 0;
                                startIndex = false;
                            }
                            
                            ////设置参数以前的字符串
                            WBMessage += setValue.Substring(lastEndIndex, beginIndex[i] - lastEndIndex);
                            
                            lastEndIndex = endIndex[i];
                            //添加argus
                            if (i < argsInCmdCount && argsValue[i] != "")
                                WBMessage += argsValue[i];
                            else
                                WBMessage += "不告诉你";

                            if (i == beginIndex.Length - 1)//到了末尾，还要加上末尾的字符
                            {
                                WBMessage += setValue.Substring(endIndex[i], setValue.Length - endIndex[i]);
                            }

                        }
                    }

                }
                else//wb里面没参数,直接存命令
                {
                    WBMessage = "";
                    return false;
                }
                return true;
                
            }
            catch (Exception err)
            {
                MessageBox.Show("error occured in Function ConverMessage:" + err.Message);
                return false;
            }
                
        }

        /*
 * 背景：
 *     到底怎么tui的？
 *     某个人类点击了开启tui，然后就激活了timer_Tui这个神，
 * 
 * timer_Tui神干啥的？
 * 
 *     timer_Tui每隔T时间（人类预先设定的）干一次活,
 *     每次干活：
 *     timer_Tui神首先给一个权杖（findingTui == true）给苦工FindTuiInfo（）
 * 苦工我有了findingTui权杖后，就可以从DataReceived（）大神里面拿数据来干活。
 * （没有这个findingTui权杖，我是没有权利从DataReceived大神手里拿数据的。
 *     
 *     同时timer_Tui神用timer_wait计时器来给FindTuiInfo干活计时，十秒钟之后
 * timer_Tui神收回权杖，停止计时；如果10s钟之内FindTuiInfo干完了活，就会告诉timer_Tui神，
 * 我成功的找到了(findedTui = true)！否则，timer_Tui给出结果findedTui = false，并且计上一过（failed = true）
 * 然后timer_Tui神清空这次使用过的东西，走人，下次再来。
 *    
 * 
 * 附：DataReceive（串口管理大神）功能：负责从串口来的数据的分配，首先会将数据显示在屏幕上公示，
 * 然后谁有权杖就可以把数据给他用
 * 
 * 我的功能：
 * timer_wait给我指定了查找的时间，10秒，10秒钟之内，如果我还没返还数据给他说我找到了，他就\
 * 会残忍的给我发个信号：failed，于是我就只能停止查找了。。。。
 * 
 * 我会进行连续的查找，如果这次没找到，下次再我会继续努力，继续找，
 * 如果10秒钟之内，我找到了，哈哈，我就会返回给timer_SerialToTui_Interval
 * (其实就是timer_wait的领导，ps.timer_wait能力有限，只能发出警告（定时的），他不能接受其他信息)
 * 一个信号（findedTui == true），告诉他我找到了
 * 然后
 * 
 * 需要告诉我的信息（就是给我进行初始化）：
 * 1.是否找过，然后失败了（failed）？
 * 2.是否找到了（findedTui）？
 */
        /// <summary>
        /// 查找串口数据里面的信息
        /// </summary>
        /// <param name="dataReceive">串口来的数据</param>
        /// <param name="bytesToRead">串口来的数据的大小</param>
        public void FindTuiInfo(byte[] dataReceive, int bytesToRead)
        {
            for (int i = 0; i < bytesToRead; i++)
            {
                if (failed)//timer_Tui神告诉我，你查找失败，这次不用找了。
                {
                    count = 0;
                    return;
                }
                if (findedTui)//有时候，我告诉了timer_Tui神，但是它还没来得及处理
                {
                    return;
                }
                //好的，现在看样子我还没到点，也没找到，继续吧
                if ((char)dataReceive[i] == beginCh)
                {
                    findedTuiSign = true;//找到了起始点，重新初始化
                    tuiInfo = null;
                    tuiInfo = new char[140];
                    count = 0;
                    continue;
                }
                if (!findedTuiSign)//如果还没找到tuiInfo的起始位，继续找
                    continue;

                //如果还能运行到这里，说明1.我还有时间（timer_wait还没到点）3.找到了tuiInfo的起始位，所以可以收数据
                if (count == 139)//到底了,一切从头再来
                {
                    findedTuiSign = false;
                    tuiInfo = null;
                    tuiInfo = new char[140];
                    count = 0;
                    continue;
                }
                else if ((char)dataReceive[i] == endCh)//哟，找到了
                {
                    if (count == 0)//但是找到的是空字符。。。。。
                    {
                        findedTuiSign = false;
                        continue;
                    }
                    else
                    {
                        findedTuiSign = false;//先自动清零，以防止下次我找的时候把数据直接存了
                        findedTui = true;//哈哈，我找到了！！（给timer_Tui神汇报呢）
                        return;
                    }
                }
                else//可以存储了
                {
                    tuiInfo[count] = (char)dataReceive[i];
                    count++;
                }
            }

        }



        #endregion

        #region 数据交互

        /// <summary>
        /// 向串口发送命令
        /// </summary>
        /// <param name="dataToSend">命令字符串</param>
        /// <returns></returns>
        public bool SendCmd(String dataToSend)
        {
            if (dataToSend == "")//如果字符为空
            {
                return false;
            }
            else
            {
                serialPort1.Write(dataToSend);
                return true;
            }
        }


        /// <summary>
        /// 获取从串口来的数据，并且将数据存到指定的参数里面
        /// </summary>
        /// <param name="dataReceive">存储来自串口的字节数据</param>
        /// <param name="text">存储字节数据转化成的字符串数据</param>
        /// <returns>返回收到的数据大小</returns>
        private int ReceiveData(ref byte[] dataReceive, ref string text)//没有ref传递的只是类型
        {
            string thText = string.Empty;
            int bytesToRead = serialPort1.BytesToRead;//由于BytesToRead在变，所以先用其他变量存储一下
            byte[] result = new byte[bytesToRead];
            serialPort1.Read(result, 0, bytesToRead);

            if (radioButton_receiveInHex.Checked == true)//如果是以16进制显示
            {
                foreach (byte b in result)
                {
                    /*这里是我修改了的，可以接受每个字符，以空格分开*/
                    thText = thText + Convert.ToString(b, 16) + " ";//转换成16进制数
                }
            }
            else//如果是以字符形式显示
            {
                foreach (byte b in result)
                {
                    /*这里是我修改了的，可以接受每个字符，以空格分开*/
                    thText = thText + (char)b;//转换成字符演示
                }
            }
            dataReceive = new byte[bytesToRead];
            result.CopyTo(dataReceive, 0);
            text = String.Copy(thText);
            return bytesToRead;
        }

        /// <summary>
        /// 处理来自串口数据的函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                //公布数据在屏幕上
                string text = string.Empty;
                byte[] dataReceive = { };
                int bytesToRead = ReceiveData(ref dataReceive, ref text);

                textBox_receiveBuffer.AppendText(text);
                textBox_receiveBuffer.ScrollToCaret();//自动显示后插入的文字

                //判断是否把数据给FindTuiInfo用
                if (findingTui)
                {
                    FindTuiInfo(dataReceive, bytesToRead);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("error occured in serialPort1_DataReceived part:" + err.Message);
            }
        }

        #endregion
        //显示状态
        private void ShowState(int state)
        {
            if (state == showNothing)
                tssl01.Text = "";
            if (state == showTuiState)
                tssl01.Text = tuiState;
            if (state == showCtrlState + showTuiState)
                tssl01.Text = tuiState + ctrlState;

            SaveSystemLog(tssl01.Text);//保存到系统日志
            Thread.Sleep(100);
        }

        #region 微博部分
        /// <summary>
        /// 线程方法：发微博，无返回值
        /// </summary>
        /// <param name="status">发微博字符串</param>
        /// <param name="imgBuff">发微博顺带的图片数据</param>>
        private void PublishStatusThread(string status, byte[] imgBuff)
        {
            btnPublish.Enabled = false;
            btnPublish.Text = "请稍等..";
            txtStatusBody.ReadOnly = true;

            Thread t = new Thread(new ThreadStart(delegate()
            {
                if (imgBuff == null)
                {
                    try
                    {
                        Sina.API.Entity.Statuses.Update(status, 0, 0, null);
                        SaveSystemLog("不带图片微博发布成功！ ");
                        Thread.Sleep(100);
                        SaveWBLog("图片：无；文字：" + status);
                    }
                    catch (Exception ex)
                    {
                        this.UIInvoke(() =>
                        {
                            MessageBox.Show(this, ex.Message, "哟？！出错了!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        });
                        SaveSystemLog("不带图片微博发布失败！ " + ex.Message);
                        Thread.Sleep(100);
                    }
                }
                else
                {
                    try
                    {
                        Sina.API.Entity.Statuses.Upload(status, imgBuff);
                        SaveSystemLog("带图片微博发布成功！ ");
                        Thread.Sleep(100);
                        SaveWBLog("图片：有；文字：" + status);
                    }
                    catch (Exception ex)
                    {
                        this.UIInvoke(() =>
                        {
                            MessageBox.Show(this, ex.Message, "哟？！出错了!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        });

                        SaveSystemLog("带图片微博发布失败！ " + ex.Message);
                        Thread.Sleep(100);
                    }
                    finally
                    {
                        imgBuff = null;
                    }
                }

                this.UIInvoke(() =>
                {
                    btnPublish.Enabled = true;
                    btnPublish.Text = "发布微博";
                    txtStatusBody.ReadOnly = false;
                    txtStatusBody.Text = string.Empty;
                    btnInsertPicture.Enabled = true;
                    LoadFriendTimeline();
                });

            }));

            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// 非线程方法：发微博，有返回值，这个是专门给串口发微博用的
        /// </summary>
        /// <param name="status">发送微博内容</param>
        /// <returns></returns>
        private bool PublishStatusNThread(String status)
        {

            btnPublish.Enabled = false;
            btnPublish.Text = "请稍等..";
            txtStatusBody.ReadOnly = true;
            bool success = false;//用来表示是否成功

            String tempSet;
            tempSet = FindSetName(status);
            String tempValue = "";
            String tempWB = "";

            String errMessage = "";
            if (tempSet != "")
            {
                
                try
                {
                 
                    for (int i = 0; i < secondClassSet[2].Length; i++)
                    {
                        if (!String.IsNullOrWhiteSpace(secondClassSet[2][i]) && secondClassSet[2][i] == tempSet)
                        {
                            if (String.IsNullOrWhiteSpace(secondClassValue[2][i]) &&i < secondClassSet2Pic.Length && File.Exists(secondClassSet2Pic[i]))
                            {
                                Sina.API.Entity.Statuses.Upload("HelloWorld:",ImageDatabytes(secondClassSet2Pic[i]) );
                                SaveWBLog("只有图片的微博哟!" + "图片路径：" + secondClassSet2Pic[i] + " 原始命令：" + status);
                            }
                            else if (!String.IsNullOrWhiteSpace(secondClassValue[2][i]))
                            {
                                tempValue = secondClassValue[2][i];
                                if (!ConvertMessage(ref tempWB, status, tempValue))
                                    tempWB = tempValue;
                                if (i <secondClassSet2Pic.Length &&secondClassSet2Pic[i] != null && secondClassSet2Pic[i] != "" && File.Exists(secondClassSet2Pic[i]))
                                {
                                    Sina.API.Entity.Statuses.Upload(tempWB,ImageDatabytes(secondClassSet2Pic[i]));
                                    SaveWBLog("图片路径：" + secondClassSet2Pic[i] + " 原始命令：" + status + "转换格式" + tempValue + " 转换后的微博：" + tempWB);
                                }
                                else
                                {
                                    Sina.API.Entity.Statuses.Update(tempWB, 0, 0, null);
                                    SaveWBLog("图片：无；原始命令：" + status + "转换格式：" + tempValue + "转换后的微博：" + tempWB);
                                }
                            }

                            success = true;
                            SaveSystemLog("微博发布成功！ ");
                            Thread.Sleep(100);
                            break;
                        }
                    }
                    errMessage = "命令没找到！";
                
                }
                catch (Exception ex)
                {
                    success = false;
                    errMessage = ex.Message;
                }
                 
                
            }
            else
                errMessage = "命令为空！";

            if (!success)
            {
                SaveSystemLog("微博发布失败！ " + errMessage);
                Thread.Sleep(100);
            }

            this.UIInvoke(() =>
            {
                btnPublish.Enabled = true;
                btnPublish.Text = "发布微博";
                txtStatusBody.ReadOnly = false;
                txtStatusBody.Text = string.Empty;
                btnInsertPicture.Enabled = true;
                if (loginState && success)//如果是登录状态
                    LoadFriendTimeline();
            });
            return success;

        }

        /// 线程方法：加载用户信息
        private void LoadUserInformation()
        {
            Thread thUserInfo = new Thread(new ThreadStart(delegate()
            {
                try
                {
                    string userID = Sina.API.Entity.Account.GetUID();
                    NetDimension.Weibo.Entities.user.Entity userInfo = Sina.API.Entity.Users.Show(userID);//获取用户信息
                    //UIUpdateUserInfo(userInfo); //2.0才需要这个东西

                    this.UIInvoke(() =>
                    {
                        lblScreenName.Text = userInfo.ScreenName;
                        lblDesc.Text = userInfo.Description;
                        imgProfile.ImageLocation = userInfo.ProfileImageUrl;
                        lblUserStatus.Text = string.Format("关注({0}) 粉丝({1}) 微博({2})", userInfo.FriendsCount, userInfo.FollowersCount, userInfo.StatusesCount);
                    });
                }
                catch (Exception err)
                {
                    SaveSystemLog("加载用户信息时出错！"+ err.Message);
                }
            }));


            thUserInfo.IsBackground = true;
            thUserInfo.Start();
        }

        /// 线程方法：加载用户及用户最新发表的微博
        private void LoadFriendTimeline()
        {
            Thread thLoad = new Thread(new ThreadStart(delegate()
            {
                try
                {
                    StringBuilder statusBuilder = new StringBuilder();
                    var json = Sina.API.Entity.Statuses.FriendsTimeline(count: 20);
                    if (json.Statuses != null)
                    {
                        foreach (var status in json.Statuses)
                        {
                            if (status.User == null)
                                continue;

                            if (status.RetweetedStatus != null && status.RetweetedStatus.User != null)
                            {
                                statusBuilder.AppendFormat(repostPattern,
                                    status.User.ProfileImageUrl,
                                    status.User.ScreenName,
                                    status.Text,
                                    status.RetweetedStatus.User.ScreenName,
                                    status.RetweetedStatus.Text,
                                    string.IsNullOrEmpty(status.RetweetedStatus.ThumbnailPictureUrl) ? "" :
                                    string.Format(imageParttern, status.RetweetedStatus.ThumbnailPictureUrl),
                                    status.RetweetedStatus.RepostsCount,
                                    status.RetweetedStatus.CommentsCount,
                                    status.RepostsCount, status.CommentsCount);

                            }
                            else
                            {
                                statusBuilder.AppendFormat(statusPattern,
                                    status.User.ProfileImageUrl,
                                    status.User.ScreenName,
                                    status.Text,
                                    string.IsNullOrEmpty(status.ThumbnailPictureUrl) ? "" :
                                    string.Format(imageParttern, status.ThumbnailPictureUrl),
                                    status.RepostsCount, status.CommentsCount);
                            }

                        }
                    }

                    var html = htmlPattern.Replace("<!--StatusesList-->", statusBuilder.ToString());

                    //对比2.0，这里是不需要单独有个UI操作的方法的。
                    this.UIInvoke(() =>
                    {
                        wbStatuses.DocumentText = html;
                    });
                }
                catch (Exception err)
                {
                    SaveSystemLog("加载用户及用户最新发表的微博时出错!" + err.Message);
                }
            }));

            thLoad.IsBackground = true;
            thLoad.Start();

        }

        //线程方法：加载用户最新微博
        //
        // 摘要:
        //     获取某个用户最新发表的微博列表
        //
        // 参数:
        //   uid:
        //     需要查询的用户ID。
        //
        //   screenName:
        //     需要查询的用户昵称。
        //
        //   sinceID:
        //     若指定此参数，则返回ID比since_id大的微博（即比since_id时间晚的微博），默认为0。
        //
        //   maxID:
        //     若指定此参数，则返回ID小于或等于max_id的微博，默认为0。
        //
        //   count:
        //     单页返回的记录条数，默认为50。
        //
        //   page:
        //     返回结果的页码，默认为1。
        //
        //   baseApp:
        //     是否只获取当前应用的数据。0为否（所有数据），1为是（仅当前应用），默认为0。
        //
        //   feature:
        //     过滤类型ID，0：全部、1：原创、2：图片、3：视频、4：音乐，默认为0。
        //
        //   trimUser:
        //     回值中user信息开关，0：返回完整的user信息、1：user字段仅返回user_id，默认为0。
        //public NetDimension.Weibo.Entities.status.Collection UserTimeline(string uid = "", string screenName = "", string sinceID = "", string maxID = "", int count = 50, int page = 1, bool baseApp = false, int feature = 0, bool trimUser = false);
        /// <summary>
        /// 加载用户微博
        /// </summary>
        private void LoadUserTimeline()
        {
            Thread thLoadUser = new Thread(new ThreadStart(delegate()
            {
                //int i = 0;
                try
                {
                    string userID = Sina.API.Entity.Account.GetUID();


                    //textBox_receiveBuffer.Text = userID + "\n";//dddd

                    StringBuilder statusBuilder = new StringBuilder();
                    var json = Sina.API.Entity.Statuses.UserTimeline(uid: userID, count: 20);
                    if (json.Statuses != null)
                    {
                        foreach (var status in json.Statuses)
                        {
                            if (status.User == null)
                                continue;
                            //i++;//
                            // textBox_receiveBuffer.Text += (i + ":" + status.ID.Trim() + "  " + status.Text.Trim() + "  ");//

                            if (status.RetweetedStatus != null && status.RetweetedStatus.User != null)
                            {
                                statusBuilder.AppendFormat(repostPattern,
                                    status.User.ProfileImageUrl,
                                    status.User.ScreenName,
                                    status.Text,
                                    status.RetweetedStatus.User.ScreenName,
                                    status.RetweetedStatus.Text,
                                    string.IsNullOrEmpty(status.RetweetedStatus.ThumbnailPictureUrl) ? "" :
                                    string.Format(imageParttern, status.RetweetedStatus.ThumbnailPictureUrl),
                                    status.RetweetedStatus.RepostsCount,
                                    status.RetweetedStatus.CommentsCount,
                                    status.RepostsCount, status.CommentsCount);


                            }
                            else
                            {
                                statusBuilder.AppendFormat(statusPattern,
                                    status.User.ProfileImageUrl,
                                    status.User.ScreenName,
                                    status.Text,
                                    string.IsNullOrEmpty(status.ThumbnailPictureUrl) ? "" :
                                    string.Format(imageParttern, status.ThumbnailPictureUrl),
                                    status.RepostsCount, status.CommentsCount);
                            }

                        }
                    }

                    var html = htmlPattern.Replace("<!--StatusesList-->", statusBuilder.ToString());

                    //对比2.0，这里是不需要单独有个UI操作的方法的。
                    this.UIInvoke(() =>
                    {
                        wbStatuses.DocumentText = html;
                    });
                }
                catch (Exception err)
                {
                    SaveSystemLog("加载用户微博时出错！" + err.Message);
                }
            }));

            thLoadUser.IsBackground = true;
            thLoadUser.Start();
        }

        /// <summary>
        /// 加载图片到缓冲区
        /// </summary>
        /// <param name="FilePath">图片路径</param>
        /// <returns>返回图片字节数据</returns>
        public static byte[] ImageDatabytes(string FilePath)
        {
            if (!File.Exists(FilePath))
                return null;
            Bitmap myBitmap = new Bitmap(Image.FromFile(FilePath));

            using (MemoryStream curImageStream = new MemoryStream())
            {
                myBitmap.Save(curImageStream, System.Drawing.Imaging.ImageFormat.Png);
                curImageStream.Flush();

                byte[] bmpBytes = curImageStream.ToArray();
                //如果转字符串的话
                //string BmpStr = Convert.ToBase64String(bmpBytes);
                return bmpBytes;
            }
        }
        #endregion

    }
}