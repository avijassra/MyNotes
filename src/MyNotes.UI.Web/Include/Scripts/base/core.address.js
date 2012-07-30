$(function () {
    $(window).trigger('hashchange');

    //Grab hash off URL (default to first tab) and update
    $(window).bind("hashchange", function (e) {
        // check if its ajax call or not
        // for ajax call url should have '#!'
        if (location.hash.indexOf('#!') >= 0) {
            newUrl = location.pathname + location.hash.replace('#!', '/');
            $.ajaxGet({ url: newUrl });
        }
        else {
            window.location = location.pathname;
        }
    });

    var url = document.URL;

    if (url.indexOf('#!') > 0) {
        $(window).trigger('hashchange');
    }
});