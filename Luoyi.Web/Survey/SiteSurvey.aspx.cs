using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Common;
using Luoyi.Entity;
using System.Globalization;
using System.Data;


namespace Luoyi.Web.Survey
{
    public partial class SiteSurvey : PageBase
    {
        
        public static List<SurveyInfo> SurveyInfo =null;
        public static string surveyTitle = "";
        public static string welcomeText = "";
        public static List<SurveyDetails> surveyContent = null;
        public static bool showCamera = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //多语言不显示
                Master.ShowDefault(false, false);
                Master.MasterHome();
                //调研主数据
                SurveyInfo = BLL.Survey.SurveyMaster(siteInfo.BUGUID, siteInfo.GUID, WebHelper.GetQueryString("type"),SysConfig.UserLanguage.ToString());
            }
        }



        /// <summary>
        /// 调研标题
        /// </summary>
        /// <returns></returns>
        public string SurveyTitle()
        {
            surveyTitle = SurveyInfo.FirstOrDefault().surveyTitle;

            string result = "";
            if (!string.IsNullOrWhiteSpace(surveyTitle))
                result = string.Format(" < p style=\"margin-top:25%;font-size:26px\"><span>{0}</span></p>", surveyTitle);
            return result;
        }


        /// <summary>
        /// 调研致辞
        /// </summary>
        /// <returns></returns>
        public void WelcomeState()
        {

            welcomeText = SurveyInfo.FirstOrDefault().welcomeText;

            string result = "";
            if (!string.IsNullOrWhiteSpace(welcomeText))
                result = string.Format("<span style=\"font-size:20px\">{0}</span>", welcomeText);
            else
                result = string.Format("<br>");
           Response.Write(result);
        }


        public void SurveyDetails()
        {
            surveyContent = SurveyInfo.FirstOrDefault().surveyLines;
            string result = "";
            
            if (surveyContent != null || !surveyContent.Any())
            {
                int i = 0;
                foreach (var item in surveyContent)
                {
                    string noteFlag = string.Empty;
                    result += string.Format("<div id='{0}' status='{1}' class='divSurveyItems'>\r\n",item.lineGuid,item.allowNull);
                    result += string.Format("<div {3}>{0}&nbsp;.&nbsp;{1}<font color='red'><b>{2}</b></font>{4}",
                        ++i,item.itemName,item.allowNull == 1 ? "" : "*", 
                        item.itemStyle.ToLower() == "image"? "class='fileinput-button'" : "",
                        item.itemStyle.ToLower() == "image" ? "" : "</div>");
                    if (string.IsNullOrWhiteSpace(item.itemAnswer))
                    {
                        if (item.itemStyle.ToLower() == "textarea")
                        {
                            result += "<div><textarea class='multitext' id=\"txt" + item.lineGuid + "\" class=\"onetext\" name=\"" + noteFlag + "\"></textarea></div>\r\n";
                        }
                        else if (item.itemStyle.ToLower() == "text")
                        {
                            result += "<div><input type=\"text\" id=\"txt" + item.lineGuid + "\" class=\"onetext\" name=\"" + noteFlag + "\" /></div>\r\n";
                        }
                        else if (item.itemStyle.ToLower() == "image")
                        {
                            //拍照按钮
                            result += "<span><font style='font-size:0.7rem;margin-right:0.3rem'>(≤ 4pc)</font>" +
                                "<img src='../img/icon/camera.png' style='width:1rem; vertical-align: bottom;'/>" +
                                "</span>" +
                                string.Format("<input id='f{0}' name='fileCamera' type='file' capture='camera' accept='image/*' />", item.lineGuid);
                            result += "</span>";
                            //显示照片
                            result += "<div style='height:4rem'><ul class='weui_uploader_files js_previews'></ul></div></div>";
                        }

                        result += "</div>";
                        continue;
                    }

                    Array list = item.itemAnswer.Split(new char[] { ';' });
                    int x = 0;
                    string sline = "";
                    foreach (var line in list)
                    {
                        sline += string.Format("<span style='width:6.8rem;{4}'>"
                            + "<input type=\"{3}\" name=\"{0}\" value=\"{1}\" id='{2}''/>&nbsp;&nbsp;"
                            + "<label style=\"font-size:0.7rem\" for='{2}'>{1}</label></span>",
                             item.lineGuid, line, item.lineGuid + (x++).ToString(), item.itemStyle, (x % 2 == 0)? "" : "float:left;" );

                        //if (x % 2 == 1) continue;
                        // Angel 修改
                        if (x % 2 == 1 && x<list.Length) continue;

                        result += string.Format("<div style='padding-left:0.3rem;width:13.6rem '>{0}</div>", sline);
                        // Angel 修改
                        if (x % 2 == 1 && x == list.Length)
                            result += "<br>";

                        sline = "";
                    }

                    result += "</div>";
                }
            }

            Response.Write( result);
        }

        public string ShowCamera()
        {
            if (showCamera)
                return "";
            else
                return "hidden=''";
        }
    }
}