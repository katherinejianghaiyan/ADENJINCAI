<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/Default.Master" AutoEventWireup="true" CodeBehind="MyComment.aspx.cs" Inherits="Luoyi.Web.Account.MyComment" %>

<%@ MasterType VirtualPath="~/Controls/Default.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/myFavorite.css" rel="stylesheet" />
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
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <div class="myList">
                    <a class="left" href="/ItemDetail.aspx?ItemID=<%# Eval("ItemID") %>">
                        <img src="../<%# Eval("Image1") %>" /></a>
                    <div class="right">
                        <p class="title"><%# Eval("ItemName") %></p>
                        <p class="star">
                            <img src="../img/icon/star_<%# Eval("Score") %>_03.jpg" />
                        </p>
                        <p class="commentDetail">
                            <%# Eval("Content") %>
                        </p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </section>
</asp:Content>
