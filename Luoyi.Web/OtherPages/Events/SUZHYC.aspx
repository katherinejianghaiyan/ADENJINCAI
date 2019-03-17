<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Controls/OtherPages/SUZHYC.Master" CodeBehind="SUZHYC.aspx.cs" Inherits="Luoyi.Web.OtherPages.Events.SUZHYC" %>

<%@ MasterType VirtualPath="~/Controls/OtherPages/SUZHYC.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../css/SUZHYC.css?<%= Guid.NewGuid() %>" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
      <div class="title">
      </div>
      <div class="main">
      <!--每日间隔填充-->
      <div class="upper"></div>
           <div style="text-align:center">
               
               <img class="event" src="<%=GetPicUrl() %>" style="width:90%;" />
               <p class="logo-text1">
                   <%= GetStartDate()%>
               </p>

           </div>
          <br />
          <div class="lower"></div>
      </div>

</asp:Content>
