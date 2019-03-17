using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Entity;
using Luoyi.Common;

namespace Luoyi.Web.Account
{
    public partial class UserInfo : PageBase
    {
        protected string showMessage = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.MasterAccount();
                imgHead.ImageUrl = ImgHeader.ImageUrl = _UserInfo.HeaderImgUrl;
                txtUserName.Text = _UserInfo.UserName ?? string.Concat(_UserInfo.FirstName, " ", _UserInfo.LastName);
                txtMobile.Text = _UserInfo.Mobile;
                txtDepartment.Text = _UserInfo.Department;
                txtSection.Text = _UserInfo.Section;

                var msg = Request["msg"];
                if ("1".Equals(msg))
                    this.showMessage = msg;
            }
        }
    }
}