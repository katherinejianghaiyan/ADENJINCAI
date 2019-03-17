using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Luoyi.Common;
using Luoyi.Entity;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace Luoyi.Web
{
    /// <summary>
    /// 后台页面基类
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        /// <summary>
        /// 管理员登录票据cookie名称
        /// </summary>
        public const string TICKET_COOKIE_NAME = "_JINGCAIHT_ADEN_TICKET";
        /// <summary>
        /// 票据有效时间
        /// </summary>
        public const int TICKET_HOURS = 10;
        /// <summary>
        ///  票据加解密密钥
        /// </summary>
        public const string DES_KEY = "t$*!=M0@";
        //private static BUInfo buInfo = null;
        private static SiteInfo site = null;
        public SiteInfo siteInfo
        {
            set
            {
                site = value;
            }
            get
            {             
                return site;
            }
        }


        private static UserInfo userInfo = null;
        public UserInfo _UserInfo
        {
            set
            {
                userInfo = value;
            }
            get
            {                
                return userInfo;
            }
        }
        public DateTime EndHour = DateTime.MinValue;
        public int DeliveryDays = -1;
        public string PickupTime = "";

       
        public string DeliveryMsg = Luoyi.Web.HtmlLang.Lang("DeliveryMsg", "上午11点前下单当天取货", "/ItemDetail.aspx");
        //public string PickupMsg = Luoyi.Web.HtmlLang.Lang("PickUpDay", "1111默认取货日期为成功付款后的当日（上午11点前下单）或第二天（上午11点后下单)", "/PickUp.aspx");


        /// <summary>
        /// 当前登录管理员信息
        /// </summary>
        //public UserInfo userInfo
        //{
        //    get;
        //    set;
        //}

        protected override void OnLoad(EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            try
            {
                //if (_UserInfo == null)
                if (this.Page is index)//首页
                    _UserInfo = UserHelper.GetUserInfo();

                if (_UserInfo == null)
                {
                    throw new Exception("账号不存在");
                }

                if (string.IsNullOrEmpty(_UserInfo.SiteGUID))
                {
                    //JavaScriptHelper.ResponseScript(this.Page, "$(function(){wx.ready(function () {$(\".weui_dialog_alert .weui_dialog_bd\").html(\"您当前未绑定相关运营点，无法进入商城\");$(\".weui_dialog_alert\").show().on('click', '.weui_btn_dialog', function () {$('.weui_dialog_alert').off('click').hide();wx.closeWindow();});});});");
                    throw new Exception("您当前未绑定相关运营点，无法进入商城，请重新扫二维码");
                }

                //if (siteInfo == null)
                if (this.Page is index)//首页
                {
                    siteInfo = BLL.Site.GetInfo(_UserInfo.SiteGUID, SysConfig.UserLanguage.ToString());
                    if(siteInfo == null)
                        throw new Exception("您当前未绑定相关运营点，无法进入商城");

                    base.OnLoad(e);
                    return;
                }

                if (!Page.IsPostBack)
                {
                    // 读取用户在当前页面拥有权限并保存状态
                }


                #region 截止时间，今天或明天发货
                UserHelper.UserPageLog(_UserInfo, HttpContext.Current.Request.Url.AbsolutePath);
                var buInfo = BLL.BU.GetInfo(_UserInfo.BUGUID);

                if (buInfo == null)
                    buInfo = BLL.BU.GetInfo(_UserInfo.BUGUID);
                DeliveryDays = buInfo.DeliveryDays;
                //Logger.Info(string.Format("buInfo.EndHour:{0}", buInfo.EndHour));

                EndHour = Convert.ToDateTime(buInfo.EndHour);
                PickupTime = buInfo.PickupTime;

                //如果Site上定义截止时间和发货天数，以Site的定义为主 steve.weng 2017-6-2
                //if (siteInfo != null)
                {
                    if (siteInfo.EndHour != null && siteInfo.EndHour != "") EndHour = Convert.ToDateTime(siteInfo.EndHour);
                    if (siteInfo.DeliveryDays != -1) DeliveryDays = siteInfo.DeliveryDays;
                    if (siteInfo.PickupTime != "") PickupTime = siteInfo.PickupTime;
                    try
                    {
                        Luoyi.Web.Controls.Header header = (Master.FindControl("header") as Luoyi.Web.Controls.Header);
                        if (header != null)
                            header.SetClass();
                    }
                    catch { }
                }

                EndHour = EndHour == DateTime.MinValue ? DateTime.Now.Date.AddHours(11) : EndHour;
                DeliveryDays = DeliveryDays == -1 ? 0 : DeliveryDays;
                #endregion

                DeliveryMsg = string.Format(DeliveryMsg, EndHour.Hour,
                    string.Format(DeliveryDays == 0 ? SysConfig.UserLanguage == SysConfig.LanguageType.ZH_CN ? "今天{0}" : "the same day" :
                     SysConfig.UserLanguage == SysConfig.LanguageType.ZH_CN ? "明天{0}" : "the next day", PickupTime));

                string scn = "当日{1}（{0} 前下单）或第二天{1}（{0} 后下单)";
                string sen = "the same day if place the order before {0}, otherwise pick up the next day";
                if (DeliveryDays == 1)
                {
                    scn = "第二天{1}（{0}点前下单）";
                    sen = "the next day if place the order before {0}";
                }
                scn = string.Format(scn, EndHour.ToString("H:00"), PickupTime);
                sen = string.Format(sen, EndHour.ToString("H:00"));

                //PickupMsg = string.Format(PickupMsg, SysConfig.UserLanguage == SysConfig.LanguageType.ZH_CN ? scn : sen);
            }
            catch (Exception ee)
            {
                JavaScriptHelper.Show(this, ee.Message);
                return;
            }
            //调用Page_Load事件
            base.OnLoad(e);
        }

        #region 添加操作日志

        #endregion
    }
}
