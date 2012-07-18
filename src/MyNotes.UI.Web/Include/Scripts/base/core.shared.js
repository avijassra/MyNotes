
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
                }, 5000);
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
        // methods
        displayErrorMessage: function (text) {
            _displayMessage(text, true);
        },
        displaySuccessMessage: function (text) {
            _displayMessage(text, false);
        },
        clearAlertMessage: function () {
            $(mynotes.constants.AlertMessage).html("").fadeOut('slow', function () { $(this).removeClass('alert alert-error alert-success'); });
        },
        uniqid: function () {
            var newdate = new date;
            return newdate.gettime();
        }
    };
} ();