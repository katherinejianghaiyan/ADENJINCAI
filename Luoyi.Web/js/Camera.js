(function ($) {
    var photoNum = 0;
    var headGuid = "";
    $.fn.Camera = function (guid, costCenterCode, costCenterGuid, userId, callback) {
        headGuid = guid;

        $(this).change(function (e) {
            var datafile = new FormData();//$('<form></form>').append($(e.target).clone())[0]);
            datafile.append('action', 'UploadImage');
            datafile.append('file', e.target.files[0]);
            datafile.append('costCenterCode', costCenterCode);
            datafile.append('headGuid', headGuid);
            datafile.append('siteGuid', costCenterGuid);
            datafile.append("username", userId);
            datafile.append("details", $(e.target).parents('div.divSurveyItems').attr("id"));

            $.ajax({
                type: "POST",
                url: "../api/Survey.ashx",
                data: datafile,
                processData: false,
                contentType: false,
                dataType: "json",
                success: function (json) {
                    if (json.Status === 1) {
                        //setTimeout(function () {
                        //$(e.target).parent('span').next('div').find('li.weui_uploader_status').removeClass('weui_uploader_status').find('.weui_uploader_status_content').remove();
                        $(e.target).parents('div.divSurveyItems').find('li.weui_uploader_status').removeClass('weui_uploader_status').find('.weui_uploader_status_content').remove();
                        //}, 3000);
                        callback(json.Data);
                        headGuid = json.Data;
                    }
                    else {
                        uploadStatus = "fail";
                        $(".weui_dialog_alert .weui_dialog_bd").html("数据提交失败");
                        $(".weui_dialog_alert").show().on('click', '.weui_btn_dialog', function () {
                            $('.weui_dialog_alert').off('click').hide();
                        });
                    }                            
                }
            });

            if (++photoNum === 4) $(e.target).hide();
            UploadFile(e.target);                  
        });

    }

    $.fn.setPhotoNum = function () {
        $(this).show();
        photoNum = 0;
        headGuid = "";
    };
})(jQuery);

function UploadFile(obj) {
    var reader = new FileReader();
    reader.readAsDataURL(obj.files[0]);

    reader.onload = function (e) {        
        ShowImage($(obj), e.target.result);
    };              
}

function ShowImage(obj,image) {
    var img = new Image();
    img.src = image;
    img.onload = function () {
        var maxSize = 300;
        maxSize = Math.min(maxSize, Math.max(this.width, this.height));
        // 不要超出最大宽度
        var w = this.width * maxSize / Math.max(this.width, this.height);
        // 高度按比例计算
        var h = this.height * maxSize / Math.max(this.width, this.height);

        //起点坐标
        var y0 = 0;  
        var canvas = document.createElement('canvas');
        var ctx = canvas.getContext('2d');

        // 设置 canvas 的宽度和高度
        canvas.width = maxSize;
        canvas.height = maxSize;

        if (isMobile()) {
            y0 = -w;
            ctx.rotate(Math.PI / 2);
        }

        ctx.drawImage(this, 0, y0, w, h);

        var base64 = canvas.toDataURL('image/png');
        // 插入到预览区
        var sli = '<li class="weui_uploader_file weui_uploader_status" style="width: 22%;background-image:url('
            + base64 + ')"><div class="weui_uploader_status_content">0%</div></li>';

        var $preview = $(sli);
        //$('.weui_uploader_files').append($preview);
        //obj.parent('span').next().children('ul.weui_uploader_files').append($preview);
        obj.parents('div.divSurveyItems').find('ul.weui_uploader_files').append($preview);
        
        Uploading($preview);     
    };
}

//进度条
function Uploading(obj) {   
    var progress = 0;
    process();

    function process() {
        if (typeof (obj.find('weui_uploader_status')) == "undefined") {
            alert(3);
            return;
        }

        if (progress < 95)
            progress = progress + 1;
        obj.find('.weui_uploader_status_content').text(progress + '%');

        setTimeout(process,500)
    };
}

function isMobile() {
    var userAgentInfo = navigator.userAgent;

    var mobileAgents = ["Android", "iPhone", "SymbianOS", "Windows Phone", "iPad", "iPod"];

    var mobile_flag = false;

    //根据userAgent判断是否是手机
    for (var v = 0; v < mobileAgents.length; v++) {
        if (userAgentInfo.indexOf(mobileAgents[v]) > 0) {
            mobile_flag = true;
            break;
        }
    }

    var screen_width = window.screen.width;
    var screen_height = window.screen.height;

    //根据屏幕分辨率判断是否是手机
    if (screen_width < 500 && screen_height < 800) {
        mobile_flag = true;
    }

    return mobile_flag;
}