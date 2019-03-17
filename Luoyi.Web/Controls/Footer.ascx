<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="Luoyi.Web.Controls.Footer" %>

<!--------底部--------->
<footer>
    <ul>
        <li class="<%= FooterBtnClass %>">
            <a href="<%= Luoyi.Common.WebHelper.GetUrlPath("/Default.aspx") %>">
                <p>
                    <i class="iconfont">&#xe61d;</i>
                </p>
                <p><%= Luoyi.Web.HtmlLang.Lang("Master_Home","首页") %></p>
            </a>
        </li>
         
        <% if (!isPOD) //现场付不显示 2017-5-9
                 { %>
       <li class="<%= FooterBtnClass %>">
            
            <a href="<%= Luoyi.Common.WebHelper.GetUrlPath("/account") %>/NotPickUp.aspx">
                <p>
                    <i class="iconfont">&#xe60a;</i>
                </p>
                <p><%= Luoyi.Web.HtmlLang.Lang("Master_Message", "未取货") %></p>
            </a>
            
       </li>
        <% } %>
             
        <li class="<%= FooterBtnClass%>">
            <a href="<%= Luoyi.Common.WebHelper.GetUrlPath("/MyCart.aspx") %>">
            <p>
                <img src="<%= Luoyi.Common.WebHelper.GetUrlPath("/img") %>/icon/footer-cart_03.jpg" />
                <span class="numCart"><%= CartQty %></span>
            </p>
        </a>
        </li>
        <li class="<%= FooterBtnClass %>"><a href="<%= Luoyi.Common.WebHelper.GetUrlPath("/account") %>/ToPickUp.aspx">
            <p>
                <i class="iconfont">&#xe602;</i>
            </p>
            <p><%= Luoyi.Web.HtmlLang.Lang("Master_Concept","待取货") %></p>
        </a></li>
        <li class="<%= FooterBtnClass %>">
            <a href="<%= Luoyi.Common.WebHelper.GetUrlPath("/account") %>/Default.aspx">
                <p>
                    <i class="iconfont">&#xe63f;</i>
                </p>
                <p><%= Luoyi.Web.HtmlLang.Lang("Master_Account","个人中心") %></p>
            </a>
        </li>
    </ul>
</footer>
