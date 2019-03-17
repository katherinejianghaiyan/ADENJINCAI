using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;

namespace Luoyi.Web.api
{
    /// <summary>
    /// WeiXin 的摘要说明
    /// </summary>
    public class WeiXin : IHttpHandler
    {
        public static readonly string Token = WebConfigurationManager.AppSettings["WeChatToken"];//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = WebConfigurationManager.AppSettings["WeChatEncodingAESKey"];//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = WebConfigurationManager.AppSettings["WeChatAppID"];//与微信公众账号后台的AppId设置保持一致，区分大小写。
        
        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";

            string signature = context.Request["signature"];
            string timestamp = context.Request["timestamp"];
            string nonce = context.Request["nonce"];
            string echostr = context.Request["echostr"];

            if (context.Request.HttpMethod == "GET")
            {
                //验证url
                if (CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    context.Response.Write(echostr);
                }
                else
                {
                    context.Response.Write("failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, Token) + "。" +
                                "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
                }
                context.Response.End();
            }
            else
            {
                //post method - 当有用户想公众账号发送消息时触发
                if (!CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    context.Response.Write("参数错误！");
                    context.Response.End();
                }

                //post method - 当有用户想公众账号发送消息时触发
                var postModel = new PostModel()
                {
                    Signature = context.Request.QueryString["signature"],
                    Msg_Signature = context.Request.QueryString["msg_signature"],
                    Timestamp = context.Request.QueryString["timestamp"],
                    Nonce = context.Request.QueryString["nonce"],
                    //以下保密信息不会（不应该）在网络上传播，请注意
                    Token = Token,
                    EncodingAESKey = EncodingAESKey,//根据自己后台的设置保持一致
                    AppId = AppId//根据自己后台的设置保持一致
                };

                var maxRecordCount = 10;

                var messageHandler = new CustomMessageHandler(context.Request.InputStream, postModel, maxRecordCount);

                try
                {
                    //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。
                    //messageHandler.RequestDocument.Save(
                    //    context.Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Request_" +
                    //                   messageHandler.RequestMessage.FromUserName + ".txt"));
                    //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。

                    //messageHandler.RequestDocument.Save(
                    //    context.Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Request_" +
                    //                   messageHandler.RequestMessage.MsgType + ".txt"));

                    //执行微信处理过程
                    messageHandler.Execute();
                    //测试时可开启，帮助跟踪数据

                    //messageHandler.ResponseDocument.Save(
                    //    context.Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Response_" +
                    //                   messageHandler.ResponseMessage.ToUserName + ".txt"));
                    context.Response.Write(messageHandler.ResponseDocument.ToString());


                    return;
                }
                catch (Exception ex)
                {
                    using (TextWriter tw = new StreamWriter(context.Server.MapPath("~/App_Data/Error_" + DateTime.Now.Ticks + ".txt")))
                    {
                        tw.WriteLine(ex.Message);
                        tw.WriteLine(ex.InnerException.Message);
                        if (messageHandler.ResponseDocument != null)
                        {
                            tw.WriteLine(messageHandler.ResponseDocument.ToString());
                        }
                        tw.Flush();
                        tw.Close();
                    }
                }
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
}