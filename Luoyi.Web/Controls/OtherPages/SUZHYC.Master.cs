using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Helpers;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.Web.Controls.OtherPages
{
    public partial class SUZHYC : System.Web.UI.MasterPage
    {
        public const string JSAPI_TICKET = "_JSAPI_TICKET";

        protected string jsapiticket = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ConfigurationManager.AppSettings["TestUserID"].ToString().Equals("0")) return;

                WxConfigInfo wxConfig = new WxConfigInfo();

                wxConfig.appId = ConfigurationManager.AppSettings["WeChatAppID"].ToString();
                wxConfig.timestamp = JSSDKHelper.GetTimestamp();
                wxConfig.nonceStr = JSSDKHelper.GetNoncestr();

                jsapiticket = CookieHelper.GetCookieValue(JSAPI_TICKET);
                if (string.IsNullOrEmpty(jsapiticket))
                {
                    jsapiticket = JsApiTicketContainer.TryGetJsApiTicket(wxConfig.appId, 
                        ConfigurationManager.AppSettings["WeChatAppSecret"].ToString(), true);
                    CookieHelper.SetCookieValue(JSAPI_TICKET, jsapiticket);
                }

                try
                {
                    wxConfig.signature = JSSDKHelper.GetSignature(jsapiticket, wxConfig.nonceStr, wxConfig.timestamp, Request.Url.AbsoluteUri.ToString());
                    hidWxConfig.Value = JsonHelper.JSONSerialize(wxConfig);
                }
                catch (Exception ex)
                {
                    Logger.Info("jsapiticket", ex);
                    jsapiticket = JsApiTicketContainer.TryGetJsApiTicket(wxConfig.appId, ConfigurationManager.AppSettings["WeChatAppSecret"].ToString(), true);
                    CookieHelper.SetCookieValue(JSAPI_TICKET, jsapiticket);
                    wxConfig.signature = JSSDKHelper.GetSignature(jsapiticket, wxConfig.nonceStr, wxConfig.timestamp, Request.Url.AbsoluteUri.ToString());
                    hidWxConfig.Value = JsonHelper.JSONSerialize(wxConfig);
                }

            }
        }

        //18-7-7 移到Header.ascx.cs
        //public string SetTitleByClass(string classGUID, out string itemImgPath)
        //{
        //    itemImgPath = "";
        //    return Header.SetTilteByClass(classGUID, out itemImgPath);
        //}

        public void HidDefault()
        {
            Header.HideDefault();
        }

        public void SetBack(bool isshow = false)
        {
            Header.SetBack(isshow);
        }

        public void ShowDefault(bool showClass, bool showSearch)
        {
            Header.ShowDefault(showClass, showSearch);
        }

        public void MasterHome()
        {
            Footer.MasterHome = "current";
        }
        public void MasterMessage()
        {
            Footer.MasterMessage = "current";
        }
        public void MasterCart()
        {
            Header.HideDefault();
            Footer.MasterCart = "current";
        }
        public void MasterConcept()
        {
            Footer.MasterConcept = "current";
        }
        public void MasterAccount()
        {
            Footer.MasterAccount = "current";
        }
    }
}