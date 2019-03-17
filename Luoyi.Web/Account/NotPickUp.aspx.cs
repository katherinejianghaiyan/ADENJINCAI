using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Entity;
using Luoyi.Common;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.AdvancedAPIs;
using System.Configuration;
using System.Data;

namespace Luoyi.Web.Account
{
    public partial class NotPickUp : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.MasterMessage();
                rptList.DataSource = BLL.SaleOrder.GetList(string.Format(" AND UserID = {0} AND Status={1} AND IsPaid = 1 AND {2}", _UserInfo.UserID, (int)SaleOrderInfo.StatusEnum.WFH, string.Format("DATEDIFF(day,'{0}',RequiredDate) < 0", DateTime.Now)));
                rptList.DataBind();
            }
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                System.Web.UI.WebControls.Image imgBarCode = e.Item.FindControl("imgBarCode") as System.Web.UI.WebControls.Image;
                Repeater rptItem = e.Item.FindControl("rptItem") as Repeater;
                var info = e.Item.DataItem as SaleOrderInfo;

                imgBarCode.ImageUrl = string.Format("../api/WxBarCode.ashx?{2}&OrderCode={0}&BarcodeOfSO={1}", 
                    info.OrderCode,siteInfo.BarcodeOfSO,Guid.NewGuid());
                imgBarCode.Style.Add("width", siteInfo.BarcodeOfSO.Trim().ToLower() == "qrcode" ? "30%" : "100%");

                e.Item.FindControl("lbtnSend").Visible = siteInfo.BarcodeOfSO.Trim().ToLower() != "qrcode";

                rptItem.DataSource = BLL.SaleOrderItem.GetTable(string.Format(" AND SOGUID = '{0}'", info.GUID));
                rptItem.DataBind();
            }
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Send"))
            {
                WxMessageHelper.SendNews(_UserInfo.WechatID, e.CommandArgument.ToString(),siteInfo.BarcodeOfSO);
            }
        }
    }
}