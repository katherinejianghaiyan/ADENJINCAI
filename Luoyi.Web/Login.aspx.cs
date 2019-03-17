using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Common;

namespace Luoyi.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            //string msg;
            //if (TicketHelper.ValidateTicket(out msg) > 0)
            //{
            //    Response.Redirect("/Default.aspx");
            //}
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Palert.Visible = false;
            //litAlert.Text = string.Empty;
            //var message = string.Empty;

            //if (txtUserName.Text.Trim() == string.Empty) message += @"<li>请输入登录账号！</li>";
            //if (txtPassword.Text.Trim() == string.Empty) message += @"<li>请输入登录密码！</li>";

            //if (message != string.Empty)
            //{
            //    litAlert.Text = message;
            //    Palert.Visible = true;
            //    return;
            //}

            //var userName = txtUserName.Text.Trim();
            //var password = txtPassword.Text.Trim().ToLower().ToMD5();

            //// 登录校验
            //var adminID = BLL.SiteAdmin.Signin(userName, password);

            //if (adminID > 0)
            //{
            //    TicketHelper.ResponseTicketCookie(adminID);

            //    //返回所请求的URL

            //    var indexUrl = "/PurchaseOrder.aspx";

            //    Response.Redirect(indexUrl);
            //}
            //else
            //{
            //    message += @"<li>用户名或密码错误！</li >";
            //    litAlert.Text = message;
            //    Palert.Visible = true;
            //}
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}