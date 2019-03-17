using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Entity;
using Luoyi.Common;

namespace Luoyi.Web.Account
{
    public partial class OrderHistory : PageBase
    {
        public bool todayWorking = false;
        public bool tomorrowWorking = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.MasterAccount();
                rptList.DataSource = BLL.SaleOrder.GetList(string.Format(" AND UserID={0} AND IsPaid = 1 AND shippeddate is not null and shippeduser is not null order by orderid desc", _UserInfo.UserID));//Status = {1}, (int)SaleOrderInfo.StatusEnum.YFH));
                rptList.DataBind();
                CalendarsHelper.GetWorking(_UserInfo.SiteGUID, _UserInfo.BUGUID, out todayWorking, out tomorrowWorking);
            }
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptItem = e.Item.FindControl("rptItem") as Repeater;

                var info = e.Item.DataItem as SaleOrderInfo;

                rptItem.DataSource = BLL.SaleOrderItem.GetTable(string.Format(" AND SOGUID = '{0}'", info.GUID),SysConfig.UserLanguage.ToString());
                rptItem.DataBind();
            }
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Again"))
            {
                var orderID = e.CommandArgument.ToString().ToInt32();
                var orderInfo = BLL.SaleOrder.GetInfo(orderID);
                var listOrderItem = BLL.SaleOrderItem.GetList(string.Format(" AND i.SOGUID = '{0}'", orderInfo.GUID));

                var info = new SaleOrderInfo();
                info.GUID = Guid.NewGuid().ToString("N");
                info.OrderCode = string.Concat(DateTime.Now.ToString("yyMMddHHmm"), StringHelper.GetRandCode(4, StringHelper.RandCodeType.NUMBER));
                info.UserID = orderInfo.UserID;
                info.SiteGUID = orderInfo.SiteGUID;
                info.OrderTime = DateTime.Now;
                info.OrderDate = DateTime.Now.ToInt();

                if (todayWorking && tomorrowWorking)
                {
                    info.RequiredDate = DateTime.Now.Hour < 11 ? DateTime.Now : DateTime.Now.AddDays(1);
                }
                else
                {
                    info.RequiredDate = DateTime.Now.AddDays(1);
                }

                info.TotalAmount = orderInfo.TotalAmount;
                info.CouponCode = string.Empty;
                info.CouponAmount = 0;
                info.PaymentAmount = orderInfo.PaymentAmount;
                info.PaymentID = orderInfo.PaymentID;
                info.IsPaid = false;
                info.PaidTime = DateTime.MinValue;
                info.PayTransCode = string.Empty;
                info.Status = (int)SaleOrderInfo.StatusEnum.WFH;
                info.IsDel = false;
                info.ShippedDate = DateTime.MinValue;

                var listItem = new List<SaleOrderItemInfo>();

                foreach (var itemInfo in listOrderItem)
                {
                    var item = new SaleOrderItemInfo();
                    item.GUID = Guid.NewGuid().ToString("N");
                    item.UserID = itemInfo.UserID;
                    item.SOGUID = info.GUID;
                    item.ItemGUID = itemInfo.ItemGUID;
                    item.UOMGUID = itemInfo.UOMGUID;
                    item.Qty = itemInfo.Qty;
                    item.Price = itemInfo.Price;
                    item.CreateTime = DateTime.Now;
                    item.ShippingStatus = info.Status;
                    item.ShippedDate = 0;
                    item.IsComment = false;
                    item.IsPrint = false;

                    listItem.Add(item);
                }

                orderID = BLL.SaleOrder.Add(info, listItem);

                if (orderID > 0)
                {
                    Response.Redirect(string.Format("/PayMent.aspx?OrderID={0}", orderID));
                }
                else
                {
                    JavaScriptHelper.Show(this.Page, "订单提交失败");
                }
            }
        }
    }
}