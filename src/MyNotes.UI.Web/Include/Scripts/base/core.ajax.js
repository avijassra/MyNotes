(function ($) {
    _ajaxCall = function (settings, axnType) {
        // clear the existing alert message
        mynotes.clearAlertMessage();

        // default options
        var options = {
            url: null,
            data: null,
            type: axnType,
            dataType: 'json',
            callback: null,
            blockOnCall: false
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
                        if (response.Result && response.Result.HasError && response.Result.HasError == true) {
                            mynotes.displayErrorMessage(response.Result.Message);
                        } else {
                            if (response.RedirectUrl)
                                window.location = response.RedirectUrl;

                            if (response.PopupView) {
                                $(mynotes.constants.PopupView).html(response.PopupView).modal();
                                $.validationBinding($(mynotes.constants.PopupView));
                            }

                            if (response.ContentView) {
                                if (!response.SlidingScreenId) {
                                    $(mynotes.constants.ContentView).html(response.ContentView);
                                    $.validationBinding($(mynotes.constants.ContentView));
                                } else {
                                    console.log(response.SlidingScreenId);
                                    $slidingScreen = $('#' + response.SlidingScreenId);
                                    $slidingScreen.html(response.ContentView).siblings().removeClass('centerScreen');
                                    $slidingScreen.addClass('centerScreen');
                                }
                            }

                            if (options.callback) {
                                if (options.callback && typeof (options.callback) === "function")
                                    options.callback(response);
                                else
                                    window.mynotesCallback[options.callback](response);
                            }

                            handler.bindFunctions();

                            if (response.Message)
                                mynotes.displaySuccessMessage(response.Message);

                            if (options.blockOnCall) {
                                $('#main').unblock();
                            }
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
        return _ajaxCall(settings, 'post');
    }

    $.ajaxPut = function (settings) {
        return _ajaxCall(settings, 'put');
    }

    $.ajaxGet = function (settings) {
        return _ajaxCall(settings, 'get');
    }

    $.ajaxDelete = function (settings) {
        return _ajaxCall(settings, 'delete');
    }

    $.validationBinding = function (elm) {
        $.validator.unobtrusive.parse(elm);
    }
})(jQuery);