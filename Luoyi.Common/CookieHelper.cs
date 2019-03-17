using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Luoyi.Common
{
    /// <summary>
    /// COOKIE辅助类
    /// 作者：骆克春
    /// </summary>
    public class CookieHelper
    {
        #region 独立COOKIE操作
        /// <summary>
        /// 获取COOKIE值
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookieName)
        {
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookieName];
            if (httpCookie == null) return null;
            return httpCookie.Value;
        }
        /// <summary>
        /// 设置Cookie值，不存在则创建
        /// </summary>
        /// <param name="cookieName">COOKIE名称</param>
        /// <param name="value">COOKIE值</param> 
        public static void SetCookieValue(string cookieName, string value)
        {
            SetCookieValue(cookieName, value, string.Empty, DateTime.MinValue, string.Empty, false);
        }
        /// <summary>
        /// 设置Cookie值，不存在则创建
        /// </summary>
        /// <param name="cookieName">COOKIE名称</param>
        /// <param name="value">COOKIE值</param>
        /// <param name="expireSeconds">过期时间 单位：秒</param>
        public static void SetCookieValue(string cookieName, string value, double expireSeconds)
        {
            SetCookieValue(cookieName, value, string.Empty, DateTime.Now.AddSeconds(expireSeconds), string.Empty, false);
        }
        /// <summary>
        /// 设置Cookie值，不存在则创建
        /// </summary>
        /// <param name="cookieName">COOKIE名称</param>
        /// <param name="value">COOKIE值</param> 
        /// <param name="expireSeconds">过期时间 单位：秒</param>
        /// <param name="path">COOKIE路径</param> 
        public static void SetCookieValue(string cookieName, string value, double expireSeconds, string path)
        {
            SetCookieValue(cookieName, value, string.Empty, DateTime.Now.AddSeconds(expireSeconds), path, false);
        }
        /// <summary>
        /// 设置Cookie值，不存在则创建
        /// </summary>
        /// <param name="cookieName">COOKIE名称</param>
        /// <param name="value">COOKIE值</param> 
        /// <param name="expireTime">过期时间(绝对)</param> 
        public static void SetCookieValue(string cookieName, string value, DateTime expireTime)
        {
            SetCookieValue(cookieName, value, string.Empty, expireTime, string.Empty, false);
        }
        /// <summary>
        /// 设置Cookie值，不存在则创建
        /// </summary>
        /// <param name="cookieName">COOKIE名称</param>
        /// <param name="value">COOKIE值</param> 
        /// <param name="expireTime">过期时间(绝对)</param>
        /// <param name="path">COOKIE路径</param> 
        public static void SetCookieValue(string cookieName, string value, DateTime expireTime, string path)
        {
            SetCookieValue(cookieName, value, string.Empty, expireTime, path, false);
        }
        /// <summary>
        /// 设置Cookie值，不存在则创建
        /// </summary>
        /// <param name="cookieName">COOKIE名称</param>
        /// <param name="value">COOKIE值</param>
        /// <param name="domain">所属域</param>
        /// <param name="expireSeconds">过期时间 单位：秒</param>
        public static void SetCookieValue(string cookieName, string value, string domain, double expireSeconds)
        {
            SetCookieValue(cookieName, value, domain, DateTime.Now.AddSeconds(expireSeconds), string.Empty, false);
        }
        /// <summary>
        /// 设置Cookie值，不存在则创建
        /// </summary>
        /// <param name="cookieName">COOKIE名称</param>
        /// <param name="value">COOKIE值</param>
        /// <param name="domain">所属域</param>
        /// <param name="expireSeconds">过期时间 单位：秒</param>
        /// <param name="path">COOKIE路径</param> 
        public static void SetCookieValue(string cookieName, string value, string domain, double expireSeconds, string path)
        {
            SetCookieValue(cookieName, value, domain, DateTime.Now.AddSeconds(expireSeconds), path, false);
        }
        /// <summary>
        /// 设置Cookie值，不存在则创建
        /// </summary>
        /// <param name="cookieName">COOKIE名称</param>
        /// <param name="value">COOKIE值</param>
        /// <param name="domain">所属域</param>
        /// <param name="expireTime">过期时间(绝对)</param> 
        public static void SetCookieValue(string cookieName, string value, string domain, DateTime expireTime)
        {
            SetCookieValue(cookieName, value, domain, expireTime, string.Empty, false);
        }
        /// <summary>
        /// 设置Cookie值，不存在则创建
        /// </summary>
        /// <param name="cookieName">COOKIE名称</param>
        /// <param name="value">COOKIE值</param>
        /// <param name="domain">所属域</param>
        /// <param name="expireTime">过期时间(绝对)</param>
        /// <param name="path">COOKIE路径</param> 
        public static void SetCookieValue(string cookieName, string value, string domain, DateTime expireTime, string path)
        {
            SetCookieValue(cookieName, value, domain, expireTime, path, false);
        }
        /// <summary>
        /// 设置Cookie值，不存在则创建
        /// </summary>
        /// <param name="cookieName">COOKIE名称</param>
        /// <param name="value">COOKIE值</param>
        /// <param name="domain">所属域</param>
        /// <param name="expireTime">过期时间(绝对)</param>
        /// <param name="path">COOKIE路径</param>
        /// <param name="isHttpOnly">是否仅http请求访问</param>
        public static void SetCookieValue(string cookieName, string value, string domain, DateTime expireTime, string path, bool isHttpOnly)
        {
            if (string.IsNullOrEmpty(cookieName)) return;
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookieName];
            if (httpCookie == null) httpCookie = new HttpCookie(cookieName);
            httpCookie.Value = value;
            if (!string.IsNullOrEmpty(domain))
            {
                httpCookie.Domain = domain;
            }
            if (expireTime != DateTime.MinValue)
            {
                httpCookie.Expires = expireTime;
            }
            if (!string.IsNullOrEmpty(path))
            {
                httpCookie.Path = path;
            }
            else
            {
                httpCookie.Path = "/";
            }
            httpCookie.HttpOnly = isHttpOnly;
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }

        #endregion

        #region COOKIE KEY-VALUE操作

        /// <summary>
        /// 获取COOKIE中某个键的值
        /// </summary>
        /// <param name="cookieName">cookie名</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetCookieValueByKey(string cookieName, string key)
        {
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookieName];
            if (httpCookie == null) httpCookie = new HttpCookie(cookieName);
            if (httpCookie[key] == null)
                return null;
            else
                return httpCookie[key];
        }
        /// <summary>
        /// 设置COOKIE某个键值，不存在则创建
        /// </summary>
        /// <param name="cookieName">Cookie名</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetCookieKeyValue(string cookieName, string key, string value)
        {
            SetCookieKeyValue(cookieName, key, value, DateTime.MinValue);
        }
        /// <summary>
        /// 设置COOKIE某个键值，不存在则创建
        /// </summary>
        /// <param name="cookieName">Cookie名</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expireSeconds">相对过期时间 单位：秒</param>
        public static void SetCookieKeyValue(string cookieName, string key, string value, double expireSeconds)
        {
            SetCookieKeyValue(cookieName, key, value, DateTime.Now.AddSeconds(expireSeconds));
        }
        /// <summary>
        /// 设置COOKIE某个键值，不存在则创建
        /// </summary>
        /// <param name="cookieName">Cookie名</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expireTime">绝对过期时间</param>
        public static void SetCookieKeyValue(string cookieName, string key, string value, DateTime expireTime)
        {
            if (string.IsNullOrEmpty(key)) return;
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookieName];
            if (httpCookie == null)
            {
                httpCookie = new HttpCookie(cookieName);
            }
            if (httpCookie[key] == null)
            {
                httpCookie.Values[key] = value;
            }
            else
            {
                httpCookie[key] = value;
            }
            if (expireTime != DateTime.MinValue)
            {
                httpCookie.Expires = expireTime;
            }
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }
        #endregion

        #region 删除COOKIE
        /// <summary>
        /// 删除COOKIE
        /// </summary>
        /// <param name="cookieName">Cookie名</param>
        public static void DeleteCookie(string cookieName)
        {
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookieName] as HttpCookie;
            if (httpCookie == null)
            {
                httpCookie = new HttpCookie(cookieName);
            }
            httpCookie.Value = string.Empty;
            httpCookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }
        #endregion
    }
}
