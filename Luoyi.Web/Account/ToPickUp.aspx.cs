using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Entity;
using System.Data;
using Luoyi.Common;

namespace Luoyi.Web.Account
{
    public partial class ToPickUp : PageBase
    {
  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                Master.MasterConcept();

                List<SaleOrderInfo> list = BLL.SaleOrder.GetList(string.Format(" AND UserID = {0} AND Status={1} AND IsPaid = 1 AND {2}", _UserInfo.UserID, (int)SaleOrderInfo.StatusEnum.WFH, string.Format("DATEDIFF(day,'{0}',RequiredDate) >= 0", DateTime.Now)));
                rptList.DataSource = list;
                rptList.DataBind();
            }
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item) return;
            
            Image imgBarCode = e.Item.FindControl("imgBarCode") as Image;
            Repeater rptItem = e.Item.FindControl("rptItem") as Repeater;
            var info = e.Item.DataItem as SaleOrderInfo;            
                
            imgBarCode.ImageUrl = string.Format("../api/WxBarCode.ashx?{2}&OrderCode={0}&BarcodeOfSO={1}",
                info.OrderCode, siteInfo.BarcodeOfSO,Guid.NewGuid());
            imgBarCode.Style.Add("width", siteInfo.BarcodeOfSO.Trim().ToLower() == "qrcode"? "30%" :"100%");

            //e.Item.FindControl("lbtnSend").Visible = siteInfo.BarcodeOfSO.Trim().ToLower() != "qrcode";

            DataTable dt = BLL.SaleOrderItem.GetTable(string.Format(" AND SOGUID = '{0}'", info.GUID),SysConfig.UserLanguage.ToString());
            rptItem.DataSource = dt;
            rptItem.DataBind();
           
            if (dt == null || dt.Rows.Count == 0) return;

            #region 取消订单

            if (!checkTime(info.RequiredDate)) return; //过了截止时间
            var obj = e.Item.FindControl("lbtnDelete");
            if (!(obj is LinkButton)) return;
            
            if (!siteInfo.PaymentMethod.Equals("POD") || !siteInfo.CanDelOrder ) return;

            ((LinkButton)obj).CommandArgument =  string.Format("{{'SOGUID': '{0}','OrderCode': '{1}','RequiredDate': '{2}'}}",
                info.GUID, info.OrderCode, info.RequiredDate.Date); 
            obj.Visible = true;
            #endregion
        }

        private bool checkTime(DateTime OrderDate)
        {
            DateTime deadline = OrderDate.Date.AddDays(-1 * DeliveryDays).Add(EndHour.TimeOfDay);
            if (DateTime.Compare(DateTime.Now, deadline) > 0) return false;

            return true;


        }
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Send"))
            {
                 WxMessageHelper.SendNews(_UserInfo.WechatID, e.CommandArgument.ToString(), siteInfo.BarcodeOfSO, SysConfig.UserLanguage.ToString(), siteInfo.ShowPrice);
                return;
            }

            if (e.CommandName.Equals("Delete"))
            {
                try
                {
                    Dictionary<string, string> list = e.CommandArgument.ToString().JSONDeserialize<Dictionary<string, string>>();
                    if (list == null || list.Count == 0) throw new Exception(HtmlLang.Lang("NoOrderDel"));// No order can be deleted");

                    if (!checkTime(DateTime.Parse(list["RequiredDate"])))
                        throw new Exception(HtmlLang.Lang("DelOrderDeadline"));// "Can't delete after deadline");
                    if(!BLL.SaleOrder.UpdateIsDel(list["SOGUID"], true))
                        throw new Exception("Can't delete");

                    JavaScriptHelper.Show(this,string.Format(HtmlLang.Lang("DeletedOrder"), list["OrderCode"]));
                    Response.AddHeader("Refresh", "0");
                }
                catch(Exception ee)
                {
                    JavaScriptHelper.ShowAndBack(ee.Message);
                }
                
            }
        }
    }
}