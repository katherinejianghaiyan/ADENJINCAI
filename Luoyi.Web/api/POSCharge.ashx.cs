using System.IO;
using System.Web;

namespace Luoyi.Web.api
{
    /// <summary>
    /// Summary description for POSCharge
    /// </summary>
    public class POSCharge : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {            
            //SendLinkToMiniProgam("");
            context.Response.ContentType = "text/plain";
            string s = "";
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                s = reader.ReadToEnd();
            }
            context.Response.Write(s);
            context.Response.Write(context.Request.InputStream);
            //context.Response.Write("Hello World");
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void SendLinkToMiniProgam(string miniProgramName)
        {
            string smsg = "<a data-miniprogram-appid=\"{0}\" data-miniprogram-path=\"{1}\" href=\"http://www.qq.com\">微信充值</a>";
            smsg = string.Format(smsg, "wx838bf2ec23c9c93f", "pages/login/login");
            string wechatId = "owE9CwajFGBr7f3G4yHujpivJCsw";

            WxMessageHelper.SendText(wechatId, smsg);
        }
    }
}