<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="Luoyi.Web.Controls.Header" %>
<!--------头部--------->
<header>
    <asp:PlaceHolder ID="phClass" runat="server" Visible="false">
        <img class="classify" src="<%= Luoyi.Common.WebHelper.GetUrlPath("/img/icon/fenlei_03.png") %>" />   
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phSearch" runat="server" Visible="false">
        <div class="seaForm">
            <input class="searchText" type="search" placeholder="搜索值" />
            <input class="rest" type="button" value="取消"/>
        </div>
        <img class="search" src="<%= Luoyi.Common.WebHelper.GetUrlPath("/img/icon/search_03.png") %>" />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phBack" runat="server" Visible="false">
        <a class="return" href="javascript:window.history.back(-1)"></a>
    </asp:PlaceHolder>
    <span class="title">
        <%= title %>
    </span>
    <asp:LinkButton ID="lbtnLanguage" runat="server" CssClass="language" OnClick="lbtnLanguage_Click"><%= Luoyi.Web.HtmlLang.Lang("Master_Lang","English") %></asp:LinkButton>
    <!---商品分类列表---->
    <ul class="goods-Classify">
        <%--<li><a href="/Default.aspx">全部</a></li>--%>
        <asp:Repeater ID="rptItemCustomClass" runat="server">
            <ItemTemplate>
                <li><a href="<%= Luoyi.Common.WebHelper.GetUrlPath("/Default.aspx") %>?CustomID=<%# Eval("ID") %>"><%# Eval("ClassName") %></a></li>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Repeater ID="rptItemClass" runat="server">
            <ItemTemplate>
                <li><a href="<%= Luoyi.Common.WebHelper.GetUrlPath("/Default.aspx") %>?ClassGUID=<%# Eval("GUID") %>"><%# Eval("ClassName") %></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</header>
