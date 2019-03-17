using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Luoyi.Common;
using Luoyi.BLL;
using Luoyi.Entity;
using System.Text;
using System.Web.Script.Serialization;


namespace Luoyi.Web.api
{
    /// <summary>
    /// Survey 的摘要说明
    /// </summary>
    public class Survey : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            AjaxJsonInfo ajax = new AjaxJsonInfo() {Status = 1,Message = "成功"};           

            try
            {
                AutoResetEvent uploaded = new AutoResetEvent(false);
                List<object> sdetail = new List<object>();
                //string sdetail = WebHelper.GetFormString("details");

                string action = WebHelper.GetFormString("action");

                if (string.IsNullOrWhiteSpace(action))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                     sdetail = serializer.Deserialize<List<object>>(WebHelper.GetFormString("details"));
                }
                
                else if (action == "UploadImage")
                {
                    if (context.Request.Files.Count == 0) throw new Exception();

                    string fileName = context.Request.Files[0].FileName;
                    string costCenterCode = WebHelper.GetFormString("costCenterCode");
                    fileName = string.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(fileName),
                        Guid.NewGuid(), Path.GetExtension(fileName));

                    if (!string.IsNullOrWhiteSpace(costCenterCode))
                        fileName = Path.Combine(costCenterCode, fileName);

                    Task.Run(()=>
                    {
                        UploadFile(fileName, context.Request.Files[0]);
                        uploaded.Set();
                    });

                    
                    string data = string.Format("'id':'{0}','answer':'{1}'", WebHelper.GetFormString("details"), fileName);
                    data= "{" + data + "}";
                    sdetail.Add(data);
                }

                string headGuid = WebHelper.GetFormString("headGuid");
                if (string.IsNullOrWhiteSpace(headGuid))
                    headGuid = Guid.NewGuid().ToString();
                ajax.Data = headGuid;

                var siteGuid = WebHelper.GetFormString("siteGuid");

                var userdepartment = WebHelper.GetFormString("userdepartment");

                var username = WebHelper.GetFormString("username");

                List<object> details = sdetail;
                var userInfo = UserHelper.GetUserInfo();

                SurveyDetails info = new SurveyDetails();
                info.headGuid = headGuid;
                info.siteGuid = siteGuid;
                info.createUser = userInfo.UserID.ToString();
                info.userName = username;
                info.userDept = userdepartment;
                info.details = details;
                info.action = action;


                if (BLL.Survey.Update(info) <= 0)
                    throw new Exception();

                if (action == "UploadImage") 
                    uploaded.WaitOne();
            }
            catch(Exception e)
            {
                ajax.Status = 0;
                ajax.Message = "失败";              
            }
            finally
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(ajax.JSONSerialize());
                context.Response.End();
            }
        }

        private void UploadFile(string fileName, HttpPostedFile file)
        {
            try
            {
                string filePath = ConfigurationManager.AppSettings["SurveyPhotosPath"];
                string spath = Path.GetDirectoryName(fileName);
                if (!string.IsNullOrWhiteSpace(spath))
                    filePath = Path.Combine(filePath,spath);

                if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

                filePath = Path.Combine(ConfigurationManager.AppSettings["SurveyPhotosPath"], fileName);

                file.SaveAs(filePath);
            }
            catch(Exception e)
            {
                Logger.Info(e.Message);
                throw e;
            }            
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