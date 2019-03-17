using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Common;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;

namespace Luoyi.Web
{
    public partial class BarCode : System.Web.UI.Page
    {

        public const string TICKET_ACCESSTOKEN_COOKIE_NAME = "_JINGCAIHT_ADEN_ACCESSTOKEN_TICKET";

        private dynamic _appConfig;
        protected dynamic AppConfig
        {
            get
            {
                if (_appConfig == null)
                {
                    _appConfig = new
                    {
                        AppId = ConfigurationManager.AppSettings["WeChatAppID"].ToString(), //换成你的信息
                        Secret = ConfigurationManager.AppSettings["WeChatAppSecret"].ToString()//换成你的信息
                    };
                }
                return _appConfig;
            }
        }

        protected string _appId
        {
            get { return AppConfig.AppId; }
        }

        protected string _appSecret
        {
            get { return AppConfig.Secret; }
        }

        protected string _openId = string.Empty;
        protected string _accessToken = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList1.DataSource = BLL.Site.GetList();
                DropDownList1.DataTextField = "Code";
                DropDownList1.DataValueField = "GUID";
                DropDownList1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            /* 2019-3-1
            if (!AccessTokenContainer.CheckRegistered(_appId))
            {
                AccessTokenContainer.Register(_appId, _appSecret);
            }

            _accessToken = CookieHelper.GetCookieValue(TICKET_ACCESSTOKEN_COOKIE_NAME);

            if (string.IsNullOrEmpty(_accessToken))
            {
                _accessToken = AccessTokenContainer.TryGetAccessToken(_appId, _appSecret);
                CookieHelper.SetCookieValue(TICKET_ACCESSTOKEN_COOKIE_NAME, _accessToken);
            }
            */
            _accessToken = AccessTokenContainer.TryGetAccessToken(_appId, _appSecret); //WxHelper.GetAccessToken();
            var createQrCodeResult = QrCodeApi.CreateByStr(_accessToken, DropDownList1.SelectedValue);

            Image1.ImageUrl = QrCodeApi.GetShowQrCodeUrl(createQrCodeResult.ticket);
        }
    }
}