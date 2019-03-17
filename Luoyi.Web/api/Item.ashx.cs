using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Text;
using System.Web;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.Web.api
{
    /// <summary>
    /// Item 的摘要说明
    /// </summary>
    public class Item : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var pageIndex = WebHelper.GetFromInt("PageIndex");
            var classGUID = WebHelper.GetFormString("ClassGUID");
            var customID = WebHelper.GetFromInt("CustomID");
            var keyword = WebHelper.GetFormString("Keyword");
            var userInfo = UserHelper.GetUserInfo();
            var siteInfo = Luoyi.BLL.Site.GetInfo(userInfo.SiteGUID);
            List<int> listSortNbrs = new List<int>();
            
            ItemFilter itemFilter = new ItemFilter()
            {
                PageIndex = pageIndex,
                PageSize = 10,
                ClassGUID = classGUID,
                Keyword = keyword,
                SiteGUID = userInfo.SiteGUID,
                PriceType = "Sales",
               // UserType = siteInfo.UserType,
                //ItemSortNbrs = string.Join(",", listSortNbrs)

            };


            if (customID > 0)
            {
                var customInfo = BLL.ItemCustomClass.GetInfo(customID);
                if (customInfo != null)
                {
                    itemFilter.Columns = customInfo.Columns;
                    itemFilter.TableName = customInfo.TableName;
                    itemFilter.Filter = customInfo.Filter;
                    itemFilter.OrderBy = customInfo.OrderBy;
                }
            }

            int recordCount = 0;

            DataTable dt = BLL.Item.GetPage(itemFilter, SysConfig.UserLanguage.ToString(), out recordCount);

            int pages = (recordCount % itemFilter.PageSize == 0) ? (recordCount / itemFilter.PageSize) : (recordCount / itemFilter.PageSize + 1);
            StringBuilder sb = new StringBuilder();

            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<div class=\"goodsList\"><a ");

                if (siteInfo.ShowProductInfoInDetail)
                    sb.AppendFormat("href=\"./ItemDetail.aspx?ItemID={0}\"", dr["ItemID"].ToString());
                sb.AppendFormat("><img class=\"detail-img\" src=\"{0}\" /></a>",
                    System.IO.Path.Combine(ConfigurationManager.AppSettings["ItemImagesURL"], dr["Image1"].ToString()));

                sb.AppendFormat("<p class=\"goods-title\">{0}{1}</p>", dr["ItemName"].ToString(), "");// (string.IsNullOrWhiteSpace(dr["HolidayName"].ToString()) ? "" : "<span class=\"holiday\">" + dr["HolidayName"].ToString().Trim() + "</span>"));
                sb.Append("<p class=\"goods-sku\">");
                sb.AppendFormat("<span><img src=\"./img/icon/icon-potion_{0}.jpg\" /><small>{0} {1}</small></span>",
                        dr["DishSize"].ToString().Trim() == "1" ? "1" : "2", HtmlLang.Lang("PotionNumber", "人份", "/Default.aspx"));
                sb.AppendFormat("<span><img src=\"./img/icon/icon-weight_07.jpg\" /><small>{0}g</small></span>", dr["Weight"].ToString());
                sb.Append("</p>");
                sb.AppendFormat("<p class=\"goods-num\"><span>￥{0}</span><img class=\"addCart\" data-cartadd=\"true\" data-itemid=\"{1}\" src=\"./img/icon/addCart_07.jpg\" /></p>",
                    dr["ItemPrice"] != null ? dr["ItemPrice"].ToDecimal().ToString("G0") : dr["Price"].ToDecimal().ToString("G0"), dr["ItemID"].ToString());
                sb.Append("</div>");
            }

            PageInfo pageinfo = new PageInfo();
            pageinfo.PageMore = itemFilter.PageIndex < pages ? "true" : "false";
            pageinfo.PageIndex = pageIndex + 1;
            pageinfo.ClassGUID = itemFilter.ClassGUID;
            pageinfo.PageContent = sb.ToString();

            AjaxJsonInfo ajax = new AjaxJsonInfo();
            ajax.Status = 1;
            ajax.Message = "成功";
            ajax.Data = pageinfo;

            context.Response.Write(JsonHelper.JSONSerialize(ajax));
        }

        public class PageInfo
        {
            public string PageMore { get; set; }
            public int PageIndex { get; set; }
            public string ClassGUID { get; set; }
            public string PageContent { get; set; }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}