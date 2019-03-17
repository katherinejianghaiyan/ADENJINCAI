using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Luoyi.Common;

namespace Luoyi.Web
{
    public class HtmlLang
    {
        public static string Lang(string controlID, string text="", string pageName = "")
        {
            //得到页面，唯一ID及文本，然后根据SysConfig.UserLanguage来判断显示中文还是英文

            try
            {
                if (string.IsNullOrEmpty(pageName))
                {
                    pageName = controlID.Contains("Master") ? "Master" : HttpContext.Current.Request.Url.PathAndQuery;//.AbsolutePath;
                }
                pageName = pageName.Replace(WebHelper.GetUrlPath(""), "");
                string stext = Lang(controlID, pageName);
                if (stext != "") return stext;

                pageName = pageName.Split('?')[0];
                stext = Lang(controlID, pageName);
                if (stext == "") stext = text;

                return stext;
            }
            catch(Exception e)
            {             
            }
            return text;
        }

        private static string Lang(string controlID, string pageName)
        {
            var info = BLL.Lang.GetInfo(pageName, controlID);
            string text = "";
            if (info != null)
            {
                switch (SysConfig.UserLanguage)
                {
                    case SysConfig.LanguageType.ZH_CN://中文
                        text = info.ZHCNText;
                        break;
                    case SysConfig.LanguageType.EN_US://英文
                        text = info.ENUSText;
                        break;
                }
            }

            return text;
        }

    }
}