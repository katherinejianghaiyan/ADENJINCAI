<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Controls/OtherPages/SUZHYC.Master" CodeBehind="ADENProfile.aspx.cs" Inherits="Luoyi.Web.OtherPages.ADENProfile" %>

<%@ MasterType VirtualPath="~/Controls/OtherPages/SUZHYC.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../css/SUZHYC.css?<%= Guid.NewGuid() %>" rel="stylesheet" />--%>
    <script type="text/javascript">
        $(function () {
            iframe = document.getElementById("ifrMain")

            if ("<%= Luoyi.Web.SysConfig.UserLanguage.ToString() %>" == "ZH_CN")
                iframe.src = "ADENProfile/AdenProfileCn.htm?<%= Guid.NewGuid() %>";
            else 
                iframe.src = "ADENProfile/AdenProfileEn.htm?<%= Guid.NewGuid() %>";

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="main">         
        <div>
          <iframe id="ifrMain" style ="width:100%;height:2060px; overflow-y:scroll;" >
          </iframe>
            
        </div>
       <br />
         <div id="footmargin" style="height:1.5rem"></div>
     </div>
</asp:Content>
