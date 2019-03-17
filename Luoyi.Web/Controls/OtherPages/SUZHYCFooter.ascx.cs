using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Linq;
using Luoyi.Entity;
using Luoyi.Common;

namespace Luoyi.Web.Controls.OtherPages
{
    public partial class SUZHYCFooter :  System.Web.UI.UserControl
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

        protected void ShowButtons()
        {
            var buttons = new List<dynamic>
            {
                new {
                    pagePath = "/OtherPages/ADENProfile.aspx",
                    buttonIcon = "/img/OtherPages/120HYC/icons/adenicon.ico",
                    pageName = "Profile"
                },
                new {
                    pagePath = "/OtherPages/Menus/SUZHYC.aspx",
                    buttonIcon = "/img/OtherPages/120HYC/icons/menu.png",
                    pageName = "WeeklyMenu"
                },
                new {
                    pagePath = "/OtherPages/Events/SUZHYC.aspx",
                    buttonIcon = "/img/OtherPages/120HYC/icons/event.png",
                    pageName = "Promotion"
                },
                new {
                    pagePath = "/Survey/SiteSurvey.aspx?type=satisfaction",
                    buttonIcon = "/img/OtherPages/120HYC/icons/satisfaction.png",
                    pageName = "Satisfaction"
                },
                new {
                    pagePath = "/Survey/SiteSurvey.aspx?type=complaint",
                    buttonIcon = "/img/OtherPages/120HYC/icons/complaint.png",
                    pageName = "Complaint"
                },
            };

            Page p = this.Page;
            SiteInfo site = p.GetType().GetProperty("siteInfo").GetValue(p) as SiteInfo;
            if (site == null) return;

            var tmp = buttons.Select(q => new { a = q });

            //显示该点规定的几个页面
            if (!string.IsNullOrWhiteSpace(site.LoadPages))
                tmp = buttons.Join(site.LoadPages.Split(',', ';'), a => a.pagePath, b => b, (a, b) => new { a });           

            //只有一个菜单
            if (tmp == null || !tmp.Any() || tmp.Count() < 2)
            {
                Response.Write("");
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<footer><ul>");
            foreach(var li in tmp)
            {
                sb.AppendFormat("<li class='FooterBtn3' style='width:{0}%'>",100 / tmp.Count());
                sb.AppendFormat("<a href='{0}'>", WebHelper.GetUrlPath(li.a.pagePath));
                sb.AppendFormat("<p><img class='iconimg' src='{0}'></p>", WebHelper.GetUrlPath(li.a.buttonIcon));
                sb.AppendFormat("<p>{0}</p>", HtmlLang.Lang(li.a.pageName, "", "SUZHYC"));
                sb.Append("</a></li>");
            }
            sb.Append("</ul></footer>");
            Response.Write(sb.ToString());
        }
    }
}