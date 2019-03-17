<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alipay.aspx.cs" Inherits="Luoyi.Web.Plugin.Payment.AliPay.Alipay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1,user-scalable=no" />
    <title>支付宝支付</title>
    <link href="/css/weui.min.css" rel="stylesheet" />
    <style type="text/css">
        .scroll-wrapper
        {
            position: fixed;
            right: 0;
            bottom: 0;
            left: 0;
            top: 0;
            -webkit-overflow-scrolling: touch;
            overflow-y: scroll;
        }


            .scroll-wrapper iframe
            {
                height: 100%;
                width: 100%;
            }
    </style>
    <script type="text/javascript">
        function loaded(frm) {
            document.getElementById("loadingToast").style.display = "none";
        }
    </script>
</head>
<body>
    <div class="scroll-wrapper">
        <asp:Literal ID="ltlAliPay" runat="server"></asp:Literal>
    </div>
    <!-- loading toast -->
    <div id="loadingToast" class="weui_loading_toast">
        <div class="weui_mask_transparent"></div>
        <div class="weui_toast">
            <div class="weui_loading">
                <div class="weui_loading_leaf weui_loading_leaf_0"></div>
                <div class="weui_loading_leaf weui_loading_leaf_1"></div>
                <div class="weui_loading_leaf weui_loading_leaf_2"></div>
                <div class="weui_loading_leaf weui_loading_leaf_3"></div>
                <div class="weui_loading_leaf weui_loading_leaf_4"></div>
                <div class="weui_loading_leaf weui_loading_leaf_5"></div>
                <div class="weui_loading_leaf weui_loading_leaf_6"></div>
                <div class="weui_loading_leaf weui_loading_leaf_7"></div>
                <div class="weui_loading_leaf weui_loading_leaf_8"></div>
                <div class="weui_loading_leaf weui_loading_leaf_9"></div>
                <div class="weui_loading_leaf weui_loading_leaf_10"></div>
                <div class="weui_loading_leaf weui_loading_leaf_11"></div>
            </div>
            <p class="weui_toast_content">连接支付宝</p>
        </div>
    </div>
</body>
</html>
