$(function () {
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
        // get the action name 
        axn = url.slice(url.indexOf('#!') + 2);
        if (axn.indexOf('/') > 0) { axn = axn.slice(0, axn.indexOf('/')); } // this is remove any extra values in the url like querystring
        if (axn.indexOf('?') > 0) { axn = axn.slice(0, axn.indexOf('?')); } // this is remove any extra values in the url like querystring
        // if element is Tab then it should be wrapped in li tag having jqTab class and
        // element itself should have class of same name as axn with prepened 'jq'
        $elem = $('.jq' + axn).parent('li.jqTab');
        if ($elem.length > 0) $elem.addClass('active');
        $(window).trigger('hashchange');
    }
});