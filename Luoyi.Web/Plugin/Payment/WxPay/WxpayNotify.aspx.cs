using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Collections.Specialized;
using Luoyi.Entity;
using Luoyi.Common;
using Senparc.Weixin.MP.TenPayLibV3;
using System.Configuration;

namespace Luoyi.Web.Plugin.Payment.WxPay
{
    public partial class WxpayNotify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ResponseHandler payNotifyRepHandler = new ResponseHandler(HttpContext.Current);
            payNotifyRepHandler.SetKey(ConfigurationManager.AppSettings["WeChatKey"]);

            //返回的参数
            string return_code = payNotifyRepHandler.GetParameter("return_code");//返回状态码
            string return_msg = payNotifyRepHandler.GetParameter("return_msg");//返回信息

            string xmlModel = "<xml><return_code><![CDATA[{0}]]></return_code><return_msg><![CDATA[{1}]]></return_msg></xml>";

            string xml = string.Format(xmlModel, return_code.ToUpper(), return_msg.ToUpper());

            //支付失败直接返回
            if (return_code.ToUpper() != "SUCCESS")
            {
                Response.Write(xml);
                Response.End();
            }

            //因为微信服务器会多次推送通知到这里，所以需要在这里判断订单是否已经完成支付，如果完成，则不进行下面操作

            string out_trade_no = payNotifyRepHandler.GetParameter("out_trade_no");//商户订单号

            //验证请求是否从微信发过来（安全）
            if (payNotifyRepHandler.IsTenpaySign())
            {
                //正确的订单处理
                Logger.Info("Processing wxpay out_trade_no1：" + out_trade_no);

                try
                {
                    var info = BLL.SaleOrder.GetInfoByOutTradeNo(out_trade_no);

                    if (info != null && !info.IsPaid)
                    {
                        BLL.SaleOrder.PaymentSuccess(info.OrderCode, payNotifyRepHandler.GetParameter("transaction_id"));
                        BLL.SaleOrderCart.EmptyCart(info.UserID);
                        SiteInfo site = BLL.Site.GetInfo(info.SiteGUID, SysConfig.UserLanguage.ToString());
                        if (site.NeedWork)
                            WebHelper.PushWS(site.GUID);

                        //完成的订单处理
                        Logger.Info("Completed wxpay out_trade_no1：" + out_trade_no);

                        if (!WxMessageHelper.SendNews(BLL.User.GetInfo(info.UserID).WechatID, info.OrderCode, site.BarcodeOfSO, SysConfig.UserLanguage.ToString()))
                        {
                            Logger.Info("微信支付 发送订单信息失败" + out_trade_no);
                        }
                    }
                }
                catch(Exception ee)
                {
                    Logger.Info("微信支付失败：" + out_trade_no + " " + ee.Message);
                }
            }
            else
            {
                //错误的订单处理
                Logger.Info("out_trade_no2：" + out_trade_no);
            }

            Response.Write(xml);
            Response.End();
        }
    }
}