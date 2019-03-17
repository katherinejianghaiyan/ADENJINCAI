using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

using Luoyi.Common;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace Luoyi.Web.api
{
    /// <summary>
    /// Summary description for OfficalAccountQRCode
    /// </summary>
    public class OfficalAccountQRCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string guid = WebHelper.GetQueryString("guid"); 
            string siteGuid = WebHelper.GetQueryString("siteGuid");
            string cardno = WebHelper.GetQueryString("cardno");

            string _appId = ConfigurationManager.AppSettings["WeChatAppID"].ToString(); //换成你的信息
            string _appSecret = ConfigurationManager.AppSettings["WeChatAppSecret"].ToString();//换成你的信息
            
            string _accessToken = AccessTokenContainer.TryGetAccessToken(_appId, _appSecret); //WxHelper.GetAccessToken();
            var createQrCodeResult = QrCodeApi.CreateByStr(_accessToken, 
                string.Format("{0},{1},{2}",siteGuid,cardno,guid));

            context.Response.ContentType = "text/plain";
            string str = QrCodeApi.GetShowQrCodeUrl(createQrCodeResult.ticket);
            context.Response.Write(str);
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