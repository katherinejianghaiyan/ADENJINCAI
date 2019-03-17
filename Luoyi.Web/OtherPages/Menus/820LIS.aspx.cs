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

namespace Luoyi.Web.OtherPages.Menus
{
    public partial class _820LIS : PageBase
    {
        public static string result_bak = "";
        public static int size = 0;
        public static string img = null;
        public static List<NutritionList> lstNutritionList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //多语言不显示
                Master.ShowDefault(true, false);
                Master.MasterHome();

                // 取得画面数据
                lstNutritionList = BLL.Nutrition.GetNutritionInfo(SysConfig.UserLanguage.ToString(), siteInfo.Code);
                // 绑定画面数据
                this.BindData();
            }
            //产品分类 18-7-7 移到Header.ascx.cs
            //var classGUID = WebHelper.GetQueryString("ClassGUID");

            //if (siteInfo.ShowByClass)
            //    classGUID = Master.SetTitleByClass(classGUID, out itemimg);
        }

        // 绑定画面数据
        private void BindData()
        {
            try
            {
                int pageIndex = int.Parse(hfPageIndex.Value);
                //顶部图片
                rptLogo.DataSource = lstNutritionList.OrderBy(q => q.dataType).ThenBy(q => q.startDate);                
                rptLogo.DataBind();

                if (lstNutritionList == null || !lstNutritionList.Any())
                    return;

                NutritionList nObj = lstNutritionList[pageIndex];
                size = nObj.lstNutritionWeekDay.Where(r => !string.IsNullOrWhiteSpace(r.day)).Count();
                img = nObj.img;
                rptList.DataSource = nObj.lstNutritionWeekDay.Where(q=>!string.IsNullOrWhiteSpace(q.day)) ;
                rptList.DataBind();
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
            }
        }

        protected string GetLogoMenu(string text)
        {
            return text + ("zh_cn".Equals(SysConfig.UserLanguage.ToString().ToLower()) ? " 菜单" : " Menu");
        }

        protected string GetLogoDate(string start, string end)
        {
            return ConvertToENDate(start) + "&nbsp;-&nbsp;" + ConvertToENDate(end);
        }

        // 日期格式化成英文
        private string ConvertToENDate(string date)
        {
            DateTime dt = Convert.ToDateTime(date);
            string lang = SysConfig.UserLanguage.ToString();

            if ("zh_cn".Equals(lang.ToLower()))
            {
                return dt.ToString("M月d日");

            }

            return dt.ToString("d MMM", CultureInfo.CreateSpecificCulture("en-GB"));
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rep = e.Item.FindControl("rptItemList") as Repeater;

            rep.DataSource = (e.Item.DataItem as NutritionWeekDay).lstNutritionInfo;
            rep.DataBind();
        }

        // TODO: 输出HTML(...)
        public string GetHtmlString(string val1, string val2)
        {
            string result = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (!"shortname".Equals(val1.ToLower()) && !"qty".Equals(val1.ToLower()) && !string.IsNullOrWhiteSpace(result_bak))
            {
                result += result_bak + "</p>";
                result_bak = string.Empty;
            }

            if ("name".Equals(val1.ToLower()) || "ingredients".Equals(val1.ToLower()))
            {
                result += "<p class=\"{0}\">{1}<p>";
                result = string.Format(result, val1.ToLower(), val2);
            }
            else if("shortname".Equals(val1.ToLower()))
            {
                if(string.IsNullOrWhiteSpace(result_bak))
                    result_bak = "<p class=\"{0}-p\"><span class=\"{0}1\">{1}</span>";
                else
                    result_bak += "<span class=\"{0}2\">{1}</span>";
                result_bak = string.Format(result_bak, val1.ToLower(), val2);
            }
            else if("qty".Equals(val1.ToLower()))
            {
                result_bak += "<span class=\"{0}\">{1}</span>";
                result_bak = string.Format(result_bak, val1.ToLower(), val2);

                if (SubstringCount(result_bak, "shortname2") > 0)
                {
                    result += result_bak;
                    result += "</p>";
                    result_bak = string.Empty;
                }
            }
            else if("am".Equals(val1.ToLower()) || "pm".Equals(val1.ToLower()))
            {
                result += "<p class=\"ampm\">";
                result += "<span>{0}:</span>";
                result += "<span class=\"ampm-content\">{1}</span>";
                result += "</p>";
                result = string.Format(result, val1.ToUpper(), val2);
            }

            return result;
        }

        // TODO: 输出HTML（图片）
        public string GetHtmlImage(int index, string day)
        {
            string result = string.Empty;

            switch(index + 1)
            {
                case 1:
                    result = "<img class=\"mon-left-img\" src=\"/img/OtherPages/820LIS/icons/" + day + ".png\"/>";
                    result += "<img class=\"mon-right-img\" src=\"/img/OtherPages/820LIS/icons/fox.png\" />";
                    break;
                case 2:
                    result = "<img class=\"tue-right-img\" src=\"/img/OtherPages/820LIS/icons/" + day + ".png\"/>";
                    break;
                case 3:
                    result = "<img class=\"wed-left-img\" src=\"/img/OtherPages/820LIS/icons/" + day + ".png\"/>";
                    result += "<img class=\"wed-right-img\" src=\"/img/OtherPages/820LIS/icons/decoration.png\" />";
                    break;
                case 4:
                    result = "<img class=\"thu-right-img\" src=\"/img/OtherPages/820LIS/icons/" + day + ".png\"/>";
                    break;
                case 5:
                    result = "<img class=\"fri-left-img\" src=\"/img/OtherPages/820LIS/icons/" + day + ".png\"/>";
                    result += "<img class=\"fri-right-img\" src=\"/img/OtherPages/820LIS/icons/fox2.png\" />";
                    break;
            }

            return result;
        }

        // TODO: 输出空行
        public string GetEmptyLine(int index)
        {
            index += 1;
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(result_bak))
            {
                result += (result_bak + "</p>");
                result_bak = string.Empty;
            }

            if (index == size)
                return result;

            if (Convert.ToBoolean(index % 2))
                result += "<div class=\"empty-line2\"></div>";
            else
                result += "<div class=\"empty-line1\"></div>";

            return result;
        }

        // TODO: Dashboard Html
        public string GetDashBord()
        {
            //<div class="dashboard-title">
            //    <p>Nutrition Facts</p>
            //</div>
            //<div class="main2">
            //    <img class="dashboard-img" src="/img/OtherPages/820LIS/datas/30408654845989145.jpg" />
            //</div>
            string result = string.Empty;
            string strImg = img == null ? string.Empty : img.ToString();

            if (string.IsNullOrWhiteSpace(strImg))
                return result;
            img = System.IO.Path.Combine(siteInfo.Code, img);
            img = System.IO.Path.Combine(ConfigurationManager.AppSettings["ItemOtherPagesPath"], img);
            img = System.IO.Path.Combine(ConfigurationManager.AppSettings["ItemImagesURL"], img);
            result += "<div class=\"dashboard-title\">";
            result += "<p>Nutrition Facts</p>";
            result += "</div>";
            result += "<div class=\"main2\">";
            result += "<img class=\"dashboard-img\" src=\"" + img + "\" />";
            result += "</div>";

            return result;
        }

        /// <summary>
        /// 计算字符串中子串出现的次数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="substring">子串</param>
        /// <returns>出现的次数</returns>
        static int SubstringCount(string str, string substring)
        {
            if (str.Contains(substring))
            {
                string strReplaced = str.Replace(substring, "");
                return (str.Length - strReplaced.Length) / substring.Length;
            }

            return 0;
        }


        protected void btnNext_Click(object sender, EventArgs e)
        {
            this.BindData();
        }
    }
}