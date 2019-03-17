<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Luoyi.Web.Default" %>

<%@ MasterType VirtualPath="~/Controls/Default.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./css/default.css?<%= Guid.NewGuid() %>" rel="stylesheet" />
    <link href="./css/owl.carousel.min.css" rel="stylesheet" />
    <link href="./css/owl.theme.default.min.css" rel="stylesheet" />
    <script type="text/javascript">

        $(function () {
            var $icon = $('.classify');
            var $list = $('.goods-Classify');

            $icon.click(function () {
                if ($list.hasClass('active')) {
                    $list.removeClass('active');
                    $icon.prop('src', './img/icon/fenlei_03.png');
                } else {
                    $list.addClass('active');
                    $icon.prop('src', './img/icon/cloase_03.jpg');
                }

            });

            var $form = $('.seaForm');
            var $seaBtn = $('.search');
            var $area = $('.searchText');
            $seaBtn.on('click', function () {
                $form.toggle();
                $area.focus();
            });
            $area.on('blur', function () {
                $form.hide();
            });

            $area.keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    if (window.location.href.indexOf('?') > -1) {
                        if (window.location.href.indexOf('Keyword') > -1) {
                            var re = eval('/(Keyword=)([^&]*)/gi');
                            window.location.href = window.location.href.replace(re, 'Keyword=' + $area.val());
                        }
                        else {
                            window.location.href = window.location.href + "&Keyword=" + $area.val();
                        }
                    }
                    else {
                        window.location.href = window.location.href + "?Keyword=" + $area.val();
                    }
                }
            });
            $("#ToTop").click(function() {
                $("html,body").animate({scrollTop:0}, 500);
            }); 

            var $addIcon = $('.addMore');
            var $wrap = $('#itemlist');

            $(window).scroll(function () {
                var docuH = parseInt($(document).height());
                var winH = parseInt($(window).height());
                var scroH = parseInt($(window).scrollTop());
                
            if(scroH < winH/5) $("#ToTop").fadeOut();
            else $("#ToTop").fadeIn();

                if (winH + scroH == docuH && $wrap.attr("data-pagemore") == "true") {
                    //$wrap.css("margin-bottom", "5rem");
                    //$addIcon.stop().fadeIn();
                    $.ajax({
                        type: "POST",
                        url: "./api/Item.ashx",
                        data: {
                            PageIndex: $wrap.attr("data-pageindex"),
                            ClassGUID: $wrap.attr("data-classguid"),
                            CustomID: $wrap.attr("data-customid"),
                            Keyword: $wrap.attr("data-keyword")
                        },
                        dataType: "json",
                        success: function (json) {
                            
                            if (json.Status == 1) {
                                $wrap.attr({ "data-pageindex": json.Data.PageIndex, "data-classguid": json.Data.ClassGUID, "data-pagemore": json.Data.PageMore });
                                $wrap.append(json.Data.PageContent);
                            }
                        }
                    });

                  //  $addIcon.fadeOut();
                } else if ($wrap.attr("data-pagemore") == "false") {
                    $addIcon.css("position", "relative").show().find('img').hide().siblings('p').text('');
                }
            });
            //$('#owl').owlCarousel({
            //    items: 1,
            //    autoplay: true,
            //    autoplayTimeout: 5000,
            //    loop: true
            //});
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div > 
        <a class="item">
            <img class='<%= string.IsNullOrWhiteSpace(itemimg)? "goods-img" : "items-img" %>' 
                src='./img/<%= string.IsNullOrWhiteSpace(itemimg)? 
                    (siteInfo == null || string.IsNullOrWhiteSpace(siteInfo.LoadImg)? "login_img_02.jpg" : 
                    @"siteimgs\"+siteInfo.LoadImg)
                    : @"ItemImgs/"+ itemimg %>' />        
        </a>
    </div>
    <!----------主体---------->
    <asp:Literal ID="ltlList" runat="server"></asp:Literal>
    <% if (SplitPages && string.IsNullOrWhiteSpace(itemimg))
        { %>
<div id="ToTop" style="position:fixed;bottom:3rem;right:1rem;text-align:right;display:none;z-index:9;">
<img style="height:2rem;width:2rem;" src="./img/icon/ToTop.png" /></div>
    <div class="addMore" style="position: relative; bottom: 2rem;width: 16rem;  text-align: center;">
        <img style="height: 0.7rem; width: 0.7rem;" src="./img/loading.gif" />
        <p style="font-size: 0.7rem; color: #333; text-align: center;display:inline">More...</p>
    </div>

<div style="height:2rem"></div>
        <%} %>
</asp:Content>
