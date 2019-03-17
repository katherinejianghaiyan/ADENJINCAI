using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luoyi.Web.api
{
    /// <summary>
    /// SendWeiXin 的摘要说明
    /// </summary>
    public class SendWeiXin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.HttpMethod == "GET")
            {
                if (context.Request.QueryString["token"] != "adenservices")
                {
                    context.Response.Write("Token不正确");
                }
                else
                {
                    string openId = context.Request.QueryString["openid"];
                    string message = "您的订单已备好,请取货, 取货日期:" + DateTime.Now.ToString("yyyy-MM-dd");
                    if (WxMessageHelper.SendText(openId, message)) context.Response.Write("ok");
                    else context.Response.Write("error");
                }
            }
            else context.Response.Write("请求类型错误");
            context.Response.End();
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