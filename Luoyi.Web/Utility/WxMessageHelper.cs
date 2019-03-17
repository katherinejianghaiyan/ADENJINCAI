using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Luoyi.Common;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Luoyi.Web
{
    public class WxMessageHelper
    {

        private static dynamic _appConfig;
        protected static dynamic AppConfig
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
                    if (!AccessTokenContainer.CheckRegistered(_appConfig.AppId))
                    {
                        AccessTokenContainer.Register(_appConfig.AppId, _appConfig.Secret);
                    }
                }
                return _appConfig;
            }
        }

        

        protected static string _appId
        {
            get { return AppConfig.AppId; }
        }

        protected static string _appSecret
        {
            get { return AppConfig.Secret; }
        }
        public static bool SendText(string openId, string message)
        {
            try
            {

                 var result = CustomApi.SendText(_appId, openId, message);
                 if (result.errcode == Senparc.Weixin.ReturnCode.请求成功) return true;
                 else
                 {
                     Logger.Debug(result.errmsg);
                     return false;
                 }
            }
            catch(Exception ex)
            {
                Logger.Debug(ex.Message);
                return false;
            }
        }

        public static void SendTextToPaiedUsers(string message)
        {
            try
            {
                var userList = BLL.SaleOrder.GetPaiedUserList();
                if (userList != null && userList.Count > 0)
                {
                    foreach (var user in userList)
                    {
                        var result = CustomApi.SendText(_appId, user.WeChatID, message);
                        if (result.errcode != Senparc.Weixin.ReturnCode.请求成功) Logger.Debug(result.errmsg);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);
            }
        }

       
        public static bool SendWechat(string openId, string title, string text, string imgPath)
        {
            try
            {
                var articles = new List<Article>();
                articles.Add(new Article()
                {
                    Title = title,
                    Description =  text,
                    PicUrl = string.Format("http://{0}",  imgPath)
                    // string.Format("http://{0}/{1}", HttpContext.Current.Request.Url.Host, imgPath)
                });

                var result = CustomApi.SendNews(_appId, openId, articles);
                if (result.errcode == Senparc.Weixin.ReturnCode.请求成功)
                {
                    return true;
                }
                else
                {
                    Logger.Debug(result.errmsg);
                    return false;
                }

            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);
                return false;
            }
        }

        public static bool SendNews(string openId, string orderCode, string barcodeOfSO, string language="", bool showPrice = true)
        {
            try
            {                
                var info = BLL.SaleOrder.GetInfo(orderCode);
                DataTable dtSaleOrderItem = BLL.SaleOrderItem.GetTable(string.Format(" AND SOGUID = '{0}'", info.GUID),language);

                if (info != null)
                {
                    var articles = new List<Article>();
                    articles.Add(new Article()
                    {
                        Title = string.Format("{0}: {1}", HtmlLang.Lang("OrderCode", "单号", @"/Account/ToPickUp.aspx"), info.OrderCode),
                        Description = string.Format("{0}: {1} {2} {3}\n{4}: {1}\n     {5}{6}",
                        HtmlLang.Lang("PickupDate", "取货时间", @"/Account/ToPickUp.aspx"),
                        info.RequiredDate.ToString("yyyy-MM-dd"),
                        info.RequiredDate.TimeOfDay == TimeSpan.Zero ? "" : info.RequiredDate.ToString("H:mm"),
                        !string.IsNullOrWhiteSpace(info.RequiredDinnerType) ? info.RequiredDinnerType : "",
                        HtmlLang.Lang("GuaranteeDate", "保质期", @"/Account/ToPickUp.aspx"),
                        
                        //UserHelper.GetUserInfo().PaymentMethod.Equals("POD")? " 11:30 ~ 12:30" : "",
                        string.Join("\n     ", dtSaleOrderItem.AsEnumerable().Select(i => string.Format("{0}  ×  {1}", i.Field<string>("ItemName"), i.Field<int>("Qty")))),
                        showPrice ? string.Format("\nTotal: {0}", info.PaymentAmount.ToString("G0")) : "")
                        + DateTime.Now.ToString("\nH:mm:ss"),
                        PicUrl = string.Format("{0}/api/WxBarCode.ashx?{3}&ShowType=SendWXMsg&OrderCode={1}&BarcodeOfSO={2}",
                            WebHelper.GetUrlApp(),
                            //HttpContext.Current.Request.Url.Host, 
                            info.OrderCode, barcodeOfSO,Guid.NewGuid())
                    });

                    var result = CustomApi.SendNews(_appId, openId, articles);
                    if (result.errcode == Senparc.Weixin.ReturnCode.请求成功)
                    {
                        return true;
                    }
                    else
                    {
                        Logger.Debug(result.errmsg);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);
                return false;
            }

        }
        public static string SendText(string[] openIds, string text)
        {
            try
            {
                if (text == null || text.Trim() == "" || openIds == null || openIds.Length == 0)
                    throw new Exception("No message");

                text = string.Format("{0}\n{1}", text.Trim(), DateTime.Now.ToString("yyyy-M-d H:m"));

                StringBuilder sb = new StringBuilder();
                var oIds = openIds.Distinct();
                //return sb.ToString();
                foreach (string openId in oIds)
                {
                    try
                    {
                        var result = CustomApi.SendText(_appId, openId, text);
                        if (result.errcode == Senparc.Weixin.ReturnCode.请求成功) continue;

                        throw new Exception(result.errmsg);
                    }
                    catch (Exception e)
                    {
                        sb.AppendFormat("{0} : {1}\n", openId, e.Message);
                        Logger.Debug(e.Message);
                    }
                }

                if (sb.Length == 0) return "ok";
                else return sb.ToString();
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);
                return ex.Message;
            }
        }
    }

    
}