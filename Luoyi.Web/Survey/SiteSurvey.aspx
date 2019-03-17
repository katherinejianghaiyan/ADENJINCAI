<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Controls/OtherPages/SUZHYC.Master" CodeBehind="SiteSurvey.aspx.cs" Inherits="Luoyi.Web.Survey.SiteSurvey" %>

<%@ MasterType VirtualPath="~/Controls/OtherPages/SUZHYC.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/SUZHYC.css?<%= Guid.NewGuid() %>" rel="stylesheet" />
    <script type ="text/javascript" src="https://res.wx.qq.com/open/js/jweixin-1.3.0.js?<%= Guid.NewGuid() %>"></script>
    <script src="../js/Camera.js?<%= Guid.NewGuid() %>"></script>
     <script type="text/javascript">
         $(function () {
             var headGuid = "";
             if ($('footer:last').height() == null)
                 $('#divButton').css('marginBottom', '0.5rem');
             $('input[type=file]').Camera(headGuid,'<%= siteInfo.Code + "','" + siteInfo.GUID + "','" + _UserInfo.UserID  %>',
                 function (d) {
                     headGuid = d;
                 });
             
             $('#surveySubmit').click(function () {
                 var savedata = [];
                 var data = {};
                 
                 $('div.divSurveyItems').each(function () {
                    
                     var allowNull = $(this).attr("status");
                     var radioval = $(this).find("input[type='radio']:checked").val();
                     var textval = $(this).find("input[type='text']").val();
                     var textComp = $(this).find("textarea").val();
                     var group="";                    
                     $(this).find("input[type='checkbox']:checked").each(function(){
                            group+=","+($(this).val());
                     });                     
                     
                     if (typeof (radioval) == "undefined" && (typeof (textval) == "undefined" || textval == "") 
                            && (typeof(textComp) == "undefined" || textComp == "") && (typeof(group)=="undefined" || group=="")) {
                         if (allowNull == "0") {
                             $(".weui_dialog_alert .weui_dialog_bd").html("请完成问卷中所有打*号的内容，非常感谢！");
                             $(".weui_dialog_alert").show().on('click', '.weui_btn_dialog', function () {
                                 $('.weui_dialog_alert').off('click').hide();
                             });
                             savedata = "";
                             return false;
                         }
                         return true;
                     }

                     //取值
                     data = "{'id':'" + $(this).attr("id") + "','answer':'";

                     if (typeof (radioval) != "undefined")
                         data  += radioval;
                     else if (typeof (textval) != "undefined" && textval.trim() != "")
                         data += textval.trim();
                     else if (typeof (textComp) != "undefined" && textComp.trim() != "")
                         data += textComp.trim();
                     else if (typeof (group)!= "undefined" && group!="")
                         data += group.substr(1);
                     data += "'}";
                   
                     savedata.push(data);                          
                 });    

                 if (savedata.length == 0)
                     return;

                 
                 $.ajax({
                     type: "POST",
                     url: "../api/Survey.ashx",
                     data: {
                         headGuid:  headGuid,                        
                         siteGuid: "<%=siteInfo.GUID %>",
                         details: JSON.stringify(savedata),
                       },
                       dataType: "json",
                       success: function (json) {
                           if (json.Status == 1) {
                               $(".weui_dialog_alert .weui_dialog_bd").html("<%= Luoyi.Web.HtmlLang.Lang("SubmitText", "谢谢您的参与")%>");
                               $(".weui_dialog_alert").show().on('click', '.weui_btn_dialog', function () {
                                   $('.weui_dialog_alert').off('click').hide();
                               });
                               $("div.divSurveyItems input[type='radio']:checked").each(function () {
                                   $(this).attr("checked", false);
                               });
                               $("div.divSurveyItems input[type='text']").val("");
                               $("div.divSurveyItems textarea").val("");
                               headGuid = "";
                               $("div.divSurveyItems input[type='checkbox']:checked").each(function () {
                                   $(this).attr("checked", false);
                               });
                               $("div.divSurveyItems input[type='file']").parents('div.divSurveyItems').find("ul li").remove();
                               $('input[type=file]').setPhotoNum();
                           }
                           else {
                               $(".weui_dialog_alert .weui_dialog_bd").html("数据提交失败");
                               $(".weui_dialog_alert").show().on('click', '.weui_btn_dialog', function () {
                                   $('.weui_dialog_alert').off('click').hide();
                               });
                           }
                       }
               });
             });
         })
     </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">       
      <div class="main" style="background-color:white;">
          <!--每日间隔填充-->
            <div style="text-align:left;vertical-align:central;margin-top:2.6rem">
                <%WelcomeState(); %>
           </div>
           <div><% SurveyDetails(); %></div>
            
          
            <div id="divButton"  style ="margin-bottom:3rem; padding-bottom:1rem">

               <a id="surveySubmit" class="linkbutton" ><%= Luoyi.Web.HtmlLang.Lang("Submit", "提交","Master") %></a>
           </div>
    </div>
</asp:Content>

