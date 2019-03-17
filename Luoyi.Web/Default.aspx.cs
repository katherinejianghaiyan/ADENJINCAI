using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Luoyi.Common;
using Luoyi.Entity;
using Senparc.Weixin.MP.TenPayLibV3;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace Luoyi.Web
{
    public partial class Default : PageBase
    {
        public bool SplitPages = true;
        protected string itemimg = "";
        protected void Page_Load(object sender, EventArgs e)
        {    //var siteInfo = Luoyi.BLL.Site.GetInfo(_UserInfo.SiteGUID);
            // string s = siteInfo.WelcomeMsgEN;
            //testwechat();
            if (string.IsNullOrWhiteSpace(_UserInfo.SiteGUID)) return;
            if (!IsPostBack)
            {
                if(siteInfo.NeedShipToAddr && (string.IsNullOrWhiteSpace(_UserInfo.UserName) ||
                   string.IsNullOrWhiteSpace(_UserInfo.Mobile) ||
                   string.IsNullOrWhiteSpace(_UserInfo.Department) ||
                   string.IsNullOrWhiteSpace(_UserInfo.Section)))
                {
                    Response.Redirect("Account\\UserInfo.aspx?msg=1");
                    return;
                }

                //多语言不显示
                Master.ShowDefault(true, true);
                Master.MasterHome();

                BLL.User.UpdateLanguage(_UserInfo.UserID, SysConfig.UserLanguage.ToString());
                 
                //SysConfig.UserLanguage
                //var classGUID = WebHelper.GetQueryString("ClassGUID"); 18-7-17 Header.ascx.cs
                var keyword = WebHelper.GetQueryString("Keyword");
                var sort = WebHelper.GetQueryInt("Sort");
                var customID = WebHelper.GetQueryInt("CustomID");

                SplitPages = siteInfo.IsPaging;
                //steve.weng 2018-7-3
                //if (string.IsNullOrWhiteSpace(classGUID) && siteInfo.ShowByClass)
                //    classGUID = Master.GetDefaultClass();

                //2018-10-11 不用控制是否显示产品分类名
                //if (siteInfo.ShowByClass)
                {
                    itemimg = (Master.FindControl("header") as Luoyi.Web.Controls.Header).pagePath; //18-11-16 Default.aspx不会转到其它页面
                    // classGUID =  Master.SetTitleByClass(classGUID,out itemimg);//18-7-7 
                }
                
                var itemFilter = new ItemFilter()
                {
                    PageIndex = 1,
                    PageSize = SplitPages ? 10 : -1,
                    ClassGUID = (Master.FindControl("header") as Luoyi.Web.Controls.Header).classGUID,//classGUID, 18-7-17
                    Keyword = keyword,
                    SiteGUID = _UserInfo.SiteGUID,
                    PriceType = "Sales",
                    //UserType = siteInfo.UserType,
                    //ItemSortNbrs = string.Join(",",listSortNbrs)
                };

                //18-11-16 Default.aspx不会转到其它页面
                if (!string.IsNullOrWhiteSpace(itemimg))
                {
                    Response.Redirect("~/OtherPages/" + itemimg + "?ClassGUID="
                        + itemFilter.ClassGUID);
                    return;
                }

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

                DateTime firstBookingDate = DateTime.Now.AddDays(siteInfo.DeliveryDays).Date;
                List<DateTime> listWorkDates = CalendarsHelper.GetWorkDates(_UserInfo.BUGUID, _UserInfo.SiteGUID,
                    siteInfo.LaunchDate, firstBookingDate.AddDays(14));
                firstBookingDate = listWorkDates.Where(q => q >= firstBookingDate).Min(q => q);

                DataTable dt = SetData(BLL.Item.GetPage(itemFilter,SysConfig.UserLanguage.ToString(), out recordCount),listWorkDates,firstBookingDate);

                int pages = (recordCount % itemFilter.PageSize == 0) ? (recordCount / itemFilter.PageSize) : (recordCount / itemFilter.PageSize + 1);
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("<section id=\"itemlist\" data-pageindex=\"{0}\" data-classguid=\"{1}\" data-pagemore=\"{2}\" data-customid=\"{3}\" data-keyword=\"{4}\">", 
                    itemFilter.PageIndex + 1, itemFilter.ClassGUID, itemFilter.PageIndex < pages ? "true" : "false", customID, itemFilter.Keyword);

                bool isDinnerSet = false;
                // 显名套餐
                if (dt.Select("itemtype='Set'").Length > 0) isDinnerSet = true;

                ////if(!isDinnerSet)
                //{
                //    Master.HidBack();
                //    Master.MasterHome();
                //}
                
                #region 生成界面
                foreach (DataRow dr in dt.Select("", "classguid,requireddate,dayofweek,sort"))
                {
                    sb.Append("<div class=\"goodsList\"><a ");

                    if(siteInfo.ShowProductInfoInDetail)
                        sb.AppendFormat("href=\"./ItemDetail.aspx?ItemID={0}\"", dr["ItemID"].ToString());
                    sb.AppendFormat("><img class=\"detail-img\" src=\"{0}\" /></a>",
                        System.IO.Path.Combine(ConfigurationManager.AppSettings["ItemImagesURL"], dr["Image1"].ToString()));

         
                        //sb.AppendFormat("<a href=\"/Item{2}Detail.aspx?ItemID={0}\"><img class=\"detail-img\" src=\"{1}\" /></a>",
                        //    dr["ItemID"].ToString(), dr["Image1"].ToString(), isDinnerSet ? "Set":"");


                    //sb.AppendFormat("<p class=\"goods{1}-title\">{0}</p>", dr["ItemName"].ToString(), isDinnerSet ? "et" : "");

                    sb.AppendFormat("<p class=\"goods-title\">{0}</p>", dr["ItemName"].ToString());
                    
                    sb.Append("<p class=\"goods-sku\">");
                    sb.AppendFormat("<span><img src=\"./img/icon/icon-potion_{0}.jpg\" /><small>{0} {1}</small></span>",
                        dr["DishSize"].ToString().Trim()=="1"? "1" : "2",HtmlLang.Lang("PotionNumber","人份","/Default.aspx"));
                    
                    if (dr["RequiredDate"].ToString() != "") //有确定的定货日
                        sb.AppendFormat("<span style=\"float:right;margin-right:0rem;\">{0}</span>",
                           DateTime.Parse(dr["RequiredDate"].ToString()).WeekDay(SysConfig.UserLanguage.ToString()));
                    //DateTime.Compare( DateTime.Parse(dr["RequiredDate"].ToString()), DateTime.Now.AddDays(7- DateTime.Now.DayOfWeek.ToInt())) > 0 ? 
                    //(SysConfig.UserLanguage == SysConfig.LanguageType.ZH_CN ? "下" : "Next") : "",
                    //DateTime.Parse(dr["RequiredDate"].ToString()).ToString("ddd", 
                    //new System.Globalization.CultureInfo(SysConfig.UserLanguage.ToString().Replace('_','-'))));
                    else if (!isDinnerSet)   //非套餐
                        sb.AppendFormat("<span><img src=\"./img/icon/icon-weight_07.jpg\" /><small>{0}g</small></span>", dr["Weight"].ToString());

                    sb.Append("</p><p class=\"goods-num\">");

                    if(siteInfo.ShowPrice)
                        sb.AppendFormat("<span>￥{0}</span>", dr["ItemPrice"].ToDecimal().ToString("G0"));

                    bool booked = false;
                    if(siteInfo.DailyMaxOrderQty > 0)
                        booked = UserHelper.GetBookingQty(_UserInfo, firstBookingDate) >= siteInfo.DailyMaxOrderQty;

                    if ((!dr.IsNull("RequiredDate") &&  //有确定的定货日                    
                        DateTime.Compare(DateTime.Parse(dr["RequiredDate"].ToString()).Date, firstBookingDate) == 0) //是明天定货
                        || (!dr.IsNull("dayofweek") && firstBookingDate.DayOfWeek.ToInt() == dr.Field<int>("dayofweek")))
                            sb.AppendFormat("<span {0}><small>不可订</small></span>",
                                booked ? "" : "style='display:none;'");
                    //else
                    //    sb.AppendFormat("<span><small>{0}</small></span>",
                    //        DateTime.Parse(dr["RequiredDate"].ToString()).WeekDay(SysConfig.UserLanguage.ToString()));
                    //DateTime.Compare( DateTime.Parse(dr["RequiredDate"].ToString()), DateTime.Now.AddDays(7- DateTime.Now.DayOfWeek.ToInt())) > 0 ? 
                    //(SysConfig.UserLanguage == SysConfig.LanguageType.ZH_CN ? "下" : "Next") : "",
                    //DateTime.Parse(dr["RequiredDate"].ToString()).ToString("ddd", 
                    //new System.Globalization.CultureInfo(SysConfig.UserLanguage.ToString().Replace('_','-'))));

                    if ((dr.IsNull("RequiredDate") && dr.IsNull("dayofweek")) ||
                        (!dr.IsNull("RequiredDate") && DateTime.Compare(DateTime.Parse(dr["RequiredDate"].ToString()).Date, firstBookingDate) == 0)  //循环
                        || (!dr.IsNull("dayofweek") && dr.Field<int>("dayofweek") == firstBookingDate.DayOfWeek.ToInt() ) )
                        sb.AppendFormat("<img class=\"addCart\" data-cartadd=\"true\" data-itemid=\"{0}\" src=\"./img/icon/addCart_07.jpg\" {1} {2}/>",
                            dr["ItemID"], 
                            (!dr.IsNull("RequiredDate") && booked) || (!dr.IsNull("dayofweek") && booked) ? "style='display:none;'" : "", 
                            siteInfo.DailyMaxOrderQty==0 ? "" : "LimitQty");
                                           
                    sb.Append("</p></div>");
                }         

                //foreach (DataRow dr in dt.Rows)
                //{
                //    sb.Append("<div class=\"goodsList\">");
                //    sb.AppendFormat("<a href=\"/ItemDetail.aspx?ItemID={0}\"><img class=\"detail-img\" src=\"{1}\" /></a>", dr["ItemID"].ToString(), dr["Image1"].ToString());
                //    sb.AppendFormat("<p class=\"goods-title\">{0}{1}</p>", dr["ItemName"].ToString(), (string.IsNullOrWhiteSpace(dr["HolidayName"].ToString()) ? "" : "<span class=\"holiday\">" + dr["HolidayName"].ToString().Trim() + "</span>"));
                //    sb.Append("<p class=\"goods-sku\">");
                //    sb.AppendFormat("<span><img src=\"/img/icon/icon-piece_06.jpg\" /><small>{0}</small></span>", dr["DishSize"].ToString());
                //    sb.AppendFormat("<span><img src=\"/img/icon/icon-weight_07.jpg\" /><small>{0}g</small></span>", dr["Weight"].ToString());
                //    sb.Append("</p>");
                //    sb.AppendFormat("<p class=\"goods-num\"><span>￥{0}</span><img class=\"addCart\" data-cartadd=\"true\" data-itemid=\"{1}\" src=\"/img/icon/addCart_07.jpg\" /></p>", dr["ItemPrice"].ToDecimal().ToString("G0"), dr["ItemID"].ToString());
                //    sb.Append("</div>");
                //}
                sb.Append("</section>");

                ltlList.Text = sb.ToString();

            }
            #endregion
        }
        //按组循环显示菜品

        protected void testwechat()
        {
            RequestHandler requestHandler = new RequestHandler(HttpContext.Current);
            requestHandler.Init();
            requestHandler.SetParameter("mch_id", ConfigurationManager.AppSettings["WeChatMchID"]);
            requestHandler.SetParameter("nonce_str", TenPayV3Util.GetNoncestr());
            string sign = requestHandler.CreateMd5Sign("key", ConfigurationManager.AppSettings["WeChatKey"]);
            requestHandler.SetParameter("sign", sign);
            string data = requestHandler.ParseXML();
            var result = TenPayV3.testwechat(data);

        }
        private DataTable SetData(DataTable data, List<DateTime> WorkDates, DateTime firstBookingDate)
        {
            DataTable dt = data.Copy();
            dt.Columns.Add("RequiredDate", typeof(DateTime));
            dt.Columns.Add("id", typeof(int));
           // return dt;
            if (dt.AsEnumerable().Any(q => q.IsNull("sortnbr") )) return dt; //没有循环|| DateTime.Compare(siteInfo.LaunchDate, DateTime.Parse("2017-1-1")) > 0)

           
            //按不同的分类有不同的循环
            foreach (DataRow dr in dt.AsEnumerable().Where(q => !q.IsNull("sortnbr")).Distinct(q => q.Field<string>("classguid")))
            {
                var qry = dt.AsEnumerable().Where(q => !q.IsNull("sortnbr") && q.Field<string>("classguid").ToString() == dr["classguid"].ToString());
                int xcount = qry.Distinct(q => q.Field<int>("sortnbr")).Count();
                int xShowDays = Math.Min(xcount, siteInfo.ShowDays);

                int xx = WorkDates.Count(q => q < firstBookingDate);
                WorkDates = WorkDates.Where(q => q >= firstBookingDate).OrderBy(q => q).Take(xShowDays).ToList();
                List<int> listSortNbrs = WorkDates.Select(q => ++xx % xcount).ToList();

                //for (int i = listWorkDates.Count - xShowDays; i < listWorkDates.Count; i++)
                //    listSortNbrs.Add(i % xcount);

                //listWorkDates = listWorkDates.OrderByDescending(q => q).Take(xShowDays).OrderBy(q => q).ToList<DateTime>();

                if (listSortNbrs != null && listSortNbrs.Count != 0) //关联日期
                {
                    int x = 0;
                    qry.OrderBy(q => q.Field<int>("sortnbr")).Aggregate((q1, q2) =>
                    {
                        if (x == 0) q1.SetField<int>("id", x);
                        if (q1.Field<int>("sortnbr").ToInt() != q2.Field<int>("sortnbr").ToInt())
                            x++;
                        q2.SetField<int>("id", x);
                        return q2;
                    });
                    DataTable ddtt = qry.CopyToDataTable();
                    for (int i = 0; i < listSortNbrs.Count; i++)
                    {
                        foreach (DataRow q in qry.Where(d => !d.IsNull("id") && d.Field<int>("id").ToInt() == listSortNbrs[i]))
                            q["RequiredDate"] = WorkDates[i];
                    }
                }
            }

            foreach (DataRow dr in dt.Select("sortnbr is not null and requireddate is null"))
                dt.Rows.Remove(dr);


            return dt;
        }

        
    }
}