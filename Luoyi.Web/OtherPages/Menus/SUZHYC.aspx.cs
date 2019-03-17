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
    public partial class SUZHYC : PageBase
    {
        private static int size = 0;

        private static List<MealDateList> lstNutritionList = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //多语言不显示
                Master.ShowDefault(true,false);

                // 取得画面数据
                lstNutritionList = BLL.Nutrition.GetDishInfo(SysConfig.UserLanguage.ToString(), siteInfo.GUID);

                //默认页
                hfPageIndex.Value = lstNutritionList.IndexOf(
                    lstNutritionList.Where(q => DateTime.Parse(q.startDate) >= DateTime.Now.Date)
                    .OrderBy(q=>DateTime.Parse(q.startDate)).FirstOrDefault()).ToString();
                if (hfPageIndex.Value.ToInt() < 0)
                    hfPageIndex.Value = "0";
                // 绑定画面数据
                this.BindData();
            }

        }

        // 绑定画面数据
        private void BindData()
        {
            try
            {
                int pageIndex = int.Parse(hfPageIndex.Value);
                //顶部图片
                string result = string.Empty;

                rptLogo.DataSource = lstNutritionList.Where(r => !string.IsNullOrWhiteSpace(r.day))
                    .Distinct(dr => new { day = dr.day, startDate=dr.startDate }).ToList();
                rptLogo.DataBind();

                if (lstNutritionList == null || !lstNutritionList.Any())
                    return;

                size = lstNutritionList[pageIndex].MealType.ToList().Count();

                rptList.DataSource = lstNutritionList[pageIndex].MealType.Select(dr => new { dinner = dr.dinner, windowlines = dr.Windowslines });
                rptList.DataBind();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private int i = 0;

        /// <summary>
        /// 餐次分类
        /// </summary>
        /// <param name="dinner"></param>
        /// <returns></returns>
        public string GetMealType(string dinner)
        {
            string mark = string.Empty;
            string result = string.Empty;

            string dinnerType = string.Empty;

            int pageIndex = int.Parse(hfPageIndex.Value);

            i++;
            switch (dinner.ToLower()) //dinner
            {
                case "早餐":
                    dinnerType = "breakfast.png";
                    break;
                case "午餐":
                    dinnerType = "lunch.png";
                    break;
                case "晚餐":
                    dinnerType = "dinner.png";
                    break;
                case "夜宵":
                    dinnerType = "supper.png";
                    break;
                case "深夜餐":
                    dinnerType = "midnightMeal.png";
                    break;
            }

            dinnerType += "?" + Guid.NewGuid();
            result = string.Format("<img class=\"{0}-img\" src=\"../../img/OtherPages/120HYC/mealType/{1}\" />",
                i % 2 == 1 ? "mon-left" : "tue-right", dinnerType);
            switch (i) //dinner
            {
                case 1:                   
                    result += "<img class=\"mon-right-img\" src=\"../../img/OtherPages/120HYC/icons/fox.png\" />";
                    break;
                case 3:
                    result += "<img class=\"wed-right-img\" src=\"../../img/OtherPages/120HYC/icons/decoration.png\" />";
                    break;
                case 5:
                    result += "<img class=\"fri-right-img\" src=\"../../img/OtherPages/120HYC/icons/fox2.png\" />";
                    break;
            }
            return result;
        }
        int index = 0;
        public string GetEmptyLine()
        {
            index += 1;
            string result = string.Empty;

            if (index == size)
                return result;
            
            if (Convert.ToBoolean(index % 2))
                result += "<div class=\"empty-line2\"></div>";
            else
                result += "<div class=\"empty-line1\"></div>";
            
            return result;
        }

        public string GetHtmlLogo(string day)
        {

            return "<img class=\"logo\" src=\"../../img/OtherPages/120HYC/icons/Day" + day + ".jpg?" + Guid.NewGuid() + "\"/>";
            //return "<img style=\"width:100%;padding-top: 1.36rem;\" src=\"../../img/OtherPages/120HYC/icons/Day" + day + ".jpg?" + Guid.NewGuid() + "\"/>";
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rep = e.Item.FindControl("rptItemList") as Repeater;

            rep.DataSource = (e.Item.DataItem as dynamic).windowlines;
            rep.DataBind();
        }

        protected string GetLogoDate(string date)
        {
            return ConvertToENDate(date);
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
        public string GetHtmlWindow(string val2, string flag) //int index, string day
        {
            string result = string.Empty;

            if (flag == "windowName")
            {
                result = "<span class=\"name\">{0}</span>";
            }
            else
            {
                result = "<p class=\"ingredients\">{0}<p>";
            }

            return string.Format(result, val2);
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

