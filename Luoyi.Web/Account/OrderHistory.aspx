<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/Default.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="Luoyi.Web.Account.OrderHistory" %>

<%@ MasterType VirtualPath="~/Controls/Default.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/orderHistory.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <img class="goods-img" src="../img/mine-bg.jpg" />

    <!----------主体---------->
    <section>
        <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound" OnItemCommand="rptList_ItemCommand">
            <ItemTemplate>
                <!---历史订单--->
                <div class="pickUp-table">
                    <div class="left">
                        <p><%= Luoyi.Web.HtmlLang.Lang("OrderCode","单号",@"/Account/ToPickUp.aspx") %>：<span><%# Eval("OrderCode") %></span></p>
                        <p><%= Luoyi.Web.HtmlLang.Lang("PickupDate","取货时间",@"/Account/ToPickUp.aspx") %>：<span>
                            <span>
                                <%# Eval("RequiredDate", "{0:yyyy-MM-dd}") %>&nbsp;
                                <%# Eval("RequiredDate", "{0:H:mm}").ToString() == "0:00"? "" :  Eval("RequiredDate", "{0:H:mm}")%>&nbsp;
                                <%# Eval("RequiredDinnerType") != null ? Eval("RequiredDinnerType") : (siteInfo.PickupTime == "" ? "" : siteInfo.PickupTime) %>
                            </span></p>
                        <p><%= Luoyi.Web.HtmlLang.Lang("GuaranteeDate","保质期",@"/Account/ToPickUp.aspx") %>：<span><span><%# Eval("RequiredDate", "{0:yyyy-MM-dd}") %></span></p>
                        <%--<p><%= Luoyi.Web.HtmlLang.Lang("Comments","备注") %>：<span><span><%# Eval("Comments") %></span></p>--%>
                        <asp:Repeater ID="rptItem" runat="server">
                            <ItemTemplate>
                                <p><%# Eval("ItemName") %>×<%# Eval("Qty") %></p>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <asp:LinkButton ID="lbtnAgainOrder" runat="server" CssClass="againOrder" CommandArgument='<%# Eval("OrderID") %>' CommandName="Again" Visible="false">再来一单</asp:LinkButton>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </section>
</asp:Content>
