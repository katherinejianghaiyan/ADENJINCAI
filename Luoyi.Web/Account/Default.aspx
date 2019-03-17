<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Luoyi.Web.Account.Default" %>

<%@ MasterType VirtualPath="~/Controls/Default.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/mine.css?<%= Guid.NewGuid() %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="head-img-wrap">
        <img class="goods-img" src="../img/mine-bg.jpg" />
        <div class="radius">
            <a href="../account/UserInfo.aspx">
                <asp:Image ID="imgHead" runat="server" CssClass="head-img" /></a>
        </div>
    </div>
    <!----------主体---------->
    <section>
        <ul class="menuList">
            <li>
                <a href="../account/topickup.aspx">
                    <img src="../img/icon/icon-mine-get_03.jpg" />
                    <p><%= Luoyi.Web.HtmlLang.Lang("ToPickUp","待取货") %></p>
                    <span class="num">
                        <asp:Literal ID="ltlToPickUp" runat="server"></asp:Literal></span>
                </a>
            </li>
            <% if (!siteInfo.PaymentMethod.Equals("POD")) { //现场付不显示 %>
            <li>
                <a href="../account/notpickup.aspx">
                    <img src="../img/icon/daiquhuo.png" />
                    <p><%= Luoyi.Web.HtmlLang.Lang("Pending", "未取货") %></p>
                    <span class="num">
                        <asp:Literal ID="ltlNotPickUp" runat="server"></asp:Literal></span>
                </a>
            </li>
            <% } %>
            <li>
                <a href="../account/comment.aspx">
                    <img src="../img/icon/icon-mine-com_03.jpg" />
                    <p><%= Luoyi.Web.HtmlLang.Lang("ToRate","评论") %></p>
                    <span class="num">
                        <asp:Literal ID="ltlToComment" runat="server"></asp:Literal></span>
                </a>
            </li>
        </ul>

        <div class="menuWrap">
            <a class="menuBlock" href="../Account/OrderHistory.aspx">
                <img src="../img/icon/finished.png" />
                <span><%= Luoyi.Web.HtmlLang.Lang("Finished ","已完成") %></span>
                <img src="../img/icon/arrowMenu_03.jpg" />
            </a>
            <a class="menuBlock" href="../Account/MyFavorite.aspx">
                <img src="../img/icon/menublock_03.jpg" />
                <span><%= Luoyi.Web.HtmlLang.Lang("MyFavorites","我的收藏") %></span>
                <img src="../img/icon/arrowMenu_03.jpg" />
            </a>
            <a class="menuBlock" href="../Account/MyComment.aspx">
                <img src="../img/icon/menublock_03_03.jpg" />
                <span><%= Luoyi.Web.HtmlLang.Lang("MyComments","我的评论") %></span>
                <img src="../img/icon/arrowMenu_03.jpg" />
            </a>
            <a class="menuBlock" href="../Account/MyCoupon.aspx">
                <img src="../img/icon/coupon.png" />
                <span><%= Luoyi.Web.HtmlLang.Lang("MyCoupon","我的优惠券") %></span>
                <img src="../img/icon/arrowMenu_03.jpg" />
            </a>
            <%--<a class="menuBlock" href="javascript:;">
                <img src="/img/icon/menublock3_03.jpg" />
                <span><%= Luoyi.Web.HtmlLang.Lang("Address","地址") %></span>
                <img src="/img/icon/arrowMenu_03.jpg" />
            </a>--%>
        </div>
    </section>

</asp:Content>
