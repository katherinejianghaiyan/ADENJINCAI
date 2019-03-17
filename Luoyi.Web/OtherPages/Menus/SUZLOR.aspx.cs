using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Common;
using Luoyi.Entity;
using System.Globalization;
using System.Data;


namespace Luoyi.Web.OtherPages.Menus
{
    public partial class SUZLOR : PageBase
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //多语言不显示
                Master.ShowDefault(true, false);
            }
        }

        protected void ShowImage()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                List<PostPic> pics = BLL.SUZHYC.GetPost(siteInfo.GUID, "SUZLORFlavor");
                if (pics == null || !pics.Any())
                    throw new Exception();

                //显示本周
                DateTime date1 = DateTime.Now;
                date1 = date1.AddDays(-1 * (date1.DayOfWeek.ToInt() == 0 ? 7 : date1.DayOfWeek.ToInt()));
                DateTime date2 = date1.AddDays(7);

                PostPic pic = pics.FirstOrDefault(q => q.startDate > date1 & q.startDate < date2);

                if (pic == null) //显示最新的
                    pic = pics.OrderByDescending(q => q.startDate).FirstOrDefault();

                sb.AppendFormat("<img class='event' src='{0}{1}' style='width:90%;' />",
                    ConfigurationManager.AppSettings["ActivityImagesURL"].ToString(), pic.picUrl);
            }
            catch { }

            Response.Write(sb.ToString());            
        }
    }
}