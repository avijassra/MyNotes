$(function () {
    $('#btnClear').bind('click', function () {
        $('input.jqlc').val('');
    });

    $('#btnLogIn').bind('click', function () {
        $('input.jqlc')
            .removeClass('control-group error');

        $.ajaxGet({
            url: validateUrl,
            data: { Username: $('#Username').val(), Password: $('#Password').val() },
            callback: function (response) {
                if (response.Result) {
                    window.location = loginSuccessUrl;
                } else {
                    $('#loginSection').addClass('control-group error');
                    $('input.jqlc').val('');
                    mynotes.displayErrorMessage('Please try again. Username or Password entered does not match');
                }
            }
        });
    });
});

