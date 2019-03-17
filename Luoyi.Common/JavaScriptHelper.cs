using System;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Luoyi.Common
{
    /// <summary>
    /// JS�ű�������
    /// </summary>
    public class JavaScriptHelper
    {
        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի��򣬲����ҳ�����ݣ���������һҳ
        /// </summary> 
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <returns></returns>
        public static void ShowAndBack(string msg)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(string.Format("<script type=\"text/javascript\">alert(\"{0}\");history.back();</script>", msg));
            HttpContext.Current.Response.End();
        }
                    

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի���
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void Show(Page page, string msg)
        { 
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", string.Format("<script type=\"text/javascript\">alert(\"{0}\");</script>", msg));
        }
           
        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի��򣬲�����ҳ����ת
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="url">��ת��Ŀ��URL</param>
        public static void Show(string msg, string url)
        {
            Show(msg, url, TargetEnum.top);
        }

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի��򣬲�����ҳ����ת
        /// </summary> 
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="url">��ת��Ŀ��URL</param>
        /// <param name="target">��תĿ�괰��</param>
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
        /// ����Զ���ű���Ϣ
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="script">����ű�</param>
        public static void ResponseScript(Page page, string script)
        { 
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", string.Format("\r\n<script type=\"text/javascript\">\r\n{0}\r\n</script>\r\n", script));           
        }

        /// <summary>
        /// ����Զ���ű���Ϣ
        /// </summary>
        /// <param name="script">����ű�</param>
        public static void ResponseScript(string script)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(string.Format("\r\n<script type=\"text/javascript\">\r\n{0}\r\n</script>\r\n", script));
            HttpContext.Current.Response.End();   
        }

        /// <summary>
        ///  ��ַ�򿪷�ʽ
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
        /// ��ʾ��Ϣ��ʾ�Ի��򣬲����ҳ�����ݣ�������Thickbox�����رյ�������
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void ShowAndClose(string msg)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(string.Format("<script type=\"text/javascript\">alert(\"{0}\");", msg));
            HttpContext.Current.Response.Write("try{frameElement.api.opener.$.dialog.list[\"dialogWindow\"].close();}catch(e){ }</script>");
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի��򣬲����ҳ�����ݣ�������Thickbox�����رյ������ڣ���ˢ�¸���ҳ��
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void ShowCloseRefresh(string msg)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(String.Format("<script type=\"text/javascript\">alert(\"{0}\");", msg));
            HttpContext.Current.Response.Write("window.parent.Refresh();</script>");
            HttpContext.Current.Response.End();
        }
    }
}
