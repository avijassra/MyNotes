
var mynotes = function () {
    var _displayMessage = function (text, isError) {
        div = '<div class="alert alert-' + (isError ? 'error' : 'success') + '">' + text + '</div>';

        popupIsVisible = $(mynotes.constants.PopupView + ':visible').length > 0;

        $altMsgDiv = $(mynotes.constants.PopupAlertMessage);
        if (!popupIsVisible || $altMsgDiv.length == 0)
            $altMsgDiv = $(mynotes.constants.AlertMessage);

        $altMsgDiv.html(div).fadeIn('slow', function () {
            if (!isError) {
                setTimeout(function () {
                    $altMsgDiv.fadeOut('slow')
                }, 3000);
            }
        });
    };

    return {
        customEvent: {
            LoginFormSubmitted: 'LoginFormSubmitted'
        },
        constants: {
            AlertMessage: '#altMsg',
            PopupAlertMessage: '#popupAltMsg',
            PopupView: '#popupContainer',
            ContentView: '#mainContainer'
        },
        /*
            display error alert message
        */
        displayErrorMessage: function (text) {
            _displayMessage(text, true);
        },
        /*
            display success alert message
        */
        displaySuccessMessage: function (text) {
            _displayMessage(text, false);
        },
         /*
            clear alert message
        */
        clearAlertMessage: function () {
            $(mynotes.constants.AlertMessage).html("").fadeOut('slow', function () { $(this).removeClass('alert alert-error alert-success'); });
        },
        /*
            generates unique id
        */
        uniqid: function () {
            var newdate = new Date();
            return newdate.getTime();
        },
        /*
        check if obj is of array type
        */
        isArray: function (obj) {
            // obj is not null and not undefined
            if (!obj || obj == null || obj == 'undefined')
                return false;

            //returns true is it is an array
            if (obj.constructor.toString().indexOf('Array') == -1) {
                return false;
            } else {
                // if is array then length is greater then 0
                return (obj.length > 0 ? true : false);
            }
        },
        /*
            string formating like c-sharp
            e.g. stringFormat("{0}, hello world", ['avi']);
        */
        stringFormat: function (str, arr) {
            if (mynotes.isArray(arr)) {
                for (i in arr) {
                    str = str.replace('{' + i + '}', arr[i]);
                }
            }
            return str;
        }
    };
} ();