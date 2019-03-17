<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/Default.Master" AutoEventWireup="true" CodeBehind="PayMent.aspx.cs" Inherits="Luoyi.Web.PayMent" %>

<%@ MasterType VirtualPath="~/Controls/Default.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./css/payMent.css?<%= Guid.NewGuid() %>" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $('.wxpaynow').click(function (e) {
                var $this = $(this);
                if (!$this.hasClass("disabled")) {
                    if ($this.attr("data-today") == "true") {
                        var endhour = $this.attr("data-endhour");
                        var myDate = new Date();
                        var timenow = (myDate.getHours() * 60 + myDate.getMinutes());

                        console.log(timenow);
                        if (timenow >= endhour) {
                            $(".weui_dialog_alert .weui_dialog_bd").html("已超过截止时间，无法选择今日下单");
                            $(".weui_dialog_alert").show().on('click', '.weui_btn_dialog', function () {
                                $('.weui_dialog_alert').off('click').hide();
                                location.href = "<%= Luoyi.Common.WebHelper.GetUrlPath("/MyCart.aspx") %>";
                            });
                        } else {
                            wx.ready(function () {
                                wx.chooseWXPay({
                                    timestamp: $this.attr("data-timeStamp"), // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
                                    nonceStr: $this.attr("data-nonceStr"), // 支付签名随机串，不长于 32 位
                                    package: $this.attr("data-package"), // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）
                                    signType: $this.attr("data-signType"), // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
                                    paySign: $this.attr("data-paySign"), // 支付签名
                                    success: function (res) {
                                        // 支付成功后的回调函数
                                        if (res.errMsg == "chooseWXPay:ok") {
                                            alert("微信支付成功!");
                                            location.href = "<%= Luoyi.Common.WebHelper.GetUrlPath("/Default.aspx") %>"
                                            //location.href = "./account/";
                                        }
                                        else {
                                            alert("用户支付失败或取消支付!");
                                        }
                                    }
                                });
                            });
                        }
                    }
                    else {
                        wx.ready(function () {
                            wx.chooseWXPay({
                                timestamp: $this.attr("data-timeStamp"), // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
                                nonceStr: $this.attr("data-nonceStr"), // 支付签名随机串，不长于 32 位
                                package: $this.attr("data-package"), // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）
                                signType: $this.attr("data-signType"), // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
                                paySign: $this.attr("data-paySign"), // 支付签名
                                success: function (res) {
                                    // 支付成功后的回调函数
                                    if (res.errMsg == "chooseWXPay:ok") {
                                        alert("微信支付成功!");
                                        location.href = "<%= Luoyi.Common.WebHelper.GetUrlPath("/Default.aspx") %>"; //account
                                    }
                                    else {
                                        alert("用户支付失败或取消支付!");
                                    }
                                }
                            });
                        });
                    }
                }
            });

            $('.cashnow').click(function (e) {
                var $this = $(this);
                if (!$this.hasClass("disabled")) {
                    if ($this.attr("data-today") == "true" || "<%= DeliveryDays%>" == "1") {
                        var endhour = $this.attr("data-endhour");
                        var myDate = new Date();
                        var timenow = (myDate.getHours() * 60 + myDate.getMinutes());

                        if (timenow >= endhour) {
                            $(".weui_dialog_alert .weui_dialog_bd").html("已超过截止时间，无法下单");
                            $(".weui_dialog_alert").show().on('click', '.weui_btn_dialog', function () {
                                $('.weui_dialog_alert').off('click').hide();
                                location.href = "./MyCart.aspx";
                            });
                        }
                        //else {
                        //    location.href = $this.attr("data-url");
                        //}
                    }
                    //else {
                    //    location.href = $this.attr("data-url");
                    //}
                    location.href = $this.attr("data-url");  
                   
                }
            });

            $('.alipaynow').click(function (e) {
                var $this = $(this);
                if (!$this.hasClass("disabled")) {
                    if ($this.attr("data-today") == "true") {
                        var endhour = $this.attr("data-endhour");
                        var myDate = new Date();
                        var timenow = (myDate.getHours() * 60 + myDate.getMinutes());
                        console.log(timenow);

                        if (timenow >= endhour) {
                            $(".weui_dialog_alert .weui_dialog_bd").html("已超过截止时间，无法选择今日下单");
                            $(".weui_dialog_alert").show().on('click', '.weui_btn_dialog', function () {
                                $('.weui_dialog_alert').off('click').hide();
                                location.href = "./MyCart.aspx";
                            });
                        } else {
                            location.href = $this.attr("data-url");
                        }
                    }
                    else {
                        location.href = $this.attr("data-url");
                    }
                }
            });

            var $check = $('.agreement').find("input[type=checkbox]");
            var $btn = $('.btn-pay').find('.button');
            $check.on('click', function () {
                if ($(this).prop('checked')) {
                    $btn.removeClass('disabled').attr('href', $btn.attr("data-url"));
                } else {
                    $btn.addClass('disabled').attr('href', 'javascript:;');
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!----------主体---------->
    <section>
        <p class="orderNumber"><%= Luoyi.Web.HtmlLang.Lang("OrderN","订单号") %>
            <span><asp:Literal ID="ltlOrderCode" runat="server"></asp:Literal></span></p>
        <p class="orderPrice"><asp:Literal ID="ltlOrderAmount" runat="server"></asp:Literal></p>
        <p class="payee"><span class="fl"><%= Luoyi.Web.HtmlLang.Lang("Payer","付款人") %>：</span><span class="fr"><asp:Literal ID="ltlRecivingParty" runat="server"></asp:Literal></span></p>
        <% if (siteInfo.NeedShipToAddr)
            { %>
        <p class="payee"><span class="fl"><%= Luoyi.Web.HtmlLang.Lang("ShipToAddr", "送货地址") %>：</span><span class="fr"><asp:Literal ID="ltlShipToAddr" runat="server"></asp:Literal></span></p>
        <% } %>
        <%--<p class="product"><span class="fl"><%= Luoyi.Web.HtmlLang.Lang("Product","产品") %>：</span><span class="fr"><%= Luoyi.Web.HtmlLang.Lang("OrderN","订单号") %><asp:Literal ID="ltlOrderCode1" runat="server"></asp:Literal></span></p>--%>
        <p class="btn-pay">
            <asp:HyperLink ID="hlPayNow" runat="server" CssClass="button" NavigateUrl="javascript:;"></asp:HyperLink>
        </p>
        <% if ((!siteInfo.PaymentMethod.Equals("POD") || !string.IsNullOrWhiteSpace(siteInfo.PaymentComments)) 
                && !siteInfo.NeedShipToAddr) //现场付不显示 2017-5-9 ；有送货地址
            { %>
        <div class="erroMsg">
           <%= comments %>
        </div>
        <p class="agreement">
            <span class="checkbox">
                <input type="checkbox" checked="checked" />
                <img class="focus" src="./img/icon/checkbox-focus.png" />
                <img class="blur" src="./img/icon/checkbox.png" />
            </span>
            <span class="xy">本人已阅读</span>
        </p>
        <%} %>
    </section>
</asp:Content>
