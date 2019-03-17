using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Luoyi.Web.OtherPages
{
    public partial class ADENProfile : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        // 绑定画面数据
        public void GetHTML()
        {
            Response.WriteFile("ADENProfile/AdenProfileCn.htm");
        }
    }
}