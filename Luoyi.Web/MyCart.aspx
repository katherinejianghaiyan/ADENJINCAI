<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/Default.Master" AutoEventWireup="true" CodeBehind="MyCart.aspx.cs" Inherits="Luoyi.Web.MyCart" %>

<%@ MasterType VirtualPath="~/Controls/Default.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./css/myCart.css?<%= Guid.NewGuid() %>" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $(".mealtimes :radio").on('click', function () {
                $('#<%= hfMealTime.ClientID%>').val($(this).val());
            });
            $('#mycart').on('click', ' .numBox .mins,.plus', function () {
                var $input = $(this).siblings('input');
                if ($(this).hasClass('mins')) {
                    if ($input.val() >= 1) {
                        $input.val(Number($input.val()) - 1);
                    }
                } else {
                    $input.val(Number($input.val()) + 1);
                }

                $.ajax({
                    type: "POST",
                    url: "./api/CartQty.ashx",
                    data: {
                        CartID: $input.attr("data-cartid"),
                        Qty: $input.val()
                    },
                    dataType: "json",
                    success: function (json) {

                        if (json.Status == 1) {
                            $(".numCart").html(json.Data.Qty);
                            $(".cart-total span").html("￥" + json.Data.Total);
                            $(".couponChoose .deductionEd").html("已抵扣￥" + json.Data.CouponAmt);
                            $("#mycart").html(json.Data.MyCart);
                            $("#mypromotion").html(json.Data.MyPromotion);

                            if (json.Data.Total == 0) {

                                $('.weixin').hide();
                                $('.zfb').hide();
                            }
                            else {
                                $('.weixin').show();
                                $('.zfb').show();
                            }
                            if (json.Data.Qty == 0) {
                                $('.weixin').removeAttr("href");
                                $('.zfb').removeAttr("href");
                            } else {

                                $('.weixin').attr("href", "javascript:__doPostBack('ctl00$ContentPlaceHolder1$lbtnWechatPay','')");
                                $('.zfb').attr("href", "javascript:__doPostBack('ctl00$ContentPlaceHolder1$lbtAliPay','')");
                            }
                            if ($input.val() == "0") {
                                $input.closest(".goods-choose").remove();
                            }
                            $("#promoted").html(json.Data.PromotedItem);
                        }
                        else {
                            console.log(json.Message);
                        }
                    }
                });
            });

            $("#mycart,#mypromotion").on("click", "input[type='checkbox']", function () {

                var $checkbox = $(this);

                $.ajax({
                    type: "POST",
                    url: "./api/CartIsBuy.ashx",
                    data: {
                        CartID: $checkbox.val(),
                        IsBuy: $checkbox.prop('checked'),
                        Promotion: $checkbox.attr("data-promotion")
                    },
                    dataType: "json",
                    success: function (json) {

                        console.log(json);
                        if (json.Status == 1) {
                            $(".numCart").html(json.Data.Qty);
                            $(".cart-total span").html("￥" + json.Data.Total);
                            $(".couponChoose .deductionEd").html("已抵扣￥" + json.Data.CouponAmt);
                            $("#mycart").html(json.Data.MyCart);
                            $("#mypromotion").html(json.Data.MyPromotion);

                            if (json.Data.Total == 0) {

                                $('.weixin').hide();
                                $('.zfb').hide();
                            }
                            else {
                                $('.weixin').show();
                                $('.zfb').show();
                            }
                            if (json.Data.Qty == 0) {
                                $('.weixin').removeAttr("href");
                                $('.zfb').removeAttr("href");
                            } else {
                                $('.weixin').attr("href", "javascript:__doPostBack('ctl00$ContentPlaceHolder1$lbtnWechatPay','')");
                                $('.zfb').attr("href", "javascript:__doPostBack('ctl00$ContentPlaceHolder1$lbtAliPay','')");
                            }
                            $("#promoted").html(json.Data.PromotedItem);
                        }
                        else if (json.Status == 2) {
                            $checkbox.prop('checked', false);
                            $(".weui_dialog_alert .weui_dialog_bd").html(json.Message);
                            $(".weui_dialog_alert").show().on('click', '.weui_btn_dialog', function () {
                                $('.weui_dialog_alert').off('click').hide();
                            });
                        }
                        else {
                            console.log(json.Message);
                        }
                    }
                });
            });
        })

        //$(function () {
        //    $('.commentText').on('change', 'textarea', function () {  //监听文本域输入长度
        //        var length = $(this).val().length;
        //        var $num = $(this).siblings('.wordNum').find('.num');
        //        $num.text(length);
        //        if (length >= 30) {
        //            alert('评价字数请在30个以内！');
        //        }
        //    });
        //})
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!----------主体---------->
    <section>
        <!---购物车列表--->
        <div id="mycart">
            <asp:Literal ID="ltlMyCart" runat="server"></asp:Literal>
        </div>
        <!--优惠信息-->
        <p class="specialOffers">
            <asp:Literal ID="ltlCoupon" runat="server"></asp:Literal>
        </p>
        <!--购物车小计-->
        <% if (siteInfo.ShowPrice)
                    { %>
        <div class="cart-total">
            <span>
                <asp:Literal ID="ltlTotal" runat="server"></asp:Literal></span>
        </div>
        <% }
    if (!siteInfo.PaymentMethod.Equals("POD"))
    {  // 现场付不显示优惠券%>
        <!--优惠券-->
        <a class="couponChoose" href="/Coupon.aspx">
            <span class="word fl"><%= Luoyi.Web.HtmlLang.Lang("Coupon", "优惠券/优惠码") %></span>
            <span class="deductionEd fr"><%= Luoyi.Web.HtmlLang.Lang("Added", "已抵扣") %>￥<asp:Literal ID="ltlCouponAmount" runat="server"></asp:Literal></span>
            <span class="arrowRight">></span>
        </a>
        <%} %>
        <!--取货日期--->
        <p class="getTime"><%= Luoyi.Web.HtmlLang.Lang("PLEASECHOOSETHEDAYFORPICKUP", "请选择取货日期") %></p>
        <p class="timeChoose">
            <asp:PlaceHolder ID="phToday" runat="server">
                <span class="checkbox fl">
                    <asp:RadioButton ID="rbtnToday" runat="server" GroupName="time" />
                    <img class="focus" src="./img/icon/checkbox-focus.png" />
                    <img class="blur" src="./img/icon/checkbox.png" />
                    <span class="word"><%= Luoyi.Web.HtmlLang.Lang("TODAY", "今天") %></span>
                </span>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phTomorrow" runat="server">
                <span class="checkbox fr">
                    <asp:RadioButton ID="rbtnTomorrow" runat="server" GroupName="time" />
                    <img class="focus" src="./img/icon/checkbox-focus.png" />
                    <img class="blur" src="./img/icon/checkbox.png" />
                    <asp:Label ID="lblTomorrow" runat="server" CssClass="word"></asp:Label>
                    <%--<span class="word"><%= Luoyi.Web.HtmlLang.Lang("TOMORROW","明天") %></span>--%>
                </span>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phDinnerType" runat="server">
                <%--<span class="dinnertype">
                    <asp:DropDownList ID="ddlDinnerType" runat="server" Font-Size="Larger">
                    </asp:DropDownList>
                </span>--%>
                
                    <% ShowMealTimes(); %>
                <asp:HiddenField ID="hfMealTime" Value="" runat="server" />
            </asp:PlaceHolder>
        </p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">   
        </asp:ScriptManager> 
        <asp:UpdatePanel runat="server" ID="UpdatePancel1" UpdateMode="Always">
            <ContentTemplate>
                <!--请选择地址--->
                <% if (siteInfo.NeedShipToAddr && !string.IsNullOrWhiteSpace(_UserInfo.ShipToAddr))
                    { %>
                <p class="getTime"><%=Luoyi.Web.HtmlLang.Lang("ShipToAddr", "送货地址", "/PayMent.aspx") %></p>
                <p class="timeChoose" style="margin-bottom: .2rem; padding-bottom: .1rem">
                    <span class="addr"><%= _UserInfo.ShipToAddr %></span>
                </p>
                <%}
    else if (siteInfo.NeedShipToAddr && string.IsNullOrWhiteSpace(_UserInfo.ShipToAddr))
    {
        if (lstSiteAddrs != null && lstSiteAddrs.Any())
        { %>
                <p class="getTime"><%= Luoyi.Web.HtmlLang.Lang("PLEASECHOOSETHEADDRESS", "请选择地址") %></p>
                <p class="timeChoose" style="margin-bottom: .2rem; padding-bottom: .1rem">
                    <asp:PlaceHolder ID="PlaceHolderAddr1" runat="server">
                        <span class="Addr1">
                            <asp:DropDownList ID="dropDownAddr1" runat="server" Font-Size="Larger" Style="width: 13rem"
                                OnSelectedIndexChanged="dropDownAddr1_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </span>
                    </asp:PlaceHolder>
                </p>
                <%} else { %>
                <div class="commentText">
                    <p class="wordNum"><%= Luoyi.Web.HtmlLang.Lang("WriteDownCommens", "请写下您的要求") %></p>
                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" MaxLength="30"></asp:TextBox>
                </div>
                <%} if (this.dropDownAddr2.Visible)
                    {%>
                <p class="timeChoose" style="padding-bottom: .1rem">
                    <asp:PlaceHolder ID="PlaceHolderAddr2" runat="server">
                        <span class="Addr2">
                            <asp:DropDownList ID="dropDownAddr2" runat="server" Font-Size="Larger" Style="width: 8rem"
                                OnSelectedIndexChanged="dropDownAddr2_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </span>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="PlaceHolderAddr3" runat="server">
                        <span class="Addr3">
                            <asp:DropDownList ID="dropDownAddr3" runat="server" Font-Size="Larger" Style="width: 4.8rem" AutoPostBack="False">
                            </asp:DropDownList>
                        </span>
                    </asp:PlaceHolder>
                </p>
                <% } %>
                <% } %>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="mypromotion">
            <asp:Literal ID="ltlPromotionItem" runat="server"></asp:Literal>
        </div>

        
        <!-----支付---->
        <asp:HyperLink ID="hlShopping" runat="server" CssClass="continueShop" NavigateUrl="./Default.aspx"><%= Luoyi.Web.HtmlLang.Lang("CONTINUESHOPPING", "继续购物") %></asp:HyperLink>
        <asp:LinkButton ID="lbtnWechatPay" runat="server" CssClass="weixin" OnClick="lbtnWechatPay_Click">
                    <%= siteInfo.PaymentMethod.Equals("POD") ? Luoyi.Web.HtmlLang.Lang("GenerateOrder", "生成订单") : Luoyi.Web.HtmlLang.Lang("WECHATPAYMEN", "微信支付") %>

        </asp:LinkButton>
        <%--<asp:LinkButton ID="lbtAliPay" runat="server" CssClass="zfb" OnClick="lbtAliPay_Click"><%= Luoyi.Web.HtmlLang.Lang("ALIPAYPAYMENT","支付宝") %></asp:LinkButton>--%>
    </section>
</asp:Content>

