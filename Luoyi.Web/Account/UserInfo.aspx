<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/Default.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="Luoyi.Web.Account.UserInfo" %>

<%@ MasterType VirtualPath="~/Controls/Default.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/setting.css?<%= Guid.NewGuid() %>" rel="stylesheet" />
    <script>
        $(function () {
            var $set = $('.setting');
            var $input = $('input.userMsg');
            var $footer = $('footer');
            $set.on('click', function () {
                var $p = $(this).siblings('.userMsg');
                $p.attr('readonly', false).focus();
                $footer.css({
                    "position": "relative",
                });
            });
            $input.on('click', function () {
                $(this).attr('readonly', false).focus();
                $footer.css({
                    "position": "relative",
                });
            });
            $input.on('blur', function () {
                $(this).attr('readonly', true);
                $footer.css("position", "fixed");
                console.log($input.val());

                let reg = /^1[34578][0-9]{9}$/;
                let param = {
                    UserName: $("#<%= txtUserName.ClientID%>").val(),
                    Mobile: $("#<%= txtMobile.ClientID%>").val(),
                    Department: $("#<%= txtDepartment.ClientID%>").val(),
                    Section: $("#<%= txtSection.ClientID%>").val(),
                }
                if ($(this).attr('name') == "ctl00$ContentPlaceHolder1$txtMobile" && typeof (param.Mobile) != "undefined" && param.Mobile != "" && !reg.test(param.Mobile)) {
                    alert("手机号不正确");
                    $(this).val('');
                    return;
                }
                let showMessage = <%= showMessage%>;
                $.ajax({
                    type: "POST",
                    url: "<%= Luoyi.Common.WebHelper.GetUrlPath("/api/UserEdit.ashx") %>",
                    data: param,
                    dataType: "json",
                    success: function (json) {
                        console.log(json);
                        if (showMessage == "1" && param.UserName != "" && param.Moible != "" && param.Department != "" && param.Section != "")
                            window.location.href = "<%= Luoyi.Common.WebHelper.GetUrlPath("/index.aspx") %>";
                    }
                });
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="head-img-wrap">
        <img class="goods-img" src="../img/mine-bg.jpg" />
        <div class="radius">
            <asp:Image ID="imgHead" runat="server" CssClass="head-img" />
        </div>
    </div>
    <!----------主体---------->
    <section>
        <div class="menuWrap">
            <%if ("1".Equals(showMessage)) {%>
            <a class="menuBlock" style="text-align:center;background:#ff0000" href="javascript:;">
                <span><%= Luoyi.Web.HtmlLang.Lang("Attention", "请先维护个人信息") %></span>
            </a>
            <%} %>
            <a class="menuBlock" href="javascript:;">
                <span class="imgWrap">
                    <img src="../img/icon/icon-head-img_03.jpg" /></span>
                <span><%= Luoyi.Web.HtmlLang.Lang("Portrait", "头像") %></span>
                <p class="userMsg">
                    <asp:Image ID="ImgHeader" runat="server" />
                </p>
            </a>
            <a class="menuBlock" href="javascript:;">
                <span class="imgWrap">
                    <img src="../img/icon/icon-name_03.jpg" /></span>
                <span><%= Luoyi.Web.HtmlLang.Lang("Name", "姓名") %></span>
                <img class="setting" src="../img/icon/icon-setting_03.jpg" />
                <asp:TextBox ID="txtUserName" runat="server" CssClass="userMsg" ReadOnly="true"></asp:TextBox>
            </a>
            <a class="menuBlock" href="javascript:;">
                <span class="imgWrap">
                    <img src="../img/icon/icon-phone_03.jpg" /></span>
                <span><%= Luoyi.Web.HtmlLang.Lang("Mobile", "手机号") %></span>
                <img class="setting" src="../img/icon/icon-setting_03.jpg" />
                <asp:TextBox ID="txtMobile" runat="server" TextMode="Number" CssClass="userMsg" ReadOnly="true"></asp:TextBox>
            </a>
            <a class="menuBlock" href="javascript:;">
                <span class="imgWrap">
                    <img src="../img/icon/company_03.jpg" /></span>
                <span><%= Luoyi.Web.HtmlLang.Lang("Department", "部门") %></span>
                <img class="setting" src="../img/icon/icon-setting_03.jpg" />
                <asp:TextBox ID="txtDepartment" runat="server" CssClass="userMsg" ReadOnly="true"></asp:TextBox>
            </a>
            <a class="menuBlock" href="javascript:;">
                <span class="imgWrap">
                    <img src="../img/icon/icon-job_03.jpg" /></span>
                <span><%= Luoyi.Web.HtmlLang.Lang("Section", "科室") %></span>
                <img class="setting" src="../img/icon/icon-setting_03.jpg" />
                <asp:TextBox ID="txtSection" runat="server" CssClass="userMsg" ReadOnly="true"></asp:TextBox>
            </a>
        </div>
    </section>
</asp:Content>
