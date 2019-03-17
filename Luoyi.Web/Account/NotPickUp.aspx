<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/Default.Master" AutoEventWireup="true" CodeBehind="NotPickUp.aspx.cs" Inherits="Luoyi.Web.Account.NotPickUp" %>

<%@ MasterType VirtualPath="~/Controls/Default.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/pickUp.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <img class="goods-img" src="../img/mine-bg.jpg" />

    <!----------主体---------->
    <section>
        <div class="msg">
            <span><%= DeliveryMsg  %></span>
        </div>
        <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound" OnItemCommand="rptList_ItemCommand">
            <ItemTemplate>
                <!---取货单列表--->
                <div class="pickUp-table">
                    <div class="left">
                        <asp:Image ID="imgBarCode" runat="server" CssClass="ywm" />
                    </div>
                    <div class="right">
                        <p><%= Luoyi.Web.HtmlLang.Lang("OrderCode", "单号",@"/Account/ToPickUp.aspx") %>：<span><%# Eval("OrderCode") %></span></p>
                        <p><%= Luoyi.Web.HtmlLang.Lang("ShippedDate", "取货时间",@"/Account/ToPickUp.aspx") %>：
                            <span><%# Eval("RequiredDate", "{0:yyyy-MM-dd}") %>&nbsp;
                                <%# Eval("RequiredDate", "{0:H:mm}").ToString() == "0:00"? "" :  Eval("RequiredDate", "{0:H:mm}")%>&nbsp;
                                <%# Eval("RequiredDinnerType") != null ? Eval("RequiredDinnerType") : (siteInfo.PickupTime == "" ? "" : siteInfo.PickupTime) %>
                            </span></p>
                        <p><%= Luoyi.Web.HtmlLang.Lang("GuaranteeDate", "保质期",@"/Account/ToPickUp.aspx") %>：<span><%# Eval("RequiredDate", "{0:yyyy-MM-dd}") %></span></p>
                        <%--<p><%= Luoyi.Web.HtmlLang.Lang("Comments", "备注") %>：<span><%# Eval("Comments") %></span></p>--%>
                        <asp:Repeater ID="rptItem" runat="server">
                            <ItemTemplate>
                                <p><%# Eval("ItemName") %>×<%# Eval("Qty") %></p>
                            </ItemTemplate>
                        </asp:Repeater>
                        <p>总计：<span>￥<%# Eval("PaymentAmount","{0:G0}") %></span></p>
                        <p>
                            <asp:LinkButton ID="lbtnSend" runat="server" CssClass="sendnews" CommandName="Send" CommandArgument='<%# Eval("OrderCode") %>'>发送订单</asp:LinkButton>
                        </p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </section>
</asp:Content>
