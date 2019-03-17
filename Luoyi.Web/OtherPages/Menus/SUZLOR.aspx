<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Controls/OtherPages/SUZHYC.Master" CodeBehind="SUZLOR.aspx.cs" Inherits="Luoyi.Web.OtherPages.Menus.SUZLOR" %>

<%@ MasterType VirtualPath="~/Controls/OtherPages/SUZHYC.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/SUZHYC.css?<%= Guid.NewGuid() %>" rel="stylesheet" />
   <script type="text/javascript">

       $(function () {
           var $icon = $('.classify');
           var $list = $('.goods-Classify');

           $icon.click(function () {
               if ($list.hasClass('active')) {
                   $list.removeClass('active');
                   $icon.prop('src', '<%= Luoyi.Common.WebHelper.GetUrlPath("/img") %>/icon/fenlei_03.png');
               } else {
                   $list.addClass('active');
                   $icon.prop('src', '../../img/icon/cloase_03.jpg');
               }
           });
       })
   </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <div class="title">
      </div>
      <div class="main">
      <!--每日间隔填充-->
      <div class="upper"></div>
           <div style="text-align:center">               
            <% ShowImage(); %>
           </div>
          <br />
          <div class="lower"></div>
      </div>
</asp:Content>

