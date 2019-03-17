<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/Default.Master" AutoEventWireup="true" CodeBehind="Coupon.aspx.cs" Inherits="Luoyi.Web.Coupon" %>

<%@ MasterType VirtualPath="~/Controls/Default.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/myCoupon.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>

        <div class="addCoupon">
            <asp:TextBox ID="txtCouponCode" runat="server" CssClass="addText" placeholder="输入优惠码"></asp:TextBox>
            <asp:Button ID="btnCouponAdd" runat="server" CssClass="addBtn" Text="添加优惠券" OnClick="btnCouponAdd_Click" />
        </div>
        <div class="couponList">
            <p class="title">
                <span>
                    <asp:Literal ID="ltlCouponRule" runat="server"></asp:Literal></span><span>某些特殊代金券可以小额抵扣,仅使用一次</span>
            </p>
            <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand" OnItemDataBound="rptList_ItemDataBound">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCoupon" runat="server" CssClass="couponBlock" CommandName="UseCoupon" CommandArgument='<%# Eval("ID") %>'>
                        <img src="<%# Eval("Image") %>" alt="<%# Eval("CouponName") %>" />
                        <span>优惠券有效期<%# Convert.ToDateTime( Eval("UseBeginTime")) > Convert.ToDateTime(Eval("StartTime")) ? Eval("UseBeginTime","{0:yyyy-MM-dd}") :Eval("StartTime","{0:yyyy-MM-dd}")%>至<%# Convert.ToDateTime( Eval("UseEndTime")) > Convert.ToDateTime(Eval("StopTime")) ? Eval("StopTime","{0:yyyy-MM-dd}") :Eval("UseEndTime","{0:yyyy-MM-dd}")%></span>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>
</asp:Content>
