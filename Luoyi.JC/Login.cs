using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Luoyi.Common;

namespace Luoyi.JC
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var userName = txtUserName.Text.Trim();
            var userPwd = txtUserPwd.Text.Trim();

            if (userName == string.Empty || userPwd == string.Empty)
            {
                MessageBox.Show("请输入账号或密码");
            }
            else
            {
                var adminID = BLL.SiteAdmin.Signin(userName, userPwd);

                if (adminID > 0)
                {
                    UserLogin._AdminInfo = BLL.SiteAdmin.GetInfo(adminID);

                    this.DialogResult = DialogResult.OK;  
                    this.Close();
                }
                else
                {
                    MessageBox.Show("账号或密码错误，请重试");
                }
            }

        }
    }
}
