using System;
using System.Configuration;
using System.Net;
using System.Threading;
using Luoyi.Common;

namespace Luoyi.Web.Plugin.Payment.Cash
{
    public partial class CashNotify : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string orderCode = Request.Params["Code"];
            Logger.Info("现场支付订单：" + orderCode);
            if (!string.IsNullOrEmpty(orderCode) && BLL.SaleOrder.PaymentSuccess(orderCode.Trim(), "")
                && BLL.SaleOrderCart.EmptyCart(_UserInfo.UserID))
            {
                if (siteInfo.NeedWork)
                    WebHelper.PushWS(siteInfo.GUID);
                // WxMessageHelper.SendNews(BLL.User.GetInfo(_UserInfo.UserID).WechatID, orderCode.Trim(), siteInfo.BarcodeOfSO, SysConfig.UserLanguage.ToString(), siteInfo.ShowPrice);
               
                //完成的订单处理
                Logger.Info("完成现场支付的订单：" + orderCode);

                if (!WxMessageHelper.SendNews(BLL.User.GetInfo(_UserInfo.UserID).WechatID, orderCode.Trim(),
                    siteInfo.BarcodeOfSO, SysConfig.UserLanguage.ToString(), siteInfo.ShowPrice))
                {
                    Logger.Info("现场支付 发送订单信息失败" + orderCode);
                }
                Response.Redirect("~/Default.aspx");
            }          
            
        }

        
        private HttpWebResponse CreateGetHttpResponse(string siteGUID)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["WSURL"].ToString();
                if (string.IsNullOrWhiteSpace(url)) throw new Exception("no url");
                url = string.Format(@"{0}?wskey={1}", url, siteGUID);
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";//链接类型
                return request.GetResponse() as HttpWebResponse;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}