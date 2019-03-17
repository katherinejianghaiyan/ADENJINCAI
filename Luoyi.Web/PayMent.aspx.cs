using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Luoyi.Common;
using Luoyi.Entity;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.TenPayLibV3;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.CommonAPIs;
using System.Text;

namespace Luoyi.Web
{
    public partial class PayMent : PageBase
    {
        protected string comments = "1、每天{0}点之前支付成功，即可{1}日取货<br>"
            + "2、订单付款成功后，不能取消 <br>"//（若支付时间为11点之后，则默认为次日取货）<br>"
            + "3、建议{2}<br>"
            + "4、请注意温馨提示<br>"
            + "5、保质期为一天<br>"
            + "6、因特殊原因不能按时取货，我们可代为保管不超过24小时（截止第二天18点前），谢绝退货。";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.HidDefault();

                var orderID = WebHelper.GetQueryInt("OrderID");
                var back = WebHelper.GetQueryInt("Back");
                comments = string.Format(comments, EndHour.Hour, 
                    DeliveryDays == 0 ? "当" : "明", PickupTime == "" ? "在标准下班时间提前半小时取货" : string.Format("在{0}取货",PickupTime));
                if (!string.IsNullOrWhiteSpace(siteInfo.PaymentComments))
                    comments = siteInfo.PaymentComments;

                if (back == 1)
                {
                    var sb = new StringBuilder();
                    sb.Append("<script type=\"text/javascript\">\r\n");
                    sb.AppendFormat("top.location.href='/PayMent.aspx?OrderID={0}';\r\n", orderID);
                    sb.Append("</script>\r\n");
                    Response.Clear();
                    Response.Write(sb.ToString());
                    Response.End();
                }

                var info = BLL.SaleOrder.GetInfo(orderID, _UserInfo.UserID);

