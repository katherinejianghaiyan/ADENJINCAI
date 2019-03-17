using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Luoyi.Common
{
    /// <summary>
    /// Web相关操作工具类
    /// </summary>
    public class WebHelper
    {
        #region 获得客户端的ip地址
        /// <summary>
        /// 获得客户端的ip地址
        /// </summary>
        /// <returns>客户端IP地址</returns>
        public static string GetClientIP()
        {
            System.Web.HttpContext current = System.Web.HttpContext.Current;
            if (current == null)
            {
                return "0.0.0.0";
            }

            string clientIP = string.Empty;
            try
            {
                if (current.Request.ServerVariables["HTTP_X_REAL_IP"] != null)
                {
                    clientIP = current.Request.ServerVariables["HTTP_X_REAL_IP"];
                    if (string.IsNullOrEmpty(clientIP))
                    {
                        clientIP = current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        clientIP = !string.IsNullOrEmpty(clientIP) ? clientIP.Split(',')[0] : clientIP;
                    }
                }//nginx代理
                else if (current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    clientIP = current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    clientIP = !string.IsNullOrEmpty(clientIP) ? clientIP.Split(',')[0] : clientIP;
                }//使用了代理
                else
                {
                    clientIP = current.Request.ServerVariables["REMOTE_ADDR"];
                }

                //Net下用戶IP
                if (string.IsNullOrEmpty(clientIP))
                {
                    clientIP = current.Request.UserHostAddress;
                }

                clientIP = string.IsNullOrEmpty(clientIP) ? "0.0.0.0" : clientIP.Trim();

            }
            catch
            {
                clientIP = "0.0.0.0";
            }

            return clientIP;
        }
        #endregion

        #region 获取服务器IP
        /// <summary>
        /// 获取服务器IP
        /// </summary>
        public static string GetServerIP()
        {
            return HttpContext.Current.Request.ServerVariables.Get("Local_Addr").ToString();
        }
        #endregion

        #region  获得域名对应的IP地址
        /// <summary>
        /// 获得域名对应的IP地址
        /// </summary>
        /// <param name="domain">域名或IP地址</param>
        /// <returns>IP地址</returns>
        public static string GetDomainIP(string domain)
        {
            try
            {
                IPAddress ip;
                if (IPAddress.TryParse(domain, out ip))
                {
                    return ip.ToString();
                }
                else
                {
                    return Dns.GetHostEntry(domain).AddressList[0].ToString();
                }
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region 获取请求页面物理路径
        /// <summary>
        /// 获取完整路径(获取请求页面物理路径)
        /// </summary>
        /// <param name="path">请求相对路径</param>
        /// <returns>物理路径</returns>
        public static string GetMapPath(string path)
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                return context.Server.MapPath(path);
            }
            else
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path.Replace("/", "\\"));
                return System.Web.Hosting.HostingEnvironment.MapPath(path);
            }
        }
        #endregion

        #region 获取请求来源页地址
        /// <summary>
        /// 返回上一个页面的地址(获取请求来源页地址)
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string result = null;
            try
            {
                result = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch { }

            if (result == null)
                return "";
            return result;
        }
        #endregion

        #region 获取当前Url中虚拟目录
        public static string GetUrlPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return HttpContext.Current.Request.ApplicationPath;
            
            path = path.TrimStart(@"/\".ToCharArray());
            
            return string.Format("{0}/{1}", HttpContext.Current.Request.ApplicationPath, path);
            //System.IO.Path.Combine(HttpContext.Current.Request.ApplicationPath, path);
        }

        #endregion

        #region 获取当前Url地址,从域名到虚拟地址
        public static string GetUrlApp()
        {            
            string path = HttpContext.Current.Request.ApplicationPath.TrimStart(@"/\".ToCharArray());
            return string.Format("{0}/{1}",HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), path);
        }
        #endregion

        #region 获取当前请求的原始
        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns>原始 URL</returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }
        #endregion

        #region 获得当前完整Url地址
        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }
        #endregion

        #region 判断当前请求是否为POST请求
        /// <summary>
        /// 判断当前请求是否为POST请求
        /// </summary>
        /// <returns></returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod == "POST";
        }
        #endregion

        #region 判断是否为GET请求
        /// <summary>
        /// 判断是否为GET请求
        /// </summary>
        /// <returns></returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod == "GET";
        }
        #endregion

        #region 判断是否为Ajax请求(支持JQuery Ajax操作)
        /// <summary>
        /// 判断是否为Ajax请求(支持JQuery Ajax操作)
        /// </summary>
        /// <returns></returns>
        public static bool IsAjaxRequest()
        {
            if (HttpContext.Current.Request.Headers.AllKeys.Contains("X-Requested-With") &&
                     HttpContext.Current.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 获得指定Url参数的值
        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            string val = HttpContext.Current.Request.QueryString[strName];
            if (val == null)
            {
                return string.Empty;
            }
            return val.Trim();
        }
        #endregion
        
        #region 获得指定Url参数的int类型值
        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName, int defValue = 0)
        {
            int result = 0;
            if (GetQueryString(strName).TryToInt32(out result, defValue))
            {
                return result;
            }
            return defValue;
        }
        #endregion

        #region 获取QueryString中的参数，并返回指定类型
        /// <summary>
        /// 获取QueryString中的参数，并返回指定类型
        /// </summary>
        /// <typeparam name="T">结果数据类型</typeparam>
        /// <param name="strName">参数名</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static T GetQuery<T>(string strName, T defValue)
        {
            string val = HttpContext.Current.Request.QueryString[strName];
            if (string.IsNullOrEmpty(val))
            {
                return defValue;
            }
            return val.Trim().ToType(defValue);
        }
        #endregion

        #region  获取指定的Form参数值
        /// <summary>
        /// 获取指定的Form参数值
        /// </summary>
        /// <param name="strName">form参数名</param>
        /// <returns>form参数值</returns>
        public static string GetFormString(string strName)
        {
            string val = HttpContext.Current.Request.Form[strName];
            if (val == null)
            {
                return string.Empty;
            }
            return val.Trim();
        }
        #endregion

        #region  获取制定的Form参数，并返回int值
        /// <summary>
        /// 获取制定的Form参数，并返回int值
        /// </summary>
        /// <param name="strName">form参数名</param>
        /// <param name="defValue">默认值</param>
        /// <returns>form参数值</returns>
        public static int GetFromInt(string strName, int defValue = 0)
        {
            string val = GetFormString(strName);
            if (string.IsNullOrEmpty(val))
            {
                return defValue;
            }
            return val.ToInt32(defValue);
        }
        #endregion

        #region 获取Form中的参数值，并返回指定类型
        /// <summary>
        /// 获取Form中的参数值，并返回指定类型
        /// </summary>
        /// <typeparam name="T">结果数据类型</typeparam>
        /// <param name="strName">参数名</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static T GetForm<T>(string strName, T defValue)
        {
            string val = HttpContext.Current.Request.Form[strName];
            if (string.IsNullOrEmpty(val))
            {
                return defValue;
            }
            return val.Trim().ToType(defValue);
        }
        #endregion

        #region Url Query String

        /// <summary>
        /// 根据参数集，获取参数名对应的值
        /// </summary>
        /// <param name="paraName">参数名</param>
        /// <param name="url">Url地址</param>
        /// <param name="splitChar">分隔符</param>
        /// <param name="encoding">Url参数解码时，所用到</param>
        /// <returns>参数名对应的值</returns>
        public static string GetUrlQueryString(string paraName, string url, char splitChar, Encoding encoding)
        {
            if (string.IsNullOrEmpty(paraName) || string.IsNullOrEmpty(url) || splitChar.ToString() == "")
            {
                return string.Empty;
            }

            string result = string.Empty;
            string[] strUrlArg = url.Split(new char[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);

            paraName += "=";
            int selectedIndex = -1;
            for (int i = 0; i < strUrlArg.Length; i++)
            {
                selectedIndex = strUrlArg[i].IndexOf(paraName, StringComparison.InvariantCultureIgnoreCase);
                if (selectedIndex == 0 || (selectedIndex > 0 && strUrlArg[i].StartsWith("?")))
                {
                    result = HttpUtility.UrlDecode(strUrlArg[i].Split('=')[1], encoding).Trim();
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 根据参数集，获取参数名对应的值
        /// </summary>
        /// <param name="paraName">参数名</param>
        /// <param name="url">Url地址</param>
        /// <returns>参数名对应的值</returns>
        public static string GetUrlQueryString(string paraName, string url)
        {
            return GetUrlQueryString(paraName, url, '&', Encoding.UTF8);
        }

        /// <summary>
        /// 根据参数集，获取参数名对应的值
        /// </summary>
        /// <param name="paraName">参数名</param>
        /// <param name="url">Url地址</param>
        /// <param name="encoding">Url解码时，所用到</param>
        /// <returns>参数名对应的值</returns>
        public static string GetUrlQueryString(string paraName, string url, Encoding encoding)
        {
            return GetUrlQueryString(paraName, url, '&', encoding);
        }

        /// <summary>
        /// 根据参数集，获取参数名对应的值
        /// </summary>
        /// <param name="paraName">参数名</param>
        /// <returns>参数名对应的值</returns>
        public static string GetUrlQueryString(string paraName)
        {
            return GetUrlQueryString(paraName, HttpContext.Current.Request.Url.Query, '&', Encoding.UTF8);
        }

        /// <summary>
        /// 根据参数集，获取参数名对应的值
        /// </summary>
        /// <param name="paraName">参数名</param>
        /// <param name="encoding">Url解码时，所用到</param>
        /// <returns>参数名对应的值</returns>
        public static string GetUrlQueryString(string paraName, Encoding encoding)
        {
            return GetUrlQueryString(paraName, HttpContext.Current.Request.Url.Query, '&', encoding);
        }

        #endregion

        #region 转换成URL参数
        /// <summary>
        /// 转换成URL参数
        /// </summary>
        /// <param name="dictionary">参数键值</param>
        /// <returns></returns>
        public static string ToQueryString(IDictionary<string, string> dictionary)
        {
            if (dictionary == null || dictionary.Count == 0) return string.Empty;

            var sb = new StringBuilder();
            foreach (var key in dictionary.Keys)
            {
                var value = dictionary[key];
                if (value != null)
                {
                    sb.Append(key + "=" + value + "&");
                }
            }

            return sb.ToString().TrimEnd('&');
        }
        #endregion

        #region 从HTML中获取文本,保留br,p,img
        /// <summary>
        /// 从HTML中获取文本,保留br,p,img
        /// </summary>
        /// <param name="html">HTML内容</param>
        /// <returns></returns>
        public static string GetTextFromHTML(string html)
        {
            Regex regEx = new Regex(@"</?(?!br|/?p|img)[^>]*>", RegexOptions.IgnoreCase);
            return regEx.Replace(html, "");
        }
        #endregion

        #region 以指定的ContentType输出指定文件
        /// <summary>
        /// 根据不同浏览器判断是否需要对文件名进行编码
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string GetEncodeFileName(string fileName)
        {
            var browser = System.Web.HttpContext.Current.Request.Browser;
            string type = browser.Type;
            if (type.IndexOf("InternetExplorer", StringComparison.OrdinalIgnoreCase) > -1
                || type.IndexOf("Mozilla", StringComparison.OrdinalIgnoreCase) > -1
                || type.IndexOf("IE", StringComparison.OrdinalIgnoreCase) > -1
                )
            {   //IE文件名需要编码，否则出现乱码(IE的UserAgent居然冒充Mozilla)，FireFox则不需要,
                return System.Web.HttpContext.Current.Server.UrlPathEncode(fileName);
            }
            return fileName;
        }


        /// <summary>
        /// 以指定的ContentType输出指定文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="filename">输出的文件名</param>
        /// <param name="filetype">将文件输出时设置的ContentType  "application/octet-stream"表示输出文件即出现下载对话框</param>
        public static void ResponseFile(string filepath, string filename, string filetype = "application/octet-stream")
        {
            Stream iStream = null;

            const int BUFFER_SIZE = 4096;
            // 缓冲区为4k
            byte[] buffer = new Byte[BUFFER_SIZE];

            // 文件长度
            int length;

            // 需要读的数据长度
            long dataToRead;

            try
            {
                // 打开文件
                iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);


                // 需要读的数据长度
                dataToRead = iStream.Length;
              
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + GetEncodeFileName(filename.Trim()));
                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");

                while (dataToRead > 0)
                {
                    // 检查客户端是否还处于连接状态
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, BUFFER_SIZE);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        // 如果不再连接则跳出死循环
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    // 关闭文件
                    iStream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 触发Web socket
        public static void PushWS(string siteGUID)
        {
            new System.Threading.Thread(() =>
            {
                try
                {
                    WebClient wc = new WebClient();
                    string url = ConfigurationManager.AppSettings["WSURL"].ToString();
                    if (string.IsNullOrEmpty(url)) throw new Exception("no url");
                    url = string.Format(@"{0}?wskey={1}", url, siteGUID);
                    Logger.Info(url);
                    Uri uri = new Uri(url);
                    wc.UploadString(uri, "");
                }
                catch (Exception ee)
                {
                    Logger.Error("Websocket:" + ee.Message);
                }
            }).Start();
        }
        #endregion
    }
}
