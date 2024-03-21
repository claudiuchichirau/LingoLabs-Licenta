var lastScrollTop = 0;

window.scrollFunction = function () {
    var navbar = document.getElementById("navbar");
    var st = window.pageYOffset || document.documentElement.scrollTop;
    if (st > lastScrollTop) {
        navbar.style.top = "-50px";
    } else {
        navbar.style.top = "0";
    }
    lastScrollTop = st <= 0 ? 0 : st;
}

window.enableScrollFunction = function () {
    var navbar = document.getElementById("navbar");
    navbar.style.transition = "top 0.5s";
    window.onscroll = window.scrollFunction;
}