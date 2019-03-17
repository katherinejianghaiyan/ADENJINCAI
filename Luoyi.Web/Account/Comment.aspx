<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/Default.Master" AutoEventWireup="true" CodeBehind="Comment.aspx.cs" Inherits="Luoyi.Web.Account.Comment" %>

<%@ MasterType VirtualPath="~/Controls/Default.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/comment.css" rel="stylesheet" />
    <script type="text/javascript">

        function CheckComment(obj) {
            if ($(obj).closest(".commentWrap").find(".grade input[type='hidden']").val() != "0") {
                return true;
            }
            else {
                $(".weui_dialog_alert .weui_dialog_bd").html("请输入评分");
                $(".weui_dialog_alert").show().on('click', '.weui_btn_dialog', function () {
                    $('.weui_dialog_alert').off('click').hide();
                });
                return false;
            }
        }

        $(function () {
            var $starWrap = $('.starWrap'); //评价星级
            $starWrap.on('click', 'img', function () {
                $(this).nextAll('img').attr("src", "/img/icon/star-blur_03.png");
                $(this).attr('src', '../img/icon/star-focus_03.png').prevAll('img').attr('src', '../img/icon/star-focus_03.png');
                console.log($(this).prevAll('img').length + 1);
                $(this).closest(".grade").find("input[type='hidden']").val($(this).prevAll('img').length + 1);
            });

            $('.commentText').on('change', 'textarea', function () {  //监听文本域输入长度
                var length = $(this).val().length;
                var $num = $(this).siblings('.wordNum').find('.num');
                $num.text(length);
                if (length >= 30) {
                    alert('评价字数请在30个以内！');
                }
            });

            var $area = $('textarea');
            var $footer = $('footer');
            $area.on('focus', function () {
                $footer.css({
                    "position": "relative",
                });
            }).on('blur', function () {
                $footer.css("position", "fixed");
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!----------主体---------->
    <section>
        <!--评论-->
        <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
            <ItemTemplate>
                <div class="commentWrap">
                    <asp:HiddenField ID="hidOrderID" runat="server" Value='<%# Eval("OrderID") %>' />
                    <asp:HiddenField ID="hidItemGUID" runat="server" Value='<%# Eval("ItemGUID") %>' />
                    <p class="title"><%# Eval("ItemName") %></p>
                    <div class="grade">
                        <span>请为他打分</span>
                        <p class="starWrap">
                            <img src="../img/icon/star-blur_03.png" />
                            <img src="../img/icon/star-blur_03.png" />
                            <img src="../img/icon/star-blur_03.png" />
                            <img src="../img/icon/star-blur_03.png" />
                            <img src="../img/icon/star-blur_03.png" />
                        </p>
                        <asp:HiddenField ID="hidStar" runat="server" Value="0" />
                    </div>
                    <div class="commentText">
                        <p class="wordNum">（<span class="num">0</span>/30）</p>
                        <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" MaxLength="30" placeholder="请写下您的评价"></asp:TextBox>
                        <p class="icon">
                            <img src="../img/icon/white_03.jpg" />
                        </p>
                    </div>
                    <div class="btnGroup">
                        <button type="reset" class="button">取消</button>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="button" CommandName="Submit" CommandArgument='<%# Eval("ID") %>' OnClientClick="return CheckComment(this);" Text="提交" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </section>
</asp:Content>
