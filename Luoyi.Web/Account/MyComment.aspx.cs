using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Luoyi.Web.Account
{
    public partial class MyComment : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.MasterAccount();
                imgHead.ImageUrl = _UserInfo.HeaderImgUrl;
                rptList.DataSource = BLL.UserComment.GetTable(string.Format(" AND c.UserID = {0}", _UserInfo.UserID));
                rptList.DataBind();
            }
        }
    }
}