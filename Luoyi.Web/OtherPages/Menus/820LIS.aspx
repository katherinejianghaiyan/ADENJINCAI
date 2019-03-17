<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Controls/Default.Master" CodeBehind="820LIS.aspx.cs" Inherits="Luoyi.Web.OtherPages.Menus._820LIS" %>

<%@ MasterType VirtualPath="~/Controls/Default.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="/css/820LIS.css?<%= Guid.NewGuid() %>" rel="stylesheet" />
    <link href="/css/myCart.css?<%= Guid.NewGuid() %>" rel="stylesheet" />
    <link href="/css/owl/owl.carousel.css" rel="stylesheet" />
    <link href="/css/owl/owl.theme.css" rel="stylesheet" />
     <link href="/css/owl/owl.aden.css" rel="stylesheet" />
    <script src="/js/owl/owl.carousel.js"></script>
    <%--<script src="<%= Luoyi.Common.WebHelper.GetUrlPath("/js") %>/owl/owl.carousel.js?<%= Guid.NewGuid() %>"></script>
    <script src="<%= Luoyi.Common.WebHelper.GetUrlPath("/js") %>/touchSlide.js?<%= Guid.NewGuid() %>"></script>--%>
    <script type="text/javascript">

        $(function () {
            var $icon = $('.classify');
            var $list = $('.goods-Classify');

            $icon.click(function () {
                if ($list.hasClass('active')) {
                    $list.removeClass('active');
                    $icon.prop('src', '/img/icon/fenlei_03.png');
                } else {
                    $list.addClass('active');
                    $icon.prop('src', '/img/icon/cloase_03.jpg');
                }
            });

            //Angel
            //$('#menu').touchSlide($("#owl-demo"));
            $('#owl-demo').owlCarousel({
                singleItem: true,
                afterMove: moved,
                <%--afterInit: function () {  
                    $("#owl-demo").trigger('owl.jumpTo', $('#<%= hfPageIndex.ClientID%>').val());
                },--%>
            });

            function moved() {
                $('#<%= hfPageIndex.ClientID%>').val($('.owl-page').index($('.active')));
                $('#<%=btnNext.ClientID%>').click();

            };
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="owl-demo" class="owl-carousel" style="touch-action: none;">
        <asp:Repeater ID="rptLogo" runat="server">
            <ItemTemplate>
                <a>
                    <img class="logo" src="/img/OtherPages/820LIS/icons/headline.png" />
                    <p class="logo-text1">
                        <%# 
                        GetLogoMenu(DataBinder.Eval(Container.DataItem, "dataType").ToString())
                        %>
                    </p>
                    <p class="logo-text2">
                        <%-- <%= logoText2 %>--%>
                        <%# 
                        GetLogoDate(DataBinder.Eval(Container.DataItem, "startDate").ToString(),DataBinder.Eval(Container.DataItem, "endDate").ToString())
                        %>
                    </p>
            </ItemTemplate>
        </asp:Repeater>

    </div>

    <!----------主体---------->
    <%--Angel--%>
    <%--<div id="menu">--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <section class="diySection">
                <div class="title">
                    <p>Menu & Ingredients</p>
                </div>
                <div class="main">
                    <!--每日间隔填充-->
                    <div class="upper"></div>

                    <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                        <ItemTemplate>
                            <%# GetHtmlImage(Container.ItemIndex, DataBinder.Eval(Container.DataItem, "day").ToString()) %>
                            <asp:Repeater runat="server" ID="rptItemList">
                                <ItemTemplate>
                                    <%# GetHtmlString(DataBinder.Eval(Container.DataItem, "val1").ToString(), DataBinder.Eval(Container.DataItem, "val2").ToString()) %>
                                </ItemTemplate>
                            </asp:Repeater>
                            <%# GetEmptyLine(Container.ItemIndex) %>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="lower">
                    </div>
                </div>
                <%= GetDashBord()%>
                <asp:HiddenField ID="hfPageIndex" Value="0" runat="server" />
                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" CssClass="btn" />
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
        <%--</div>--%>
</asp:Content>
