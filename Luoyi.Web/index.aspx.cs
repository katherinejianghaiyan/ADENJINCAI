using System;
using System.Web.UI;
using Luoyi.Entity;

namespace Luoyi.Web
{
    public partial class index : PageBase//System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page p = this.Page;
            SiteInfo site = p.GetType().GetProperty("siteInfo").GetValue(p) as SiteInfo;
            if(string.IsNullOrWhiteSpace(site.LoadPages)) 
                Response.Redirect("Default.aspx",true);

            string s = site.LoadPages.Split(",;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].Trim();
            s = string.Format("~/{0}", s);
            Response.Redirect(s,true);
        }
    }
}