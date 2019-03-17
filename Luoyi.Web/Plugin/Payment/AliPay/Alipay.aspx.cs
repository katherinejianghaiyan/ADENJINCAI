using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.Web.Plugin.Payment.AliPay
{
    public partial class Alipay : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Logger.Info("Request.UrlReferrer.AbsoluteUri" + Request.UrlReferrer.AbsoluteUri);

            if (!IsPostBack)
            {
                ////////////////////////////////////////////请求参数////////////////////////////////////////////
                int orderID = WebHelper.GetQueryInt("orderID");
                SaleOrderInfo info = BLL.SaleOrder.GetInfo(orderID, _UserInfo.UserID);

                if (info == null)
                {
                    Response.Redirect("/cart.html");
                    Response.End();
                }

                var paytimeout = _UserInfo.TimeOut <= 5 ? "5m" : string.Concat(_UserInfo.TimeOut, "m");

                //商户订单号，商户网站订单系统中唯一订单号，必填
                string out_trade_no = string.Concat(info.Code, info.OrderCode);

                //订单名称，必填
                string subject = string.Concat(info.Code, info.OrderCode);

                //付款金额，必填
                string total_fee = info.PaymentAmount.ToString("F2");

                //收银台页面上，商品展示的超链接，必填
                string show_url = string.Format("http://{0}/PayMent.aspx?OrderID={1}&Back=1", HttpContext.Current.Request.Url.Host, orderID);

                //商品描述，可空
                string body = string.Empty;


                ////////////////////////////////////////////////////////////////////////////////////////////////

                //把请求参数打包成数组
                SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
                sParaTemp.Add("partner", Config.partner);
                sParaTemp.Add("seller_id", Config.seller_id);
                sParaTemp.Add("_input_charset", Config.input_charset.ToLower());
                sParaTemp.Add("service", Config.service);
                sParaTemp.Add("payment_type", Config.payment_type);
                sParaTemp.Add("notify_url", Config.notify_url);
                sParaTemp.Add("return_url", Config.return_url);
                sParaTemp.Add("out_trade_no", out_trade_no);
                sParaTemp.Add("subject", subject);
                sParaTemp.Add("total_fee", total_fee);
                sParaTemp.Add("show_url", show_url);
                sParaTemp.Add("body", body);
                sParaTemp.Add("it_b_pay", paytimeout);
                //其他业务参数根据在线开发文档，添加参数.文档地址:https://doc.open.alipay.com/doc2/detail.htm?spm=a219a.7629140.0.0.2Z6TSk&treeId=60&articleId=103693&docType=1
                //如sParaTemp.Add("参数名","参数值");

                //建立请求
                // string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");

                ltlAliPay.Text = string.Format("<iframe src=\"https://mapi.alipay.com/gateway.do?{0}\" name=\"frmalipay\" id=\"frmalipay\"  scrolling=\"no\" frameborder=\"0\" onload=\"loaded(this)\"></iframe>", Submit.BuildRequestParaToString(sParaTemp, Encoding.UTF8));
            }

        }
    }
}