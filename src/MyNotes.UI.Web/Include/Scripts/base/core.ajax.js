(function ($) {
    _ajaxCall = function (settings, axnType) {
        var options = {
            type: axnType
        };

        // $.extend() method takes two or more objects as arguments 
        // and merges the contents of them into the first object.
        $.extend(options, settings);

        //console.log('in ajaxcall -> url: ' + url + ' | actionType: ' + actionType + ' | actionData: ' + actionData + ' | actionDataType: ' + actionDataType + ' | blockOnCall: ' + blockOnCall + ' | eventName: ' + eventName + ' | callback: ' + callback);
        if (options.blockOnCall) {
            $('#main').block({
                message: '<h1>Processing</h1>',
                css: { border: '3px solid #a00', 'border-radius': '10px' }
            });
        }

        $.ajax({
            type: options.type,
            url: options.url,
            data: options.data,
            dataType: options.dataType,
            success: function (response, status, xhr) {
                //console.log(response);
                if (response) {
                    if (!response.HasError) {
                        if (response.RedirectUrl)
                            window.location = response.RedirectUrl;

                        if (response.PopupView) {
                            $(mynotes.constants.PopupView).html(response.PopupView).modal();
                            $.validationBinding($(mynotes.constants.PopupView));
                        }

                        if (response.ContentView) {
                            $(mynotes.constants.ContentView).html(response.ContentView);
                            $.validationBinding($(mynotes.constants.PopupView));
                        }

                        if (options.callback) {
                            if (options.callback && typeof (options.callback) === "function")
                                options.callback(response);
                            else
                                window[options.callback](response);
                        }

                        handler.bindFunctions();

                        if (response.Message)
                            mynotes.displaySuccessMessage(response.Message);

                        if (options.blockOnCall) {
                            $('#main').unblock();
                        }
                    } else {
                        mynotes.displayErrorMessage(response.Message);
                    }
                }
            },
            error: function (xhr, status, error) {
                if (options.blockOnCall) {
                    $('#main').unblock();
                }
                mynotes.displayErrorMessage('Error has occured. Please try again later');
            }
        });
    }

    $.ajaxPost = function (settings) {
        mynotes.clearAlertMessage();

        var options = {
            url: null,
            data: null,
            dataType: 'json',
            callback: null,
            blockOnCall: false
        };

        // $.extend() method takes two or more objects as arguments 
        // and merges the contents of them into the first object.
        $.extend(options, settings);

        return _ajaxCall(options, 'post');
    }

    $.ajaxGet = function (settings) {
        mynotes.clearAlertMessage();

        var options = {
            url: null,
            data: null,
            dataType: 'json',
            callback: null,
            blockOnCall: false
        };

        // $.extend() method takes two or more objects as arguments 
        // and merges the contents of them into the first object.
        $.extend(options, settings);

        return _ajaxCall(options, 'get');
    }

    $.ajaxDelete = function (settings) {
        mynotes.clearAlertMessage();

        var options = {
            url: null,
            data: null,
            dataType: 'json',
            callback: null,
            blockOnCall: false
        };

        // $.extend() method takes two or more objects as arguments 
        // and merges the contents of them into the first object.
        $.extend(options, settings);

        return _ajaxCall(options, 'delete');
    }

    $.validationBinding = function (elm) {
        $.validator.unobtrusive.parse(elm);
    }
})(jQuery);