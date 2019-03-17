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


namespace Luoyi.Web.OtherPages.Events
{
    public partial class SUZHYC : PageBase
    {
        public static string startdate = "";
        public static string picUrl = null;
        public static List<PostPic> PostPic = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //多语言不显示
                Master.ShowDefault(false, false);
                string siteGuid = siteInfo.GUID;
                string businessType = "SUZHYCActivity";

                List<PostPic> PostPic = BLL.SUZHYC.GetPost(siteGuid,businessType);

               startdate = PostPic[0].startDate.ToString("yyyy-M-d");
               picUrl = PostPic[0].picUrl;

               
            }
        }
        // 绑定画面数据
        public string GetPicUrl()
        {
            return string.Format("{0}{1}",
                    ConfigurationManager.AppSettings["ActivityImagesURL"].ToString(), picUrl);
        }
        public string GetStartDate()
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(startdate))
            {
                result= "活动日期: " + startdate + "<br>敬请期待";
            }
            return result;
        }

    }
}

