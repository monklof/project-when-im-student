using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.IO.Ports;
using Microsoft.VisualBasic;
using NetDimension.Weibo;
namespace HelloWorld
{
    //本部分为系统启动界面、响应系统界面的事件部分
    public partial class frmHelloWorld : Form
    {


        public frmHelloWorld()//声明一个与类同名的字段函数(其实就是实例化类)
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (setChanged && MessageBox.Show("保存配置吗？", "保存配置", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {

                    /*
                    secondClassSet[0] = new String[6] { "SerialName", "Bault", "CheckBit", "StatusBit", "StopBit", "DisplayType" };
                    secondClassSet[1] = new String[6] { "RoundEnable", "RoundInterval", "TuiInterval", "CtrlInterval", "DefaultBeginCh", "DefaultEndCh" };
                    secondClassSet[2] = new String[6];
                    secondClassSet[3] = new String[6];


                    secondClassValue[0] = new String[6] { "COM1", "9600", "None", "8", "One", "Char" };
                    secondClassValue[1] = new String[6] { "False", "500", "2", "2", "@", "#" };
                    */

                    AddString(0, 0, "SerialName", comboBox_serialName.SelectedItem.ToString());
                    AddString(0, 1, "Bault", comboBox_Bault.SelectedItem.ToString());
                    AddString(0, 2, "CheckBit", comboBox_checkBit.SelectedItem.ToString());
                    AddString(0, 3, "StatusBit", comboBox_statusBit.SelectedItem.ToString());
                    AddString(0, 4, "StopBit", comboBox_stopBit.SelectedItem.ToString());
                    AddString(0, 5, "DisplayType", (radioButton_receiveInChar.Checked == true ? "Char" : "Hex"));

                    AddString(1, 0, "RoundEnable", (checkBox_sendRound.Checked == true ? "True" : "False"));
                    AddString(1, 1, "RoundInterval", numericUpDown1.Value.ToString());
                    AddString(1, 2, "TuiInterval", numericUpDown_Interval.Value.ToString());
                    AddString(1, 3, "CtrlInterval", numericUpDown_Ctrl.Value.ToString());
                    AddString(1, 4, "DefaultBiginCh", textBox_BeginCh.Text);
                    AddString(1, 5, "DefaultEndCh", textBox_EndCh.Text);

                    SaveBufferSet();
                }
                catch (Exception err)
                {
                    MessageBox.Show("保存出错！自动恢复为上次的文件！" + err.Message);
                    if (File.Exists(Application.StartupPath + @"\SysSetTemp.txt"))
                        File.Delete(Application.StartupPath + @"\SysSetTemp.txt");
                }
            }
            base.OnClosed(e);
        }
        #region 微博列表的模板
        const string htmlPattern = @"<!DOCTYPE html>
<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
<head>
	<title></title>
	<style type=""text/css"">
		html, body {
			font-size: 12px;
			cursor: default;
			padding: 5px;
			margin: 0;
			font-family:微软雅黑,Tahoma;
		}

		div.status {
			padding-left: 60px;
			position: relative;
			margin-bottom: 10px;
			min-height:80px;
			_height:80px;
		}

			div.status p {
				margin: 0 0 5px 0;
				line-height: 1.5;
				padding: 0;
			}

				div.status p span.name {
					color: #369;
				}

				div.status p.status-content {
					color: #333;
				}

				div.status p.status-count {
					color:#999;
				}

			div.status .face {
				position: absolute;
				left: 0;
				top: 0;
			}

			div.status div.repost {
				border: solid 1px #ACD;
				background: #F0FAFF;
				padding: 10px 10px 0 10px;
			}