                if (info != null)
                {
                    ltlOrderCode.Text =  info.OrderCode; //ltlOrderCode1.Text =
                    if(siteInfo.ShowPrice)
                        ltlOrderAmount.Text = info.PaymentAmount.ToString("￥0.##");
                    ltlRecivingParty.Text = BLL.User.GetRealName(info.UserID);
                    #region add by chris.cao 2018-06-21
                    ltlShipToAddr.Text = info.ShipToAddr + info.Comments;
                    #endregion

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

                    hlPayNow.Attributes.Add("data-endhour", (EndHour.Hour * 60 + EndHour.Minute).ToString());
                    hlPayNow.Attributes.Add("data-today", info.RequiredDate.Date == DateTime.Now.Date ? "true" : "false");
                   // testwechat();
                    switch (info.PaymentID)
                    {
                        case (int)SaleOrderInfo.PaymentEnum.AliPay:
                            hlPayNow.Text = HtmlLang.Lang("PayNowAliPay", "支付宝支付", "/PayMent.aspx");
                            hlPayNow.CssClass = "button alipaynow";
                            hlPayNow.Attributes.Add("data-url", string.Format("/Plugin/Payment/AliPay/Alipay.aspx?OrderID={0}", info.OrderID));
                            break;
                        case (int)SaleOrderInfo.PaymentEnum.WechatPay:
                            hlPayNow.Text = HtmlLang.Lang("PayNowWeiXin", "微信支付", "/PayMent.aspx");
                            WeiXinPay(info);
                            break;
                        case (int)SaleOrderInfo.PaymentEnum.Cash:                            
                            hlPayNow.Text = info.PaymentAmount == 0 ? HtmlLang.Lang("Confirm", "确认", "/PayMent.aspx") :
                                HtmlLang.Lang("DeliveryToPay", "现场支付", "/PayMent.aspx");
                            hlPayNow.CssClass = "button cashnow";
                            hlPayNow.Attributes.Add("data-url", 
                                string.Format("./Plugin/Payment/Cash/CashNotify.aspx?code={0}", info.OrderCode));
                            break;
                    }
                }
            }
        }


        protected void WeiXinPay(SaleOrderInfo info)
        {
            TenPayV3Info tenPayV3Info = new TenPayV3Info(ConfigurationManager.AppSettings["WeChatAppID"],
                ConfigurationManager.AppSettings["WeChatAppSecret"], ConfigurationManager.AppSettings["WeChatMchID"]
                     , ConfigurationManager.AppSettings["WeChatKey"], 
                string.Format("{0}{1}", WebHelper.GetUrlApp(),ConfigurationManager.AppSettings["WeChatNotifyUrl"]));


            RequestHandler requestHandler = new RequestHandler(HttpContext.Current);
            requestHandler.Init();

            var nonceStr = TenPayV3Util.GetNoncestr();
            var timeStamp = TenPayV3Util.GetTimestamp();
            var paytimeout = _UserInfo.TimeOut <= 5 ? 5 : _UserInfo.TimeOut;

            //微信分配的公众账号ID（企业号corpid即为此appId）
            requestHandler.SetParameter("appid", tenPayV3Info.AppId);
            //附加数据，在查询API和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据
            requestHandler.SetParameter("attach", "");
            //商品或支付单简要描述
            requestHandler.SetParameter("body", info.Code + info.OrderCode);
            //微信支付分配的商户号
            requestHandler.SetParameter("mch_id", tenPayV3Info.MchId);
            //随机字符串，不长于32位。
            requestHandler.SetParameter("nonce_str", nonceStr);
            //订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010。其他详见时间规则
            //注意：最短失效时间间隔必须大于5分钟
            requestHandler.SetParameter("time_expire", DateTime.Now.AddMinutes(paytimeout).AddSeconds(1).ToString("yyyyMMddHHmmss"));
            //接收微信支付异步通知回调地址，通知url必须为直接可访问的url，不能携带参数。
            requestHandler.SetParameter("notify_url", tenPayV3Info.TenPayV3Notify);
            //trade_type=JSAPI，此参数必传，用户在商户公众号appid下的唯一标识。
            requestHandler.SetParameter("openid", _UserInfo.WechatID);
            //商户系统内部的订单号,32个字符内、可包含字母，自己生成
            requestHandler.SetParameter("out_trade_no", info.Code + info.OrderCode);
            //APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP。
            requestHandler.SetParameter("spbill_create_ip", WebHelper.GetClientIP());
            //订单总金额，单位为分，做过银联支付的朋友应该知道，代表金额为12位，末位分分
            requestHandler.SetParameter("total_fee", (info.PaymentAmount * 100).ToString("G0"));
            //取值如下：JSAPI，NATIVE，APP，我们这里使用JSAPI
            requestHandler.SetParameter("trade_type", TenPayV3Type.JSAPI.ToString());
            string sign = requestHandler.CreateMd5Sign("key", tenPayV3Info.Key);
            requestHandler.SetParameter("sign", sign);
            string data = requestHandler.ParseXML();

            //获取并返回预支付XML信息
            var result = TenPayV3.Unifiedorder(data);

            var res = XDocument.Parse(result);

            UnifiedorderResult resultInfo = new UnifiedorderResult();

            resultInfo.FillEntityWithXml<UnifiedorderResult>(res);

            if (resultInfo.return_code.Equals("SUCCESS"))
            {
                RequestHandler paySignReqHandler = new RequestHandler(HttpContext.Current);
                paySignReqHandler.SetParameter("appId", tenPayV3Info.AppId);
                paySignReqHandler.SetParameter("timeStamp", timeStamp);
                paySignReqHandler.SetParameter("nonceStr", nonceStr);
                paySignReqHandler.SetParameter("package", string.Format("prepay_id={0}", resultInfo.prepay_id));
                paySignReqHandler.SetParameter("signType", "MD5");
                var paySign = paySignReqHandler.CreateMd5Sign("key", tenPayV3Info.Key);


                string ticket = JsApiTicketContainer.TryGetJsApiTicket(tenPayV3Info.AppId, tenPayV3Info.AppSecret, true);
                string signature = JSSDKHelper.GetSignature(ticket, nonceStr, timeStamp, Request.Url.AbsoluteUri.ToString());

                //JSSDKHelper.GetSignature

                hlPayNow.CssClass = "button wxpaynow";
                hlPayNow.Attributes.Add("data-appId", tenPayV3Info.AppId);
                hlPayNow.Attributes.Add("data-timeStamp", timeStamp);
                hlPayNow.Attributes.Add("data-nonceStr", nonceStr);
                hlPayNow.Attributes.Add("data-package", string.Format("prepay_id={0}", resultInfo.prepay_id));
                hlPayNow.Attributes.Add("data-signType", "MD5");
                hlPayNow.Attributes.Add("data-paySign", paySign);
                hlPayNow.Attributes.Add("data-signature", signature);
                hlPayNow.Attributes.Add("data-openid", _UserInfo.WechatID);
                hlPayNow.Attributes.Add("data-url", "javascript:;");
            }
            else
            {
                hlPayNow.Attributes.Add("data-msg", resultInfo.return_msg);
            }
        }
    }
}