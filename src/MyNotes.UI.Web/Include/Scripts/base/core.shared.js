
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
            string formating like c-sharp
            e.g. stringFormat("{0}, hello world", ['avi']);
        */
        stringFormat: function (str, arr) {
            if (ca4common.isArray(arr)) {
                for (i in arr) {
                    str = str.replace('{' + i + '}', arr[i]);
                }
            }
            return str;
        }
    };
} ();