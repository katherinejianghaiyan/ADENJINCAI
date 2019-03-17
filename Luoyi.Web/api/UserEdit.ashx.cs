using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Luoyi.Common;
using Luoyi.BLL;

namespace Luoyi.Web.api
{
    /// <summary>
    /// UserEdit 的摘要说明
    /// </summary>
    public class UserEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var userName = WebHelper.GetFormString("UserName");
            var mobile = WebHelper.GetFormString("Mobile");
            var department = WebHelper.GetFormString("Department");
            var section = WebHelper.GetFormString("Section");

            var info = UserHelper.GetUserInfo();
            info.UserName = userName;
            info.Mobile = mobile;
            info.Department = department;
            info.Section = section;

            AjaxJsonInfo ajax = new AjaxJsonInfo();
            if (User.Update(info) > 0)
            {
                ajax.Status = 1;
                ajax.Message = "成功";
                ajax.Data = info;

                UserHelper.ResponseTicketCookie(info);
            }
            else
            {
                ajax.Status = 0;
                ajax.Message = "失败";
            }

            context.Response.Write(ajax.JSONSerialize());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}