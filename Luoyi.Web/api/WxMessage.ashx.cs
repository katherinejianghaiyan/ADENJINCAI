using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Web;

namespace Luoyi.Web.api
{
    /// <summary>
    /// WxMessage 的摘要说明
    /// </summary>
    public class WxMessage : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            try
            {
                Stream stream = context.Request.InputStream;
                StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("UTF-8"));
                string sJson = sr.ReadToEnd();

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                WxMsgData jdata = serializer.Deserialize<WxMsgData>(sJson);
                DateTime sendDate = DateTime.Parse(jdata.date);

                if (sendDate > DateTime.Now || (DateTime.Now - sendDate).TotalHours > 48) context.Response.Write("Date error");
                
                context.Response.Write(WxMessageHelper.SendText(jdata.WechatIDs, jdata.msg));
            }
            catch (Exception e)
            {
                context.Response.Write(e.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class WxMsgData
    {
        public string[] WechatIDs { get; set; }
        public string msg { get; set; }
        public string date { get; set; }
    }
}