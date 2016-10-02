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
        private void timer1_Tick(object sender, EventArgs e)
        {
            /*自检是否还有roundSendingOn权杖可用*/
            if (roundSendingOn)//哟，我还可以继续发～～
            {
                timer1.Interval = Convert.ToInt32(numericUpDown1.Value);

                if (SendCmd(textBox_sendContent.Text.Trim()) == false)
                {
                    /*结束操作*/
                    roundSendingOn = false;
                    timer1.Enabled = false;
                    button_serialOpen.Enabled = true;
                    button_send.Text = "开始发送";//重新回到开始发送状态
                    MessageBox.Show("如果要发送单字符串 请填写字符");
                    textBox_sendContent.Focus();//聚焦为sendContent
                }
            }
            else//否则释放这时候不应该控制的项
            {
                timer1.Enabled = false;
                button_serialOpen.Enabled = true;
                button_send.Text = "开始发送";
            }
        }

        /* 背景：
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
         */


        public void timer_Tui_Tick(object sender)
        {

            failed = false;
            findedTui = false;
            findedTuiSign = false;

            findingTui = true;
            tuiState = "正在查找tuiInfo";//我正在干活
            ShowState(showTuiState + showCtrlState);
            timer_Wait = new System.Threading.Timer(new TimerCallback(timer_Wait_Tick), this, 10000, 0);//注意这里必须重新开启一个新线程
            
            Thread findTui_Thread = new Thread(() =>
                {
                    while (!failed)//如果还没到点
                    {
                        Thread.Sleep(1000);
                        if (findedTui)//如果你告诉我找到了
                        {
                            
                            if (PublishStatusNThread(new String(tuiInfo).Split('\0')[0]))//表彰你的成果，发微博！！(由于数组有140大小，把后面跟着的\0去掉，也就是说信号里面最好不带\)
                            {
                                failed = false;//发微博没失败
                            }
                            else
                            {
                                failed = true;//发微博失败了
                            }
                            break;
                        }
                    }
                    if (failed)//时间到，你还没找到
                    {
                        if (findedTui)
                            SaveSystemLog("找到了tuiInfo，但是发送失败！");
                        else
                            SaveSystemLog("没有找到tuiInfo！");
                        Thread.Sleep(100);

                        tuiFailedTimes++;
                        tuiState = "tui失败!" + tuiFailedTimes + "次（详见日志SystemLog.txt）";
                        Thread.Sleep(100);//等0.1秒，让前面的SaveSystemLog把日志存储完
                        ShowState(showTuiState + showCtrlState);
                    }
                    else//找到了
                    {
                        tuiSucceedTimes++;
                        tuiState = "tui成功！" + tuiSucceedTimes + "次";
                        ShowState(showTuiState + showCtrlState);
                    }
                    findingTui = false;//这次工作到此为止，我要收回权杖
                    timer_Wait.Dispose();//关闭等待定时器
                });

            findTui_Thread.Start();

        }

        //开始搜索
        private void timer_Ctrl_Tick(object sender)
        {

            Thread thLoadUser = new Thread(new ThreadStart(delegate()
            {
                try
                {
                    string userID = Sina.API.Entity.Account.GetUID();
                    StringBuilder statusBuilder = new StringBuilder();
                    var json = Sina.API.Entity.Statuses.UserTimeline(uid: userID, sinceID: beginWBID);
                    if (json.Statuses != null)
                    {
                        ctrlState = "  正在Ctrl... ";
                        ShowState(showTuiState + showCtrlState);
                        int first = 0;
                        foreach (var status in json.Statuses)//这里是从高到低读
                        {
                            if (first == 0)//由于是从高到低度，最上面的也就是最新的，存储最新的微博
                                beginWBID = status.ID;
                            if (status.User == null)
                                continue;
                            SendCtrlInfo(status.Text);
                            first++;
                        }
                        ctrlState = "  Ctrl结束... ";
                        ShowState(showTuiState + showCtrlState);
                    }
                }
                catch (Exception err)
                {
                    SaveSystemLog("ctrl过程出错！" + err.Message);
                }
            }));

            thLoadUser.IsBackground = true;
            thLoadUser.Start();

        }

        private void timer_Wait_Tick(object sender)
        {
            failed = true;//不再等待
            timer_Wait.Dispose();
        }
    }
}
