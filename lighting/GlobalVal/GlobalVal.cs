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

        #region 状态栏显示变量
        public readonly int showNothing = 0;
        public readonly int showTuiState = 1;
        public readonly int showCtrlState = 2;

        public String tuiState = "未开启tui";//串口tui状态
        public String ctrlState = "未开启Ctrl";
        #endregion

        #region 定时器、函数之间通信变量
        public bool roundSendingOn = false;//是否是正在循环发送过程中
        public bool findingTui = false;//给FindTuiInfo的权杖，表示是否有权利去DataReceived神那里拿串口数据
        public bool failed = false;//默认是FindTuiInfo你还没失败啦

        public bool findedTuiSign = false;//FindTuiInfo用来表示是否可以开始存储数据。
        public bool findedTui = false;//FindTuiInfo 用来表示是否已经找到了tuiInfo
        #endregion

        #region 系统运行状态记录变量

        public static bool loginState;//记录用户登录状态

        public static bool setChanged = false;//记录用户配置变化状态，false，配置没变，不需要保存配置
        
        public static string beginWBID;//记录微博id，用于判断是否更新了微博的标记

        
        public int tuiFailedTimes = 0;//记录tui失败的次数
        public int tuiSucceedTimes = 0;//记录tui成功的次数

        
        public int ctrlFailedTimes = 0;//记录ctrl失败次数
        public int ctrlSuceedTimes = 0;//记录ctrl成功次数

        #endregion        

        #region 系统配置变量

        //类别的名称
        public  static String[] topClassSet = {"SerialSet","IActionSet","TuiTriggerAndWB","CtrlTriggerAndCmd"};
        //大类别对应的配置名
        public static String[][] secondClassSet = new String[4][];

        //配置名的普通值
        public static String[][] secondClassValue = new String[4][];

        //配置名的pic值
        public static String[] secondClassSet2Pic = new String[1];//第三大类，即secondClassSet[2]才有的属性，picture

        //起始和终止标记
        public char beginCh = '@';
        public char endCh = '#';

        #endregion

        #region 其他变量
        public int count = 0;//FindTuiInfo用来给数组赋值时计数的
        public char[] tuiInfo = new char[140];//FindTuiInfo用来给要找到的信息存储的

        public string ctrlInfo = "";

        public byte[] imgBuffInsert;//存储发表图片微博时，用户存入的图片数据
        #endregion
    }
}
