<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/Default.Master" AutoEventWireup="true" CodeBehind="ItemSetDetail.aspx.cs" Inherits="Luoyi.Web.ItemSetDetail" %>

<%@ MasterType VirtualPath="~/Controls/Default.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/goodsDetail.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            
            $('.goods-price').on('click', 'img.fav', function () {
                var $this = $(this);
                if (!$this.hasClass('active')) {
                    $.ajax({
                        type: "POST",
                        url: "/api/Favorite.ashx",
                        data: {
                            TypeID: 1,
                            ItemGUID: $this.attr("data-itemguid"),
                        },
                        dataType: "json",
                        success: function (json) {
                            console.log(json);
                            if (json.Status == 1) {
                                console.log(json.Message);
                                $this.prop('src', '/img/icon/icon-fav-active.png').addClass('active');
                                $('#toast .weui_toast_content').html("收藏成功");
                                $('#toast').show();
                                setTimeout(function () {
                                    $('#toast').hide();
                                }, 2000);
                            }
                            else {
                                console.log(json.Message);
                                $(".weui_dialog_alert .weui_dialog_bd").html("收藏失败");
                                $(".weui_dialog_alert").show().on('click', '.weui_btn_dialog', function () {
                                    $('.weui_dialog_alert').off('click').hide();
                                });
                            }
                        }
                    });
                } else {
                    $.ajax({
                        type: "POST",
                        url: "/api/Favorite.ashx",
                        data: {
                            TypeID: 0,
                            ItemGUID: $this.attr("data-itemguid"),
                        },
                        dataType: "json",
                        success: function (json) {
                            console.log(json);
                            if (json.Status == 1) {
                                console.log(json.Message);
                                $this.prop('src', '/img/icon/icon-fav-def.png').removeClass('active');
                                $('#toast .weui_toast_content').html("取消收藏成功");
                                $('#toast').show();
                                setTimeout(function () {
                                    $('#toast').hide();
                                }, 2000);
                            }
                            else {
                                console.log(json.Message);
                                $(".weui_dialog_alert .weui_dialog_bd").html("取消收藏失败");
                                $(".weui_dialog_alert").show().on('click', '.weui_btn_dialog', function () {
                                    $('.weui_dialog_alert').off('click').hide();
                                });
                            }
                        }
                    });
                }
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="head-img-wrap">
        <asp:Image ID="Image3" runat="server" CssClass="goods-img" />
    </div>
    <!----------主体---------->
    <section>
        <p class="goods-name">
            <asp:Literal ID="ltlItemName" runat="server"></asp:Literal>
        </p>
        <p class="goods-sku">
            <span class="sku">
                <img src="/img/icon/icon-piece_06.jpg" /><small>
                    <asp:Literal ID="ltlDishSize" runat="server"></asp:Literal>
                </small>
            </span>
            <span class="addCart">
                <img src="/img/icon/addCart_07.jpg" data-cartadd="true" data-itemid="<%= ItemID %>" />
            </span>
        </p>
        

        <div class="goods-price">
            <p class="price">
                <span>￥<asp:Literal ID="ltlPrice" runat="server"></asp:Literal></span>
                <img class="fx" src="/img/icon/icon-fx_03.jpg" />
                <asp:Image ID="imgFav" runat="server" CssClass="fav" ImageUrl="/img/icon/icon-fav-def.png" />
            </p>
            <p class="comment">
                <span><%= Luoyi.Web.HtmlLang.Lang("Score","评分") %>：</span>
                <img class="star" src="/img/icon/star_5_03.jpg" />
                <span><%= Luoyi.Web.HtmlLang.Lang("Comment","评论") %>：</span>
                <span>
                    <asp:Literal ID="ltlComment" runat="server"></asp:Literal></span>
            </p>
        </div>
        <div class="product">           
            <div class="proInfo">                
                <!--成分-->
                <p class="constituents-title">Menu</p>
                <ul class="set-content">
                    <asp:Repeater ID="rptBom" runat="server">
                        <ItemTemplate>
                            <li><span><%# Eval("ItemName") %></span><span><%# Eval("StdQty","{0:F0}") %><%# Eval("NameEn") %></span></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="commentDiv">
                <p class="comment">
                    <span><%= Luoyi.Web.HtmlLang.Lang("CommentRate","好评率") %>：</span>
                    <img class="star" src="/img/icon/star_4_03.jpg" />
                    <span><%= Luoyi.Web.HtmlLang.Lang("Average","平均分数") %>：</span>
                    <span>4.5</span>

                </p>
                <p class="comment commentNum">
                    <span>
                        <span><%= Luoyi.Web.HtmlLang.Lang("Comment","评论") %>：</span>
                        <span>
                            <asp:Literal ID="ltlComment1" runat="server"></asp:Literal></span>
                    </span>
                </p>
                <asp:Repeater ID="rptComment" runat="server">
                    <ItemTemplate>
                        <div class="commentList">
                            <p class="title">
                                <img src="/img/icon/icon-user.jpg" />
                                <span class="name"><%# Eval("UserName") %></span>
                                <span class="fr time"><%# Eval("CommentTime","{0:yyyy-MM-dd}") %></span>
                                <span class="fr"><%= Luoyi.Web.HtmlLang.Lang("Score","评分") %>：<%# Eval("Score","{0:F0}") %></span>
                            </p>
                            <p class="content">
                                <%# Eval("Content") %>
                            </p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="smile">
            <img src="/img/icon/icon-btm_07.jpg" />
        </div>
    </section>
</asp:Content>
