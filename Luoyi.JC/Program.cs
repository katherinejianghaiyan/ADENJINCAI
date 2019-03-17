using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Luoyi.JC
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form Login = new Login();
            Login.ShowDialog();//显示登陆窗体  
            if (Login.DialogResult == DialogResult.OK)
                Application.Run(new MainMDI());//判断登陆成功时主进程显示主窗口  
            else return;  
        }
    }
}