		div.repost p.repost-content {
			color: #666 !important;
		}
	</style>
</head>
<body>
<!--StatusesList-->
</body>
</html>";
        const string imageParttern = @"<img src=""{0}"" alt=""图片"" class=""inner-pic"" />";
        const string statusPattern = @"	<div class=""status"">
		<img src=""{0}"" alt=""{1}"" class=""face"" />
		<p class=""status-content""><span class=""name"">{1}</span>：{2}</p>
		{3}
		<p class=""status-count"">转发({4}) 评论({5})</p>
	</div>
";
        const string repostPattern = @"	<div class=""status"">
		<img src=""{0}"" alt=""{1}"" class=""face"" />
		<p class=""status-content""><span class=""name"">{1}</span>：{2}</p>
		<div class=""repost"">
			<p class=""repost-cotent""><span class=""name"">@{3}</span>：{4}</p>
			{5}
			<p class=""status-count"">转发({6}) 评论({7})</p>
		</div>
		<p class=""status-count"">转发({8}) 评论({9})</p>
	</div>
";
        #endregion

        #region 公共部分
        private OAuth oauth;//新建一个授权类
        private Client Sina;//新建一个微博操作类
        private string UserID = string.Empty;

        //用来运行传递OAuth的情形
        public frmHelloWorld(NetDimension.Weibo.OAuth oauth)//重载与类同名的字段函数
        {
            InitializeComponent();
            this.oauth = oauth;//复制oauth
            Sina = new NetDimension.Weibo.Client(oauth);//实例化Sina
        }

        private System.Threading.Timer timer_Wait;

        private System.Threading.Timer timer_Tui;

        private System.Threading.Timer timer_Ctrl;
        //初始化界面
        private void HelloWorld_Load(object sender, EventArgs e)
        {

            LoadSerialSet();//加载串口配置信息
            if (loginState)
            {
                LoadUserInformation();//加载用户信息
                LoadFriendTimeline();//加载好友信息
                //LoadUserTimeline();//加载用户微博信息
            }
            frmHelloWorld.CheckForIllegalCrossThreadCalls = false;
        }

        #endregion

        #region 微博部分按钮

        //发微博按钮
        private void btnPublish_Click(object sender, EventArgs e)
        {

            if (txtStatusBody.TextLength == 0)
            {
                MessageBox.Show(this, "说点什么新鲜事儿呗。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string status = txtStatusBody.Text;
            PublishStatusThread(status, imgBuffInsert);
        }

        //刷新按钮
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            LoadUserInformation();//加载用户信息
            LoadFriendTimeline();//加载好友信息
        }

        //微博加载图片按钮
        private void btnInsertPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    using (FileStream stream = imageFile.OpenRead())
                    //imageFile.FullName获取文件路径
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        try
                        {
                            stream.Seek(0, SeekOrigin.Begin);
                            imgBuffInsert = reader.ReadBytes((int)stream.Length);
                        }
                        finally
                        {
                            reader.Close();
                            stream.Close();
                        }
                    }
                    btnInsertPicture.Enabled = false;

