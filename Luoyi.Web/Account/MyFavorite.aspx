<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/Default.Master" AutoEventWireup="true" CodeBehind="MyFavorite.aspx.cs" Inherits="Luoyi.Web.Account.MyFavorite" %>

<%@ MasterType VirtualPath="~/Controls/Default.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/myFavorite.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $("section").on("click", ".favDel", function () {
                var $this = $(this);
                $.ajax({
                    type: "POST",
                    url: "../api/Favorite.ashx",
                    data: {
                        TypeID: 0,
                        ItemGUID: $this.attr("data-itemguid"),
                    },
                    dataType: "json",
                    success: function (json) {
                        console.log(json);
                        if (json.Status == 1) {
                            console.log(json.Message);
                            $this.closest(".myList").remove();
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
            });
        });
    </script>
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
                    <a class="left" href="../ItemDetail.aspx?ItemID=<%# Eval("ItemID") %>">
                        <img src="../<%# Eval("Image1") %>" /></a>
                    <div class="right">
                        <p class="title"><%# Eval("ItemName") %></p>
                        <p class="star">
                            <img src="../img/icon/star_5_03.jpg" />
                        </p>
                        <p class="caozuo">
                            <img class="addCart" data-cartadd="true" data-itemid="<%# Eval("ItemID") %>" src="/img/icon/addCart_07.jpg" /><img class="favDel" data-itemguid="<%# Eval("ItemGUID") %>" src="/img/icon/icon-trash.png" />
                        </p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </section>
</asp:Content>
