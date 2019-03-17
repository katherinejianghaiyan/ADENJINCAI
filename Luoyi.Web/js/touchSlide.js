(function ($) {
    $.fn.touchSlide = function (el) {
        var startX = 0, startY = 0;

        $(this).on("touchstart", function (evt) {
            try {
                // evt.preventDefault(); //阻止触摸时浏览器的缩放、滚动条滚动等
                var touch = evt.originalEvent.changedTouches[0]; //获取第一个触点
                startX = Number(touch.pageX); //页面触点X坐标
                //startY = Number(touch.pageY); //页面触点Y坐标
            }
            catch (e) {
            }
        }).on('touchend', function (evt) {
            try {
                var touch = evt.originalEvent.changedTouches[0]; //获取第一个触点
                var x = Number(touch.pageX); //页面触点X坐标
                //var y = Number(touch.pageY); //页面触点Y坐标

                //判断滑动方向
                if (Math.abs(x - startX) < 80 ||
                    ($('.owl-page').index($('.active')) == 0 && x > startX)  //第一个
                    || ($('.owl-page').index($('.active')) == $(".owl-page").length - 1 && x < startX)) //最后一个
                    return;

                // 判断默认行为是否可以被禁用
                if (evt.cancelable) {
                    // 判断默认行为是否已经被禁用
                    if (!evt.defaultPrevented) evt.preventDefault();
                }

                if (x < startX) {
                    $(el).trigger('owl.next');
                    return
                }

                $(el).trigger('owl.prev');
            }
            catch (e) {
            }
        });

    };
})(jQuery);

(function ($) {
    $.fn.touchSlide1 = function (el) {
        var startX = 0, startY = 0;
        var changedX = 0;
        
        $(this).on("touchstart", function (evt) {
            try {
                // evt.preventDefault(); //阻止触摸时浏览器的缩放、滚动条滚动等
                var touch = evt.originalEvent.targetTouches[0]; //获取第一个触点
                var x = Number(touch.pageX); //页面触点X坐标
                var y = Number(touch.pageY); //页面触点Y坐标
                //记录触点初始位置
                startX = x;
                startY = y;
            }
            catch (e) {
            }
        }).on('touchmove', function (evt) {
            try {
                // 
                var touch = evt.originalEvent.targetTouches[0]; //获取第一个触点
                var x = Number(touch.pageX); //页面触点X坐标
                var y = Number(touch.pageY); //页面触点Y坐标
                changedX = 0;

                //判断滑动方向
                if (Math.abs(y - startY) * 2 > Math.abs(x - startX))
                    return;

                evt.preventDefault();

                changedX = x - startX;
            }
            catch (e) {
            }
            }).on('touchend', function (evt) {
                alert(evt.originalEvent.changedTouches[0])
            //evt.preventDefault();
            if (Math.abs(changedX) < 5 ||
                ($('.owl-page').index($('.active')) == 0 && changedX > 0)
                || ($('.owl-page').index($('.active')) == $(".owl-page").length - 1 && changedX < 0))
                return;

            evt.preventDefault(); //阻止触摸时浏览器的缩放、滚动条滚动等
            if (changedX < 0) {
                $(el).trigger('owl.next');
                return
            }

            $(el).trigger('owl.prev');
        });  

        $(document).on("scroll", function (evt) {
            evt.preventDefault();
        });
    };
})(jQuery);