using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luoyi.Web
{
    public class SysConfig
    {
        public enum LanguageType
        {
            ZH_CN = 0,
            EN_US = 1
        }

        /// <summary>
        /// 中英文 标识
        ///  中文：ZH_CN
        ///  英文：EN_US
        /// </summary>
        public static LanguageType UserLanguage
        {
            get
            {
                    if (HttpContext.Current.Request.Cookies["Language"] == null)
                    {
                        UserLanguage = LanguageType.ZH_CN;
                    }

                    return (LanguageType)Enum.Parse(typeof(LanguageType), HttpContext.Current.Request.Cookies["Language"].Value);
            }
            set
            {

                HttpCookie UserCookie = new HttpCookie("Language", value.ToString());
                HttpContext.Current.Response.Cookies.Add(UserCookie);
            }
        }


    }
}