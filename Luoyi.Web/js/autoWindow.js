! function (a) {
    function b() {
        var b = g.getBoundingClientRect().width;
        b / c > 640 && (b = 640 * c), a.rem = b / 16, g.style.fontSize = a.rem + "px"
    }
    var c, d, e, f = a.document,
        g = f.documentElement,
        h = f.querySelector('meta[name="viewport"]'),
        i = f.querySelector('meta[name="flexible"]');
    if (h) {
        var j = h.getAttribute("content").match(/initial\-scale=(["']?)([\d\.]+)\1?/);
        j && (d = parseFloat(j[2]), c = parseInt(1 / d))
    } else if (i) {
        var j = i.getAttribute("content").match(/initial\-dpr=(["']?)([\d\.]+)\1?/);
        j && (c = parseFloat(j[2]), d = parseFloat((1 / c).toFixed(2)))
    }
    if (!c && !d) {
        var k = (a.navigator.appVersion.match(/android/gi), a.navigator.appVersion.match(/iphone/gi)),
            c = a.devicePixelRatio;
        c = k ? c >= 3 ? 3 : c >= 2 ? 2 : 1 : 1, d = 1 / c
    }
    if (g.setAttribute("data-dpr", c), !h)
        if (h = f.createElement("meta"), h.setAttribute("name", "viewport"), h.setAttribute("content", "initial-scale=" + d + ", maximum-scale=" + d + ", minimum-scale=" + d + ", user-scalable=no"), g.firstElementChild) g.firstElementChild.appendChild(h);
        else {
            var l = f.createElement("div");
            l.appendChild(h), f.write(l.innerHTML)
        }
    a.dpr = c, a.addEventListener("resize", function () {
        clearTimeout(e), e = setTimeout(b, 100)
    }, !1), a.addEventListener("pageshow", function (a) {
        a.persisted && (clearTimeout(e), e = setTimeout(b, 100))
    }, !1), "complete" === f.readyState ? f.body.style.fontSize = 12 * c + "px" : f.addEventListener("DOMContentLoaded", function () {
        f.body.style.fontSize = 12 * c + "px"
    }, !1), b()
}(window);