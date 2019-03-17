using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;
using Luoyi.Entity;
using Luoyi.Common;

namespace Luoyi.Web
{
    public partial class MyCart : PageBase
    {
        protected static List<SiteAddrs> lstSiteAddrs = new List<SiteAddrs>();
        protected string commentsText = Luoyi.Web.HtmlLang.Lang(@"WriteDownCommens", @"请写下您的要求");
        private static string fieldNameAddr1 = string.Empty;
        private static string fieldNameAddr2 = string.Empty;
        private static string fieldNameAddr3 = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsPostBack)
            {
                Master.MasterCart();
                Bind();
            }
        }

        protected void Bind()
        {
            CouponHelper.RemoveOrderCoupon(_UserInfo.UserID);

            CartHelper cart = new CartHelper(_UserInfo);

            decimal total = cart.GetTotal();

            ltlMyCart.Text = cart.GetMyCart(siteInfo.DailyMaxOrderQty == 0,siteInfo.ShowPrice);
            ltlPromotionItem.Text = cart.GetMyPromotion();

            var listRule = BLL.CouponRule.GetList(_UserInfo.BUGUID);

            var couponInfos = BLL.CouponRelease.GetInfo(_UserInfo.BUGUID);

            ltlCoupon.Text = couponInfos != null ? couponInfos.Infos : "";

            var couponInfo = CouponHelper.GetCoupon();
            decimal couponAmount = 0;
            if (couponInfo == null)
            {
                //默认优惠券
                couponInfo = BLL.UserCoupon.GetDefaultUseCouponInfo(_UserInfo.UserID, _UserInfo.BUGUID, total);
                if (couponInfo != null)
                {
                    CouponHelper.RememberCoupon(couponInfo);
                    //是否可以向下抵扣
                    if (couponInfo.IsUseDown)
                    {
                        //是否存在向下抵扣规则
                        var rule = BLL.CouponRule.GetInfo(_UserInfo.BUGUID, total);

                        if (rule != null)
                        {
                            if (couponInfo.Amount == rule.DeductAmt)
                            {
                                couponAmount = couponInfo.Amount;
                                total -= couponInfo.Amount;
                            }
                            else if (couponInfo.Amount > rule.DeductAmt)
                            {
                                couponAmount = rule.DeductAmt;
                                total -= rule.DeductAmt;
                            }
                        }
                    }
                    else
                    {
                        couponAmount = couponInfo.Amount;
                        total -= couponInfo.Amount;
                    }
                }
            }
            else
            {

                //是否可以向下抵扣
                if (couponInfo.IsUseDown)
                {
                    //是否存在向下抵扣规则
                    var rule = BLL.CouponRule.GetInfo(_UserInfo.BUGUID, total);

                    if (rule != null)
                    {
                        if (couponInfo.Amount == rule.DeductAmt)
                        {
                            couponAmount = couponInfo.Amount;
                            total -= couponInfo.Amount;
                        }
                        else if (couponInfo.Amount > rule.DeductAmt)
                        {
                            couponAmount = rule.DeductAmt;
                            total -= rule.DeductAmt;
                        }
                    }
                }
                else
                {
                    couponAmount = couponInfo.Amount;
                    total -= couponInfo.Amount;
                }
            }

            ltlCouponAmount.Text = couponAmount.ToString("G0");
            ltlTotal.Text = string.Format("{0}: ￥{1}", Luoyi.Web.HtmlLang.Lang("TOTAL", "总计"), total.ToString("G0"));

            if (cart.listCart == null || cart.listCart.Count == 0)// || total == 0)
            {
                lbtnWechatPay.Enabled = false;
                //lbtAliPay.Enabled = false;
            }

            #region 修改日前选择，支付 steve weng 2017-5-31 
            if (IsPostBack) return;
            string date1 = "";
            string date2 = "";
            string workingPeriod = "";
            bool isWorking = CalendarsHelper.IsWorking(_UserInfo.SiteGUID, _UserInfo.BUGUID, out date1, out date2, out workingPeriod);
            
            bool todayWorking = date1 != "" && DateTime.Parse(date1).Date == DateTime.Now.Date;
            bool tomorrowWorking = date2 != "" && DateTime.Parse(date2).Date == DateTime.Now.AddDays(1).Date;

            #region 修改2017-8-3
            // rbtnToday.Text = date1;
            // rbtnTomorrow.Text = date2;
            //CalendarsHelper.GetWorking(_UserInfo.SiteGUID, _UserInfo.BUGUID, out todayWorking, out tomorrowWorking);


            // todayWorking = true;
            // tomorrowWorking = true;

            /*
            DateTime EndHour = DateTime.MinValue;
            int DeliveryDays = 0;

            var buInfo = BLL.BU.GetInfo(_UserInfo.BUGUID);
            var siteInfo = BLL.Site.GetInfo(_UserInfo.SiteGUID);

            if (buInfo != null)
            {
                DeliveryDays = buInfo.DeliveryDays;
                //Logger.Info(string.Format("buInfo.EndHour:{0}", buInfo.EndHour));

                EndHour = Convert.ToDateTime(buInfo.EndHour);
            }

            //如果Site上定义截止时间和发货天数，以Site的定义为主 steve.weng 2017-6-2
            if(siteInfo != null)
            {
                if (siteInfo.EndHour != null && siteInfo.EndHour != "") EndHour = Convert.ToDateTime(siteInfo.EndHour);
                if (siteInfo.DeliveryDays != -1) DeliveryDays = siteInfo.DeliveryDays;

            }
            
            //Logger.Info(string.Format("_UserInfo.EndHour:{0}", _UserInfo.EndHour));

            //Logger.Info(string.Format("EndHour:{0}", EndHour.ToShortTimeString()));

            //没有设置时间 默认为11点
            EndHour = EndHour == DateTime.MinValue ? DateTime.Now.Date.AddHours(11) : EndHour;
            */
            //Logger.Info(string.Format("EndHour:{0}", EndHour.ToShortTimeString()));

            //Logger.Info(string.Format("todayWorking:{0};tomorrowWorking:{1};", todayWorking, tomorrowWorking));      
            #endregion

            //今天上班，且没过截止时间
            todayWorking = todayWorking && DeliveryDays == 0 && (DateTime.Now.TimeOfDay <= EndHour.TimeOfDay);
            tomorrowWorking = tomorrowWorking && (DeliveryDays == 0 || (DeliveryDays == 1 && (DateTime.Now.TimeOfDay <= EndHour.TimeOfDay)));

            //仅第二天取货
            if (!tomorrowWorking && DeliveryDays == 1 && DateTime.Now.TimeOfDay <= EndHour.TimeOfDay && date2 != "")
                tomorrowWorking = true;

            //有营业时间限制时，只能选今天 2018-6-13
            if (!string.IsNullOrWhiteSpace(workingPeriod)) tomorrowWorking = false;

            phToday.Visible = todayWorking;
            
            //明天
            phTomorrow.Visible = tomorrowWorking;
            lblTomorrow.Text = DateTime.Parse(date2).Equals(DateTime.Now.AddDays(1).Date) ? HtmlLang.Lang("TOMORROW", "明天") :
                DateTime.Parse(date2).WeekDay(SysConfig.UserLanguage.ToString());
            lblTomorrow.ToolTip = date2;
            //日前默认选择
            rbtnToday.Checked = todayWorking;
            rbtnTomorrow.Checked = !todayWorking && tomorrowWorking;
           // 

            hlShopping.Visible = (todayWorking || tomorrowWorking);
            lbtnWechatPay.Visible = (todayWorking || tomorrowWorking) && isWorking; //营业时间，或没有营业时间限制
            //ShowDinnerType();
            #endregion

            #region 修改工作日 steve weng 2017-5-31
            /*
            //今天明天都是工作日
            if (todayWorking && tomorrowWorking)
            {
                if (DateTime.Now.TimeOfDay >= EndHour.TimeOfDay)
                {
                    rbtnToday.Enabled = false;
                    phToday.Visible = false;
                    rbtnTomorrow.Checked = true;
                }
                else
                {
                    rbtnToday.Checked = true;
                }
            }
            else if (todayWorking && !tomorrowWorking)
            {
                phTomorrow.Visible = false;
                rbtnTomorrow.Enabled = false;
                if (DateTime.Now.TimeOfDay >= EndHour.TimeOfDay)
                {
                    phToday.Visible = false;
                    //lbtAliPay.Visible = lbtnWechatPay.Visible = false;
                }
                else
                {
                    rbtnToday.Checked = true;
                }
            }
            else if (!todayWorking)
            {
                phToday.Visible = false;
               // lbtAliPay.Visible = lbtnWechatPay.Visible = false;
            }
            else if (!tomorrowWorking)
            {
                phTomorrow.Visible = false;
            }
            */
            #endregion

            #region 取得地址下拉框数据 by chris.cao 2018-06-20
            GetSiteAddrs();
            #endregion

        }


        protected void ShowMealTimes()
        {
            try
            {
                if ((!phToday.Visible && !phTomorrow.Visible) || siteInfo.DeliveryTimes == null || !siteInfo.DeliveryTimes.Any())
                    return;
                StringBuilder sb = new StringBuilder();
                sb.Append("<div class=\"mealtimes\">");
                foreach (var q in siteInfo.DeliveryTimes)
                {                 
                    if (q.StartTime >= q.EndTime)
                    {
                        sb.AppendFormat("<span style='padding-left:1.5rem'><input type='radio' value='{0}' name='mealtime' id='{0}'><label for='{0}'>&nbsp;{0}</label></span>", q.Name);
                        continue;
                    }

                    sb.AppendFormat("<div>{0}</div>", q.Name);
                    //需要选择时间

                    for (DateTime time = DateTime.Parse(q.StartTime.ToString("HH:mm"));
                        time <= DateTime.Parse(q.EndTime.ToString("HH:mm")); time = time.AddMinutes(q.TimeStep))
                    {
                        sb.AppendFormat("<span style='padding-left:1.5rem'><input type='radio' value='{0}' name='mealtime' id='{1}'><label for='{1}'>&nbsp;{0}</label></span>",
                            time.ToString("H:mm"),time.ToString("HHmm"));
                    }

                };
                sb.Append("</div>");
                Response.Write(sb.ToString());
            }
            catch { }
        }
        //private void ShowDinnerType()
        //{
        //    try
        //    {
        //        if ((!phToday.Visible && !phTomorrow.Visible) || siteInfo.DeliveryTimes == null || !siteInfo.DeliveryTimes.Any())
        //            throw new Exception();

        //        //string[] listDinner = siteInfo.DinnerTypes.Split(';');
        //        ddlDinnerType.Items.Clear();
        //       foreach(var q in siteInfo.DeliveryTimes)
        //        {
        //            ListItem l1 = new ListItem();
        //            l1.Text = q.Name;
        //            l1.Value = q.Name;
        //            l1.Attributes.CssStyle.Add("color", "brown");
        //            l1.Attributes.CssStyle.Add("font-weight", "bold");
        //            ddlDinnerType.Items.Add(l1);
        //            if (q.StartTime < q.EndTime) //需要选择时间
        //            {
        //                l1.Attributes.Add("disabled", "disabled");

        //                for (DateTime time = DateTime.Parse(q.StartTime.ToString("HH:mm"));
        //                    time <= DateTime.Parse(q.EndTime.ToString("HH:mm")); time = time.AddMinutes(q.TimeStep))
        //                {
        //                    ListItem l2 = new ListItem();
        //                    l2.Text = string.Format("{0}{1}", HttpUtility.HtmlDecode("&emsp;&emsp;"), time.ToString("H:mm"));
        //                    l2.Value = time.ToString("H:mm");
        //                    //l2.Attributes.Add("DeliveryType", q.Name);
        //                    ddlDinnerType.Items.Add(l2);
        //                }
        //            }

        //        };

        //    }
        //    catch
        //    {
        //        ddlDinnerType.Visible = false;
        //    }
        //}

        /// <summary>
        /// 根据SiteGuid取得地址下拉框数据
        /// </summary>
        private void GetSiteAddrs()
        {
            if (!siteInfo.NeedShipToAddr) return;
            // 清空选项
            this.InitDropDownAddrs();
            // ShipToAddr有值时直接显示
            if (!string.IsNullOrWhiteSpace(_UserInfo.ShipToAddr)) return;

            // 根据SiteGuid取得地址下拉框数据
            lstSiteAddrs = BLL.Site.GetAddrs(_UserInfo.SiteGUID);
            if (lstSiteAddrs == null || !lstSiteAddrs.Any()) return;

            var lq = lstSiteAddrs.Distinct(r => r.ShipToAddr1ZHCN);

            ListItem li = new ListItem();
            string text = string.Empty;

            // 多语言
            switch (SysConfig.UserLanguage)
            {
                case SysConfig.LanguageType.ZH_CN://中文
                    fieldNameAddr1 = "ShipToAddr1ZHCN";
                    fieldNameAddr2 = "ShipToAddr2ZHCN";
                    fieldNameAddr3 = "ShipToAddr3ZHCN";
                    break;
                case SysConfig.LanguageType.EN_US://英文
                    fieldNameAddr1 = "ShipToAddr1ENUS";
                    fieldNameAddr2 = "ShipToAddr2ENUS";
                    fieldNameAddr3 = "ShipToAddr3ENUS";
                    break;
            }
            foreach (var q in lq)
            {
                li = new ListItem();

                text = q.GetProperty(fieldNameAddr1);
                if (string.IsNullOrWhiteSpace(text))
                    continue;
                li.Text = text;
                li.Value = text;
                dropDownAddr1.Items.Add(li);
            }
            if (this.dropDownAddr1.Items.Count > 0)
            {
                this.dropDownAddr1.Visible = true;
                this.GetSiteAddr2();
            }
        }

        /// <summary>
        /// 根据Addr1中的值得到Addr2的待选项
        /// </summary>
        private void GetSiteAddr2()
        {
            this.dropDownAddr2.Items.Clear();
            string strAddr1 = this.dropDownAddr1.SelectedValue;

            if (string.IsNullOrWhiteSpace(strAddr1))
            {
                this.dropDownAddr2.Visible = false;
                this.dropDownAddr3.Visible = false;
                return;
            }

            var lq = lstSiteAddrs.Where(r => strAddr1.Equals(r.GetProperty(fieldNameAddr1))).Distinct(r=>r.ShipToAddr2ZHCN);

            ListItem li = new ListItem();
            string text = string.Empty;

            foreach (var q in lq)
            {
                li = new ListItem();

                text = q.GetProperty(fieldNameAddr2);
                if (string.IsNullOrWhiteSpace(text))
                    continue;
                li.Text = text;
                li.Value = text;

                dropDownAddr2.Items.Add(li);
            }
            if (this.dropDownAddr2.Items.Count > 0)
            {
                this.dropDownAddr2.Visible = true;
                this.GetSiteAddr3();
            }
            else
            {
                this.dropDownAddr2.Visible = false;
                this.dropDownAddr3.Visible = false;
            }
        }

        /// <summary>
        /// 根据Addr2中的值得到Addr3的待选项
        /// </summary>
        private void GetSiteAddr3()
        {
            this.dropDownAddr3.Items.Clear();
            string strAddr1 = this.dropDownAddr1.SelectedValue;
            string strAddr2 = this.dropDownAddr2.SelectedValue;

            if (string.IsNullOrWhiteSpace(strAddr2))
            {
                this.dropDownAddr3.Visible = false;
                return;
            }

            var lq = lstSiteAddrs.Where(r => strAddr1.Equals(r.GetProperty(fieldNameAddr1)) && strAddr2.Equals(r.GetProperty(fieldNameAddr2))).Distinct(r => r.ShipToAddr3ZHCN);

            ListItem li = new ListItem();
            string text = string.Empty;

            foreach (var q in lq)
            {
                li = new ListItem();

                text = q.GetProperty(fieldNameAddr3);
                if (string.IsNullOrWhiteSpace(text))
                    continue;
                li.Text = text;
                li.Value = text;

                dropDownAddr3.Items.Add(li);
            }
            if (this.dropDownAddr3.Items.Count > 0)
                this.dropDownAddr3.Visible = true;
            else
                this.dropDownAddr3.Visible = false;
        }

        

        /// <summary>
        /// 初始化地址下拉框
        /// </summary>
        private void InitDropDownAddrs()
        {
            // 隐藏 + 清空选项
            this.dropDownAddr1.Items.Clear();
            this.dropDownAddr2.Items.Clear();
            this.dropDownAddr3.Items.Clear();
            this.dropDownAddr1.Visible = false;
            this.dropDownAddr2.Visible = false;
            this.dropDownAddr3.Visible = false;
        }

        protected void dropDownAddr1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSiteAddr2();
        }
        protected void dropDownAddr2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSiteAddr3();
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                PlaceHolder phQty = e.Item.FindControl("phQty") as PlaceHolder;
                var info = e.Item.DataItem as SaleOrderCartInfo;
                if (!string.IsNullOrEmpty(info.PromotionGuid))
                {
                    phQty.Visible = false;
                }
            }
        }

        protected void CreateOrderAndPay(SaleOrderInfo.PaymentEnum payment)
        {
            CartHelper cart = new CartHelper(_UserInfo, true);

            if (cart.listCart == null || cart.listCart.Count == 0)
            {
                Response.Redirect("~/Default.aspx", true);
            }

            if (rbtnToday.Checked)
            {
                #region steve weng 2017-6-27
                /*
                DateTime EndHour = DateTime.MinValue;

                var buInfo = BLL.BU.GetInfo(_UserInfo.BUGUID);
                if (buInfo != null)
                {
                    EndHour = Convert.ToDateTime(buInfo.EndHour);
                }
                
                EndHour = EndHour == DateTime.MinValue ? DateTime.Now.Date.AddHours(11) : EndHour;
                */
                #endregion
                if (DateTime.Now.TimeOfDay >= EndHour.TimeOfDay)
                {
                    JavaScriptHelper.ResponseScript(this.Page, "$(function(){$(\".weui_dialog_alert .weui_dialog_bd\").html(\"已过截止时间，今天不能取货！请重新确定取货日期\");$(\".weui_dialog_alert\").show().on('click', '.weui_btn_dialog', function () {$('.weui_dialog_alert').off('click').hide();});});");
                    return;
                }
            }

            var info = new SaleOrderInfo();
            info.GUID = Guid.NewGuid().ToString("N");
            info.OrderCode = string.Concat(DateTime.Now.ToString("yyMMddHHmm"), StringHelper.GetRandCode(4, StringHelper.RandCodeType.NUMBER));
            info.UserID = _UserInfo.UserID;
            info.SiteGUID = _UserInfo.SiteGUID;
            info.OrderTime = DateTime.Now;
            info.OrderDate = DateTime.Now.ToInt();

            if (rbtnToday.Checked)
                info.RequiredDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            else
                try
                { info.RequiredDate = DateTime.Parse(lblTomorrow.ToolTip); }
                catch
                { info.RequiredDate = DateTime.Now.AddDays(1); }
            //info.RequiredDate = rbtnToday.Checked ? DateTime.Now : DateTime.Parse(lblTomorrow.ToolTip);//DateTime.Now.AddDays(1);
            //2018-7-20 就餐时间
            string dinnerType = "";
            if (!string.IsNullOrWhiteSpace(hfMealTime.Value))//ddlDinnerType.Visible)
            {
                dinnerType = hfMealTime.Value;//ddlDinnerType.SelectedValue;
                    
                try
                {
                    TimeSpan time = DateTime.Parse(dinnerType).TimeOfDay;
                    info.RequiredDate = DateTime.Parse(string.Format("{0} {1}", info.RequiredDate.ToString("yyyy-MM-dd"), dinnerType));
                    dinnerType = siteInfo.DeliveryTimes.SingleOrDefault(q => q.StartTime.TimeOfDay <= time
                        && q.EndTime.TimeOfDay >= time).Name;
                }
                catch {  }
                    
            
            }
            info.RequiredDinnerType = dinnerType;

            var couponInfo = CouponHelper.GetCoupon();

            if (couponInfo != null)
            {
                info.CouponCode = couponInfo.ID.ToString();
                info.CouponAmount = couponInfo.Amount;
            }
            else
            {
                info.CouponCode = string.Empty;
                info.CouponAmount = 0;
            }
            info.TotalAmount = cart.GetTotal();
            info.PaymentAmount = info.TotalAmount - info.CouponAmount;
            info.PaymentID = (int)payment;
            info.IsPaid = false;
            info.PaidTime = DateTime.MinValue;
            info.PayTransCode = string.Empty;
            info.Status = (int)SaleOrderInfo.StatusEnum.WFH;
            info.IsDel = false;
            info.ShippedDate = DateTime.MinValue;

            #region add by Chris.cao 2018-06-14
            //info.Comments = this.txtComments.Text;
            #endregion

            #region add by chris.cao 2018-06-21
            if (siteInfo.NeedShipToAddr)
            {
                try
                {
                    //具体地址
                    info.ShipToAddr = string.IsNullOrWhiteSpace(_UserInfo.ShipToAddr) ?
                        ((dropDownAddr1.Visible ? dropDownAddr1.SelectedValue : "") +
                         (dropDownAddr2.Visible ? " " + dropDownAddr2.SelectedValue : "") +
                         (dropDownAddr3.Visible ? " " + dropDownAddr3.SelectedValue : ""))
                         : _UserInfo.ShipToAddr;
                }
                catch {   }

                try
                {
                    //写备注
                    info.Comments = this.txtComments.Text;
                }
                catch  { }
            }
            #endregion

            var listItem = new List<SaleOrderItemInfo>();

            foreach (var cartInfo in cart.listCart)
            {
                if (cartInfo.IsBuy)
                {
                    var item = new SaleOrderItemInfo();
                    item.GUID = Guid.NewGuid().ToString("N");
                    item.UserID = _UserInfo.UserID;
                    item.SOGUID = info.GUID;
                    item.ItemGUID = cartInfo.ItemGUID;
                    item.UOMGUID = cartInfo.UOMGUID;
                    item.Qty = cartInfo.Qty;
                    item.Price = cartInfo.Price;
                    item.CreateTime = DateTime.Now;
                    item.ShippingStatus = info.Status;
                    item.ShippedDate = 0;
                    item.IsComment = false;
                    item.IsPrint = false;
                    item.PromotionGUID = cartInfo.PromotionGuid;

                    listItem.Add(item);
                }
            }

            int orderID = BLL.SaleOrder.Add(info, listItem);
            if (orderID > 0)
            {
                if (couponInfo != null)
                {
                    BLL.UserCoupon.UpdateQty(couponInfo.ID, -1);
                    CouponHelper.RemoveCoupon();
                }

                Response.Redirect(string.Format("~/PayMent.aspx?OrderID={0}", orderID));
            }
            else
            {
                JavaScriptHelper.Show(this.Page, "订单提交失败");
            }
        }

        protected void lbtnWechatPay_Click(object sender, EventArgs e)
        {
            #region add by Chris.Cao 2018.06.14
            /*
            if(siteInfo.MustComments && string.IsNullOrWhiteSpace(this.txtComments.Text))
            {
                JavaScriptHelper.Show(this.Page, "请填写备注信息");
                return;
            }*/
            #endregion
            if(siteInfo.DeliveryTimes != null && siteInfo.DeliveryTimes.Any() && string.IsNullOrWhiteSpace(hfMealTime.Value))
            {
                JavaScriptHelper.Show(this.Page,"请选择就餐时间");
                return;
            }
            SaleOrderInfo.PaymentEnum paymentMethod = SaleOrderInfo.PaymentEnum.WechatPay;
            if (!string.IsNullOrEmpty(siteInfo.PaymentMethod) && siteInfo.PaymentMethod.Equals("POD"))
                paymentMethod = SaleOrderInfo.PaymentEnum.Cash;
            CreateOrderAndPay(paymentMethod);
        }

        protected void lbtAliPay_Click(object sender, EventArgs e)
        {
            CreateOrderAndPay(SaleOrderInfo.PaymentEnum.AliPay);
        }
    }
}