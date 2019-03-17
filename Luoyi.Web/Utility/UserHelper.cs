using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Threading;
using Luoyi.Common;
using Luoyi.Entity;
using Senparc.Weixin;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Luoyi.Web
{
    public class UserHelper
    {
        /// <summary>
        /// 管理员登录票据cookie名称
        /// </summary>
        public const string TICKET_COOKIE_NAME = "_JINGCAIHT_ADEN_TICKET";
        public const string TICKET_COOKIE_ACCESSTOKEN = "_JINGCAIHT_ADEN_ACCESSTOKEN";
        /// <summary>
        /// 票据有效时间
        /// </summary>
        public const int TICKET_HOURS = 10;
        /// <summary>
        ///  票据加解密密钥
        /// </summary>
        public const string DES_KEY = "t$*!=M0@";

        private static string appId = ConfigurationManager.AppSettings["WeChatAppID"];
        private static string secret = ConfigurationManager.AppSettings["WeChatAppSecret"];
        private static string testUserID = ConfigurationManager.AppSettings["TestUserID"];

        public static UserInfo GetUserInfo()
        {
            testUserID = ConfigurationManager.AppSettings["TestUserID"];
            if (!testUserID.Equals("0"))
            {
                //RemoveTicket();
                return BLL.User.GetInfo(testUserID.ToInt32());
            }            

            HttpCookie ticketCookie = HttpContext.Current.Request.Cookies[TICKET_COOKIE_NAME];

            if (ticketCookie == null || string.IsNullOrEmpty(ticketCookie.Value))
                return SetNewUser();
                 
            string decryptTicket = Common.DES.Decrypt(ticketCookie.Value, DES_KEY);
            //Logger.Info("Cookie中的用户\n" + decryptTicket);
            var userInfo = JsonHelper.JSONDeserialize<UserInfo>(decryptTicket);
            string wechatIDTicket = userInfo.WechatID;
            try
            {
                userInfo = BLL.User.GetInfo(userInfo.UserID);
               // if (userInfo == null) return null;
            }
            catch
            {
                return null;
            }
            //Cookie中的用户信息不对
            if (userInfo == null || wechatIDTicket != userInfo.WechatID)
            {
                RemoveTicket();
                SetNewUser();
            }

            #region 显示微信照片 2017-8-24
            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, secret);
            AccessTokenResult result = AccessTokenContainer.GetAccessTokenResult(appId, false);
            //Logger.Info("Cookie中的用户\n " + userInfo.FirstName + " \n" + userInfo.WechatID);
            Senparc.Weixin.MP.AdvancedAPIs.User.UserInfoJson wechatInfo = UserApi.Info(accessToken.ToString(), userInfo.WechatID);
            userInfo.HeaderImgUrl = wechatInfo.headimgurl;
            userInfo.FirstName = wechatInfo.nickname;//.ToUnicode();
            userInfo.UnionID = wechatInfo.unionid;

            SysConfig.UserLanguage = wechatInfo.language.Trim().ToLower() == "zh_cn" ?
                SysConfig.LanguageType.ZH_CN : SysConfig.LanguageType.EN_US;

            (new Thread(() => { BLL.User.Update(userInfo); })).Start();
            #endregion

            return userInfo;
        }

        public static void UserPageLog(UserInfo user,string pageName)
        {
            BLL.User.UserPageLog(user, pageName);
        }
        private static UserInfo SetNewUser()
        {
            string AuthorizeUrl = OAuthApi.GetAuthorizeUrl(appId, WebHelper.GetUrlApp()
               // string.Concat("http://", HttpContext.Current.Request.Url.Host)
                , "jcaden", OAuthScope.snsapi_userinfo);

            string code = HttpContext.Current.Request.QueryString["code"];
            string state = HttpContext.Current.Request.QueryString["state"];

            UserInfo userInfo = new UserInfo();

            if (string.IsNullOrEmpty(code) || !state.Equals("jcaden"))
            {
                HttpContext.Current.Response.Redirect(AuthorizeUrl);
                HttpContext.Current.Response.End();
            }
            OAuthAccessTokenResult result = null;

            //通过，用code换取access_token
            try
            {
                result = OAuthApi.GetAccessToken(appId, secret, code);
            }
            catch (Exception ex)
            {
                Logger.Error("GetAccessToken", ex);
                HttpContext.Current.Response.Redirect(AuthorizeUrl);
                HttpContext.Current.Response.End();
            }
            if (result.errcode != ReturnCode.请求成功)
            {
                HttpContext.Current.Response.Redirect(AuthorizeUrl);
                HttpContext.Current.Response.End();
            }

            //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息
            try
            {
                OAuthUserInfo wechatInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);

                userInfo = BLL.User.GetInfo(wechatInfo.openid);

                if (userInfo == null)
                {
                    var info = new UserInfo()
                    {
                        WechatID = wechatInfo.openid,
                        UnionID = wechatInfo.unionid,
                        FirstName = wechatInfo.nickname,
                        LastName = string.Empty,
                        SiteGUID = string.Empty,
                        BirthDay = 0,
                        Gender = wechatInfo.sex == 1 ? "男" : wechatInfo.sex == 2 ? "女" : "未知",
                        Mobile = string.Empty,
                        City = wechatInfo.city,
                        Department = string.Empty,
                        Position = string.Empty,
                        CreateTime = DateTime.Now,
                        CreateDate = DateTime.Now.ToInt()
                    };

                    var userID = BLL.User.Add(info);

                    if (userID > 0)
                        userInfo = BLL.User.GetInfo(userID);
                }
                else
                {
                    userInfo.FirstName = wechatInfo.nickname;
                    userInfo.Gender = wechatInfo.sex == 1 ? "男" : wechatInfo.sex == 2 ? "女" : "未知";
                    userInfo.City = wechatInfo.city;
                    userInfo.UnionID = wechatInfo.unionid;

                    (new Thread(() => { BLL.User.Update(userInfo); })).Start();
                }

                userInfo.HeaderImgUrl = wechatInfo.headimgurl;
                ResponseTicketCookie(userInfo);

            }
            catch (ErrorJsonResultException ex)
            {
                Logger.Error("GetUserInfo", ex);

                HttpContext.Current.Response.Redirect(AuthorizeUrl);
                HttpContext.Current.Response.End();
            }


            //Logger.Info("PageBase (ticket is null)" + JsonHelper.JSONSerialize(userInfo));

            return userInfo;
        }

        //type = "SO" 仅显示已下单
        public static int GetBookingQty(UserInfo userInfo, DateTime date, string type="")
        {
            int qty = 0;
            if(type.ToLower().Trim() == "") //包含购物车中的数量
                qty = (new CartHelper(userInfo)).GetCartQty();

            List<SaleOrderItemInfo> listsoitem = BLL.SaleOrderItem.GetList(
                string.Format(" and o.ispaid=1 and o.paidtime is not null and o.userid={0} and convert(varchar(10),o.requireddate,112)='{1}'", 
                userInfo.UserID, date.ToString("yyyyMMdd")));

            if (listsoitem != null && listsoitem.Count > 0)
                qty += listsoitem.Sum(q => q.Qty);            

            return qty;
        }
        /// <summary>
        /// 输出登录票据cookie
        /// </summary>
        /// <param name="userID">用户ID</param>
        public static void ResponseTicketCookie(UserInfo info)
        {
            var encryptTicket = DES.Encrypt(JsonHelper.JSONSerialize(info), DES_KEY);

            // 写Cookie
            var ticketCookie = new HttpCookie(TICKET_COOKIE_NAME) { Value = encryptTicket, Path = "/" };
            HttpContext.Current.Response.AddHeader("P3P", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
            HttpContext.Current.Response.Cookies.Add(ticketCookie);
        }

        /// <summary>
        /// 清除该票据
        /// </summary> 
        public static void RemoveTicket()
        {
            var ticketCookie = new HttpCookie(TICKET_COOKIE_NAME) { Expires = DateTime.Now.AddDays(-1) };
            HttpContext.Current.Response.AppendCookie(ticketCookie);
        }
    }
}