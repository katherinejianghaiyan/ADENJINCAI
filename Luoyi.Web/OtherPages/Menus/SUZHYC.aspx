<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Controls/OtherPages/SUZHYC.Master" CodeBehind="SUZHYC.aspx.cs" Inherits="Luoyi.Web.OtherPages.Menus.SUZHYC" %>

<%@ MasterType VirtualPath="~/Controls/OtherPages/SUZHYC.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       
    <%--   <link href="<%= Luoyi.Common.WebHelper.GetUrlPath("/css") %>/820LIS.css?" rel="stylesheet" />--%>
    <link href="<%= Luoyi.Common.WebHelper.GetUrlPath("/css") %>/owl/owl.carousel.css" rel="stylesheet" />
    <link href="<%= Luoyi.Common.WebHelper.GetUrlPath("/css") %>/owl/owl.theme.css" rel="stylesheet" />
    <link href="<%= Luoyi.Common.WebHelper.GetUrlPath("/css") %>/owl/owl.aden.css" rel="stylesheet" />
    <link href="<%= Luoyi.Common.WebHelper.GetUrlPath("/css") %>/SUZHYC.css?<%= Guid.NewGuid() %>" rel="stylesheet" />
    <script src="<%= Luoyi.Common.WebHelper.GetUrlPath("/js") %>/owl/owl.carousel.js?<%= Guid.NewGuid() %>"></script>
    <script src="<%= Luoyi.Common.WebHelper.GetUrlPath("/js") %>/touchSlide.js?<%= Guid.NewGuid() %>"></script>
    <script type="text/javascript">

        $(function () {
            var $icon = $('.classify');
            var $list = $('.goods-Classify');

            $icon.click(function () {
                if ($list.hasClass('active')) {
                    $list.removeClass('active');
                    $icon.prop('src', '<%= Luoyi.Common.WebHelper.GetUrlPath("/img") %>/icon/fenlei_03.png');
                } else {
                    $list.addClass('active');
                    $icon.prop('src', '../../img/icon/cloase_03.jpg');
                }
            });

            $('#menu').touchSlide($("#owl-demo"));                    
            $('#owl-demo').owlCarousel({
                singleItem: true,
                afterMove: moved,
                afterInit: function () {  
                    $("#owl-demo").trigger('owl.jumpTo', $('#<%= hfPageIndex.ClientID%>').val());
                },
            });

            function moved() {
                $('#<%= hfPageIndex.ClientID%>').val($('.owl-page').index($('.active')));
                $('#<%=btnNext.ClientID%>').click();
            };
        })      

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="owl-demo" class="owl-carousel" style="touch-action: none; width:16rem; position:fixed; top:0rem;z-index:9;">
        <asp:Repeater ID="rptLogo" runat="server">
            <ItemTemplate>
                <a>
                  <%# GetHtmlLogo(DataBinder.Eval(Container.DataItem, "day").ToString()) %>
                   <p class="logo-text3">
                       <%# 
                        GetLogoDate(DataBinder.Eval(Container.DataItem, "startDate").ToString())
                        %>
                    </p>
            </ItemTemplate>
        </asp:Repeater>
        </div>
    <!----------主体---------->
    <div id="menu" style ="margin-top : 7.5rem; ">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="diySection"> 
                <div class="title">
                    <p>&nbsp;</p>
                </div>
                <div class="main">
                    <!--每日间隔填充-->
                    <div class="upper"></div>
                        <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                            <ItemTemplate>
                                <%# GetMealType(DataBinder.Eval(Container.DataItem, "dinner").ToString()) %>
                                        <asp:Repeater runat="server" ID="rptItemList">
                                            <ItemTemplate>
                                             <%# GetHtmlWindow(DataBinder.Eval(Container.DataItem, "windowName").ToString(),"windowName") %><br />
                                             <%# GetHtmlWindow(DataBinder.Eval(Container.DataItem, "val2").ToString(),"item") %>
                                            </ItemTemplate>
                                        </asp:Repeater>
    
                                <%# GetEmptyLine() %>
                            </ItemTemplate>
                        </asp:Repeater>

                    <div class="lower">
                    </div>
                </div>
                <asp:HiddenField ID="hfPageIndex" Value="0" runat="server" />
                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" CssClass="btn" />
            </section>
        </ContentTemplate>
            </asp:UpdatePanel>
        </div>
</asp:Content>
