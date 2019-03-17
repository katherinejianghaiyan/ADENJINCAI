using System;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Luoyi.Common
{
    /// <summary>
    /// JS脚本工具类
    /// </summary>
    public class JavaScriptHelper
    {
        /// <summary>
        /// 显示消息提示对话框，不输出页面内容，并返回上一页
        /// </summary> 
        /// <param name="msg">提示信息</param>
        /// <returns></returns>
        public static void ShowAndBack(string msg)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(string.Format("<script type=\"text/javascript\">alert(\"{0}\");history.back();</script>", msg));
            HttpContext.Current.Response.End();
        }
                    

        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void Show(Page page, string msg)
        { 
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", string.Format("<script type=\"text/javascript\">alert(\"{0}\");</script>", msg));
        }
           
        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void Show(string msg, string url)
        {
            Show(msg, url, TargetEnum.top);
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary> 
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        /// <param name="target">跳转目标窗口</param>
        public static void Show(string msg, string url, TargetEnum target)
        {
            var sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">\r\n");
            sb.AppendFormat("alert('{0}');\r\n", msg);
            sb.AppendFormat("{0}.location.href='{1}';\r\n", target.ToString(), url);
            sb.Append("</script>\r\n");
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End();  
        }
        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public static void ResponseScript(Page page, string script)
        { 
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", string.Format("\r\n<script type=\"text/javascript\">\r\n{0}\r\n</script>\r\n", script));           
        }

        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="script">输出脚本</param>
        public static void ResponseScript(string script)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(string.Format("\r\n<script type=\"text/javascript\">\r\n{0}\r\n</script>\r\n", script));
            HttpContext.Current.Response.End();   
        }

        /// <summary>
        ///  网址打开方式
        /// </summary>
        public enum TargetEnum
        {
            /// <summary>
            /// 
            /// </summary>
            top,
            /// <summary>
            /// 
            /// </summary>
            self,
            /// <summary>
            /// 
            /// </summary>
            parent
        }

        /// <summary>
        /// 显示消息提示对话框，不输出页面内容，并调用Thickbox方法关闭弹出窗口
        /// </summary>
        /// <param name="msg">提示信息</param>
        public static void ShowAndClose(string msg)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(string.Format("<script type=\"text/javascript\">alert(\"{0}\");", msg));
            HttpContext.Current.Response.Write("try{frameElement.api.opener.$.dialog.list[\"dialogWindow\"].close();}catch(e){ }</script>");
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 显示消息提示对话框，不输出页面内容，并调用Thickbox方法关闭弹出窗口，再刷新父级页面
        /// </summary>
        /// <param name="msg">提示信息</param>
        public static void ShowCloseRefresh(string msg)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(String.Format("<script type=\"text/javascript\">alert(\"{0}\");", msg));
            HttpContext.Current.Response.Write("window.parent.Refresh();</script>");
            HttpContext.Current.Response.End();
        }
    }
}
