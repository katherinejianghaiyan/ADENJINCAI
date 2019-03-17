using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Entity;

namespace Luoyi.Web.Controls
{
    public partial class Footer :  System.Web.UI.UserControl
    {
        public string FooterBtnClass { get; set; }
        public int CartQty { get; set; }
        public string MasterHome { get; set; }
        public string MasterMessage { get; set; }
        public string MasterCart { get; set; }
        public string MasterConcept { get; set; }
        public string MasterAccount { get; set; }
        public bool isPOD { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            isPOD = false;
           // if (!IsPostBack)
            {
                //var userInfo = UserHelper.GetUserInfo();
                //isPOD = userInfo.PaymentMethod.Equals("POD");
                Page p = this.Page;
                UserInfo userInfo = p.GetType().GetProperty("_UserInfo").GetValue(p) as UserInfo;
                SiteInfo site = p.GetType().GetProperty("siteInfo").GetValue(p) as SiteInfo;
                if (site == null) return;

                isPOD = site.PaymentMethod.Equals("POD");
                FooterBtnClass = "FooterBtn5";
                if (isPOD)  FooterBtnClass = "FooterBtn4";
                CartHelper cart = new CartHelper(userInfo, HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("mycart") ? true : false);
                CartQty = cart.GetQty();
            }
        }
    }
}