<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/Default.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Luoyi.Web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/login.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--------头部--------->
    <header>
        <a class="return" href="window.history.back(-1)"></a>
        <span class="title">首页</span>
    </header>

    <img class="login-img" src="/img/login_img_02.jpg" />
    <!----------主体---------->
    <section>
        <div class="login-form">
            <p>
                <asp:TextBox ID="txtUserName" runat="server" placeholder="姓名"></asp:TextBox>
            </p>
            <p>
                <asp:TextBox ID="txtEmail" runat="server" placeholder="邮箱"></asp:TextBox>
            </p>
            <p>
                <asp:TextBox ID="txtMobile" runat="server" placeholder="手机号"></asp:TextBox>
            </p>
            <p class="agreement">
                <span class="checkbox">
                    <input type="checkbox" />
                    <asp:CheckBox ID="ckbAgreement" runat="server" />
                    <img class="focus" src="/img/icon/checkbox-focus.png" />
                    <img class="blur" src="/img/icon/checkbox.png" />
                </span>
                <span class="xy">我接受服务协议</span>
            </p>
            <p class="readAgree"><a>阅读协议</a></p>
            <p class="btn-wrap">
                <button type="reset">取消</button>
                <asp:Button ID="btnSubmit" runat="server" Text="提交" OnClick="btnSubmit_Click" />
            </p>
        </div>
    </section>

</asp:Content>
