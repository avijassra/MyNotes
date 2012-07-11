
var mynotes = {
    CustomEvent: {
        LoginFormSubmitted: 'LoginFormSubmitted'
    },
    Constants: {
        AlertMessage: '#alertMessage',
        PopupView: '#popupContainer',
        ContentView: '#mainContainer',
        PopupMessage: '#popupMsg'
    },
    // methods
    DisplayAlertMessage: function (text) {
        $(mynotes.Constants.PopupMessage).html(text).addClass('alert alert-error').fadeIn('slow');
    },
    ClearAlertMessage: function () {
        $(mynotes.Constants.PopupMessage).html("").removeClass('alert alert-error').fadeOut('slow');
    },
    uniqid: function () {
        var newdate = new date;
        return newdate.gettime();
    }
};