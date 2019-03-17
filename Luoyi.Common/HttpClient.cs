using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Luoyi.Common
{
    /// <summary>
    /// Http模拟请求
    /// </summary>
    public class HttpClient
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get;
            private set;
        }

        /// <summary>
        ///  获得请求网址返回文本内容,默认UTF-8编码
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="requestParameters">请求参数</param>
        /// <param name="headers">请求头部信息</param>
        /// <param name="method">GET/POST</param>
        ///   <returns>发生错误返回空字符</returns>
        public string GetResponseString(string url, IDictionary<string, string> requestParameters,
            NameValueCollection headers, RequestMethod method)
        {
            return GetResponseString(url, requestParameters, headers, method, Encoding.UTF8);
        }

        /// <summary>
        ///  获得请求网址返回文本内容
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="requestParameters">请求参数</param>
        /// <param name="headers">请求头部信息</param>
        /// <param name="method">GET/POST</param>
        /// <param name="encoding">编码</param>
        /// <returns>发生错误返回空字符</returns>
        public string GetResponseString(string url, IDictionary<string, string> requestParameters, NameValueCollection headers, RequestMethod method, Encoding encoding)
        {
            UriBuilder uri = new UriBuilder(url);
            if (method == RequestMethod.GET)
            {
                BuildQueryString(ref uri, requestParameters);
            }

            HttpWebRequest request = BuildRequest(method, uri);
            if (headers != null)
            {
                request.Headers.Add(headers);
            }

            if (method == RequestMethod.POST)
            {
                BuildPostString(ref request, requestParameters);
            }


            string result = string.Empty;

            if (url.StartsWith("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    Stream responseStream = response.GetResponseStream();
                    if (responseStream == null)
                    {
                        return result;
                    }
                    using (StreamReader reader = new StreamReader(responseStream, encoding))
                    {
                        result = reader.ReadToEnd();
                    }
                }

                return result;
            }
            catch (WebException ex)
            {
                ErrorMessage = ex.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 创建请求实例
        /// </summary>
        /// <param name="method">请求方式</param>
        /// <param name="uri">请求网址</param>
        /// <returns></returns>
        private HttpWebRequest BuildRequest(RequestMethod method, UriBuilder uri)
        {
            var request = WebRequest.Create(uri.Uri) as HttpWebRequest;
            request.ServicePoint.Expect100Continue = false;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0)";
            request.ContentType =  "application/x-www-form-urlencoded";

            request.Method = method.ToString();
            request.ServicePoint.Expect100Continue = false;
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;
            return request;
        }

        /// <summary>
        /// 生成提交的字符串
        /// </summary>
        /// <param name="request">HTTP请求实例</param>
        /// <param name="requestParameters">请求参数值</param>
        private void BuildPostString(ref HttpWebRequest request, IDictionary<string, string> requestParameters)
        {
            using (StreamWriter requestStream = new StreamWriter(request.GetRequestStream()))
            {
                requestStream.Write(WebHelper.ToQueryString(requestParameters));
                requestStream.Close();
            }
        }

        /// <summary>
        ///  生成HTTP GET的查询字符
        /// </summary>
        /// <param name="uri">请求网址</param>
        /// <param name="requestParameters">查询参数值</param>
        /// <returns></returns>
        private void BuildQueryString(ref UriBuilder uri, IDictionary<string, string> requestParameters)
        {
            if (requestParameters == null || requestParameters.Count == 0) return;

            if (string.IsNullOrEmpty(uri.Query) == false)
            {
                uri.Query += "&";
            }

            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in requestParameters)
            {
                builder.AppendFormat("{0}={1}&", kvp.Key, kvp.Value);
            }

            uri.Query += builder.ToString().TrimEnd('&');
        }


        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }


        /// <summary>
        /// 请求方式
        /// </summary>
        public enum RequestMethod
        {
            /// <summary>
            /// GET请求
            /// </summary>
            GET,
            /// <summary>
            /// POST请求
            /// </summary>
            POST
        }
    }
}
