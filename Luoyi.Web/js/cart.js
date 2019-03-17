$(function () {

    //加入购物车
    $("body").on("click", "[data-cartadd='true']", function () {
        var $this = $(this);
        $.ajax({
            type: "POST",
            url: './api/CartAdd.ashx',
            data: {
                ItemID: $this.attr("data-itemid"),
                Qty: 1
            },
            dataType: "json",
            success: function (json) {
                console.log(json);
                if (json.Status == 1) {
                    console.log(json.Message);
                    $(".numCart").html(json.Data.Qty);
                    $('#toast .weui_toast_content').html("加入购物车成功");
                    
                    if (typeof ($this.attr("data-cartadd")) != "undefined" && typeof ($this.attr("LimitQty")) != "undefined") {  
                        $("[data-cartadd='true']").each(function () {
                            $(this).hide();
                            $(this).prev().show();
                        })                        
                    }
                    $('#toast').show();
                    setTimeout(function () {
                        $('#toast').hide();
                    }, 2000);
                }
                else {
                    console.log(json.Message);

                    $(".weui_dialog_alert .weui_dialog_bd").html("加入购物车失败");
                    $(".weui_dialog_alert").show().on('click', '.weui_btn_dialog', function () {
                        $('.weui_dialog_alert').off('click').hide();
                    });
                }
            }
        });
    });

});