                    MessageBox.Show(this, "图片已附加，发条微博看看吧～", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        ///计剩下微博字数
        private void txtStatusBody_TextChanged(object sender, EventArgs e)
        {
            int remainCount = 140 - txtStatusBody.TextLength;
            lblCharCount.Text = string.Format("剩下{0}字", remainCount);
        }

        #endregion

        #region 串口部分按钮

        //清空接收缓冲区
        private void button_clearReceive_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.DiscardInBuffer();
                textBox_receiveBuffer.Text = "";
            }
        }
        // 清空发送缓冲区
        private void button_clearSend_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.DiscardOutBuffer();
                textBox_sendContent.Text = "";
            }
        }

        private void textBox_sendContent_TextChanged(object sender, EventArgs e)
        {

        }

        // 作出打开还是关闭串口反应
        private void button_serialOpen_Click(object sender, EventArgs e)
        {

            if (button_serialOpen.Text == "打开串口")//如果人类是要打开串口大门
            {
                try
                {

                    #region 设置串口相关配置

                    serialPort1.PortName = comboBox_serialName.SelectedItem.ToString();//设置串口名称
                    serialPort1.BaudRate = Convert.ToInt32(comboBox_Bault.SelectedItem.ToString());//设置波特率
                    switch (comboBox_checkBit.SelectedIndex)//设置校验位
                    {
                        case 0:
                            serialPort1.Parity = Parity.None;//0
                            break;
                        case 1:
                            serialPort1.Parity = Parity.Odd;//1
                            break;
                        case 2:
                            serialPort1.Parity = Parity.Even;//2
                            break;
                        case 3:
                            serialPort1.Parity = Parity.Mark;//3
                            break;
                        case 4:
                            serialPort1.Parity = Parity.Space;//4
                            break;
                    }

                    serialPort1.DataBits = Convert.ToInt32(comboBox_statusBit.SelectedItem.ToString());//设置数据位

                    switch (comboBox_stopBit.SelectedIndex)//设置停止位
                    {
                        case 0:
                            serialPort1.StopBits = StopBits.One;
                            break;
                        case 1:
                            serialPort1.StopBits = StopBits.Two;
                            break;
                        case 2:
                            serialPort1.StopBits = StopBits.OnePointFive;
                            break;
                    }

                    #endregion

                    //打开串口
                    serialPort1.Open();

                    //设置状态栏颜色
                    panel_state.BackColor = Color.LawnGreen;

                    Lock_OpenCom(true);//锁定串口配置

                    button_serialOpen.Text = "关闭串口";
                }
                catch (Exception)
                {
                    MessageBox.Show("串口打开失败" + "\n" + "1.请检查配置的参数是否正确" + "\n" + "2.外围设备是否没有连接" + "\n" + "3.串口是否已经打开或被占用" + "\n" + "4.串口驱动是否没有安装");
                }
            }
            else//如果人类是要关闭串口大门
            {
                //关闭串口
                serialPort1.Close();

                panel_state.BackColor = Color.Red;

                Lock_OpenCom(false);//解除锁定串口配置

                button_serialOpen.Text = "打开串口";
            }
        }

        //保存串口配置
        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {

                /*
                secondClassSet[0] = new String[6] { "SerialName", "Bault", "CheckBit", "StatusBit", "StopBit", "DisplayType" };
                secondClassSet[1] = new String[6] { "RoundEnable", "RoundInterval", "TuiInterval", "CtrlInterval", "DefaultBeginCh", "DefaultEndCh" };
                secondClassSet[2] = new String[6];
                secondClassSet[3] = new String[6];


                secondClassValue[0] = new String[6] { "COM1", "9600", "None", "8", "One", "Char" };
                secondClassValue[1] = new String[6] { "False", "500", "2", "2", "@", "#" };
                */

                AddString(0, 0, "SerialName", comboBox_serialName.SelectedItem.ToString());
                AddString(0, 1, "Bault", comboBox_Bault.SelectedItem.ToString());
                AddString(0, 2, "CheckBit", comboBox_checkBit.SelectedItem.ToString());
                AddString(0, 3, "StatusBit", comboBox_statusBit.SelectedItem.ToString());
                AddString(0, 4, "StopBit", comboBox_stopBit.SelectedItem.ToString());
                AddString(0, 5, "DisplayType", (radioButton_receiveInChar.Checked == true ? "Char" : "Hex"));

                AddString(1, 0, "RoundEnable", (checkBox_sendRound.Checked == true ? "True" : "False"));
                AddString(1, 1, "RoundInterval", numericUpDown1.Value.ToString());
                AddString(1, 2, "TuiInterval", numericUpDown_Interval.Value.ToString());
                AddString(1, 3, "CtrlInterval", numericUpDown_Ctrl.Value.ToString());
                AddString(1, 4, "DefaultBiginCh", textBox_BeginCh.Text);
                AddString(1, 5, "DefaultEndCh", textBox_EndCh.Text);

                SaveBufferSet();
            }
            catch (Exception err)
            {
                MessageBox.Show("保存出错！配置文件自动恢复为上一次的配置！" + err.Message);
                if (File.Exists(Application.StartupPath + @"\SysSetTemp.txt"))
                    File.Delete(Application.StartupPath + @"\SysSetTemp.txt");
            }
        }

        // 开始发送数据按钮
        private void button_send_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)//如果串口是开的
            {

                if (button_send.Text == "开始发送")//如果按钮是开始发送
                {
                    if (checkBox_sendRound.Checked == true)//如果是循环发送
                    {

                        Lock_BeginSend(true);/*锁定相关按键*/

                        roundSendingOn = true;//给timer1大神权杖
                        timer1.Interval = Convert.ToInt32(numericUpDown1.Value);//先给timer1大神一个工作时间间隔
                        timer1.Enabled = true;//好，现在把东西交给timer1大神处理

                        button_send.Text = "停止发送";
                    }
                    else//否则直接叫SerialSend小工给我做事就行了
                    {

                        if (SendCmd(textBox_sendContent.Text) == false)
                            MessageBox.Show("能不能别这么二，把发送数据一栏填好好么？");
                        //这里没必要设为“停止发送”
                    }

                }
                else//如果按钮是停止发送
                {
                    roundSendingOn = false;//回收给timer1的权杖
                    timer1.Enabled = false;//现在，不让你干活了

                    Lock_BeginSend(false);/*解除锁定相关配置*/
                    button_send.Text = "开始发送";
                }
            }
            else//如果串口是关的
            {
                MessageBox.Show("请先打开串口 再发送数据");
            }
        }


        #endregion

        #region 菜单部分按钮
        private void TSMI_Help_About_Click(object sender, EventArgs e)
        {
            String showIt =
            @"        本软件可以将串口发来的指定信息发布到微博上去！


                                                  华中科技大学                                    
                                                  HelloWorld队";
            MessageBox.Show(showIt, "家庭小助手Beta1.0版");
        }

        private void TSMI_View_SystemLog_Click(object sender, EventArgs e)
        {
            string Path = Application.StartupPath + @"\SystemLog.txt";

            if (File.Exists(@Path))
            {
                Process.Start(@Path);//记得一定要@
            }
            else
            {
                MessageBox.Show("目前还没有系统日志哟...");
            }
        }

        private void TSMI_View_WBLog_Click(object sender, EventArgs e)
        {
            string Path = Application.StartupPath + @"\WBLog.txt";
            if (File.Exists(@Path))
            {
                Process.Start(@Path);//记得一定要@
            }
            else
            {
                MessageBox.Show("现在还没有发微博记录哦...");
            }
        }

        private void TSMI_ConvertCmd_Ctrl_Click(object sender, EventArgs e)
        {

        }

        private void TSMI_View_SysSet_Click(object sender, EventArgs e)
        {

            string Path = Application.StartupPath + @"\SysSet.txt";
            if (File.Exists(@Path))
            {
                Process.Start(@Path);//记得一定要@
                Thread.Sleep(100);
            }
            else
            {
                MessageBox.Show("目前还没有系统配置哟...");
            }
        }

        //查看关键字和命令等
        private void TSMI_View_Convert_Click(object sender, EventArgs e)
        {
            var showConvert = new frmShowConvert();
            showConvert.Show();
        }

        #endregion

        #region 交互区按钮
        //开始串口发微博的按钮
        public bool SendCtrlInfo(String infoToSend)
        {
            ctrlInfo = "";

            //String tempCtrlInfo = "";
            if (infoToSend[0] == beginCh && (infoToSend.Length > 2) && infoToSend[infoToSend.Length - 1] == endCh)
            {

                ctrlInfo = infoToSend.Substring(1, infoToSend.Length - 2);

                for (int i = 0; i < secondClassSet[3].Length; i++)
                {
                    if (!String.IsNullOrWhiteSpace(secondClassSet[3][i]) && !String.IsNullOrWhiteSpace(secondClassValue[3][i]) && secondClassSet[3][i] == ctrlInfo)
                    {
                        ctrlInfo = secondClassValue[3][i];
                        if (ctrlInfo == "tuiOn")//如果是开始tui
                        {
                            #region
                            if (serialPort1.IsOpen)//如果串口是开的
                            {
                                if (btn_receiveTui.Text == "开始tui")//如果按钮是开始接收
                                {
                                    CommunicationSet();//调用Communication函数,设置开始接收和停止的校验位

                                    tuiState = "开启tui";
                                    btn_receiveTui.Text = "停止tui";
                                    Lock_BeginTui(true);//锁定


                                    timer_Tui = new System.Threading.Timer(new TimerCallback(timer_Tui_Tick), this, 0, (int)(numericUpDown_Interval.Value * 1000 * 60));//注意这里必须重新开启一个新线程

                                    PublishStatusThread("成功！我们将开始为您报道情况:)", null);
                                    //好了，到这里，把工作交给定时器timer_SerialToTui_Interval去完成
                                }
                                else//如果按钮是停止tui
                                {
                                    PublishStatusThread("我们正在勤劳的工作呢 :)", null);
                                }
                            }
                            else//如果串口是关的
                            {
                                PublishStatusThread("现在串口还没打开，对不起，我们没法为您工作:)", null);
                            }
                            #endregion
                        }
                        else if (ctrlInfo == "tuiOff")
                        {
                            if (btn_receiveTui.Text == "开始tui")//如果按钮是开始接收
                            {
                                PublishStatusThread("我们本来就没有在工作啊 :)", null);
                            }
                            else//如果按钮是停止tui
                            {
                                Lock_BeginTui(false);//解锁
                                timer_Tui.Dispose();//接收定时器关闭
                                tuiState = "未开启tui";
                                btn_receiveTui.Text = "开始tui";
                                PublishStatusThread("嗯嗯，我们现在不报道，等待您的指示 :)", null);
                            }
                        }
                        else
                        {
                            SendCmd(ctrlInfo);

                            ctrlSuceedTimes++;
                            ctrlState = "Ctrl成功" + ctrlSuceedTimes + "次";
                            ShowState(showTuiState + showCtrlState);
                            SaveWBLog("Ctrl成功, 原始微博为：" + infoToSend + "转换后的命令为：" + ctrlInfo);
                        }
                        return true;

                    }
                }
            }

            //能到这里就说明没找到
            ctrlFailedTimes++;
            ctrlState = "Ctrl失败" + ctrlFailedTimes + "次";
            ShowState(showTuiState + showCtrlState);
            SaveSystemLog("没有找到微博控制关键字");
            Thread.Sleep(100);
            return false;

        }
        public void btn_OpenTui_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)//如果串口是开的
            {
                if (btn_receiveTui.Text == "开始tui")//如果按钮是开始接收
                {
                    CommunicationSet();//调用Communication函数,设置开始接收和停止的校验位

                    tuiState = "开启tui";
                    btn_receiveTui.Text = "停止tui";
                    Lock_BeginTui(true);//锁定

                    timer_Tui = new System.Threading.Timer(new TimerCallback(timer_Tui_Tick), this, 0, (int)(numericUpDown_Interval.Value * 1000 * 60));//注意这里必须重新开启一个新线程


                    //好了，到这里，把工作交给定时器timer_SerialToTui_Interval去完成
                }
                else//如果按钮是停止tui
                {
                    Lock_BeginTui(false);//解锁
                    timer_Tui.Dispose();//接收定时器关闭
                    tuiState = "未开启tui";
                    btn_receiveTui.Text = "开始tui";
                    ShowState(showTuiState + showCtrlState);
                }
            }
            else//如果串口是关的
            {
                MessageBox.Show("请先打开串口");
            }
        }

        private void btn_SendCtrl_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)//如果串口是开的
            {
                if (btn_SendCtrl.Text == "开始Ctrl")//如果按钮是开始Ctrl
                {

                    try
                    {
                        //获取当前最新的微博id
                        string userID = Sina.API.Entity.Account.GetUID();
                        var json = Sina.API.Entity.Statuses.UserTimeline(uid: userID, count: 1);
                        if (json.Statuses != null)
                        {
                            foreach (var status in json.Statuses)
                            {
                                beginWBID = status.ID;//获得当前最新的微博id

                            }
                        }
                        else
                        {
                            MessageBox.Show("没有微博！");
                            return;
                        }

                        CommunicationSet();//调用Communication函数,设置开始接收和停止的校验位
                        ctrlState = "开启Ctrl";
                        btn_SendCtrl.Text = "停止Ctrl";
                        Lock_BeginCtrl(true);//锁定

                        ShowState(showTuiState + showCtrlState);


                        timer_Ctrl = new System.Threading.Timer(new TimerCallback(timer_Ctrl_Tick), this, 0, (int)(numericUpDown_Ctrl.Value * 1000 * 60));//注意这里必须重新开启一个新线程
                    }
                    catch (Exception err)
                    {
                        SaveSystemLog("ctrl过程出错！" + err.Message);
                    }
                    //好了，到这里，把工作交给定时器timer_Ctrl完成
                }
                else//如果按钮是停止Ctrl
                {
                    Lock_BeginCtrl(false);//解锁
                    timer_Ctrl.Dispose();//接收定时器关闭
                    ctrlState = "未开启Ctrl";
                    btn_SendCtrl.Text = "开始Ctrl";
                }
            }
            else//如果串口是关的
            {
                MessageBox.Show("请先打开串口");
            }
        }

        #endregion

        #region 微博按钮
        private void btn_RefreshMyWB_Click(object sender, EventArgs e)
        {
            LoadUserInformation();//加载用户信息
            LoadUserTimeline();//加载好友信息
        }

        #endregion

        #region 记录系统配置是否有改变
        private void comboBox_serialName_SelectedIndexChanged(object sender, EventArgs e)
        {
            setChanged = true;
        }

        private void comboBox_Bault_SelectedIndexChanged(object sender, EventArgs e)
        {
            setChanged = true;
        }

        private void comboBox_checkBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            setChanged = true;
        }

        private void comboBox_statusBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            setChanged = true;
        }

        private void comboBox_stopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            setChanged = true;
        }

        private void radioButton_receiveInHex_CheckedChanged(object sender, EventArgs e)
        {
            setChanged = true;
        }

        private void radioButton_receiveInChar_CheckedChanged(object sender, EventArgs e)
        {
            setChanged = true;
        }

        private void checkBox_sendRound_CheckedChanged(object sender, EventArgs e)
        {
            setChanged = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            setChanged = true;
        }

        private void numericUpDown_Interval_ValueChanged(object sender, EventArgs e)
        {
            setChanged = true;
        }

        private void numericUpDown_Ctrl_ValueChanged(object sender, EventArgs e)
        {
            setChanged = true;
        }

        private void textBox_BeginCh_TextChanged(object sender, EventArgs e)
        {
            setChanged = true;
        }

        private void textBox_EndCh_TextChanged(object sender, EventArgs e)
        {
            setChanged = true;

        }
        #endregion


        #region 保存相关文件函数模块

        private static void SaveSystemLog(string logToWrite)
        {
            if (logToWrite != "")
            {
                Thread SaveLog = new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(Application.StartupPath + @"\SystemLog.txt", true, Encoding.UTF8))
                            {
                                sw.WriteLine(DateTime.Now.ToString() + " " + logToWrite);
                                sw.Flush();
                                sw.Close();
                                break;
                            }
                        }
                        catch
                        { }
                    }
                });
                SaveLog.Start();
            }
        }

        private void SaveWBLog(string WBToWrite)
        {
            try
            {
                if (WBToWrite != "")
                {
                    Thread SaveLog = new Thread(() =>
                    {
                        using (StreamWriter sw = new StreamWriter(Application.StartupPath + @"\WBLog.txt", true, Encoding.UTF8))
                        {
                            sw.WriteLine(DateTime.Now.ToString() + "：" + WBToWrite);
                            sw.Flush();
                            //sw.Close();
                        }
                        Thread.Sleep(100);
                    });
                    SaveLog.Start();
                }
            }
            catch { }
        }

        #endregion

    }
}
