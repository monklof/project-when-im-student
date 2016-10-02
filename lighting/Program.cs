using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NetDimension.Weibo;

namespace HelloWorld
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
            //新建一个授权2.0类
			OAuth oauth = new OAuth(Properties.Settings.Default.AppKey, Properties.Settings.Default.AppSecret, Properties.Settings.Default.CallbackUrl);


			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            
            //先建立一个授权页面
			var LoginForm = new frmLogin(oauth);
			if (LoginForm.ShowDialog() == DialogResult.OK)//如果登录成功
			{
				Application.Run(new frmHelloWorld(oauth));//运行主界面
			}
		}
	}
}
