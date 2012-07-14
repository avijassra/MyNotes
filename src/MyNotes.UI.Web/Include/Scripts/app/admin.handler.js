$(function () {
    $.ajaxGet({ url: groupListUrl });
});

handler.admin = function ($selector) {
    $selector.find('#btnNewGroup').bind('click', function () {
        $.ajaxGet({ url: addGroupUrl });
    });

    $selector.find('#btnNewUser').bind('click', function () {
        $.ajaxGet({ url: addUserUrl });
    });

    $selector.find('li.icon-edit.jqGroup').bind('click', function () {
        $tr = $(this).closest('tr');
        $.ajaxGet({
            url: updateGroupUrl,
            callback: function (response) {
                $updateTr = $tr.next('tr');
                $updateTd = $('td', $updateTr);
                $updateTd.html(response.Result);
                $('#Id').val($tr.data('id'));
                $('#Name').val($tr.find('td:nth-child(2)').html());
                $updateTr.fadeIn('slow');
            }
        });
    });

    $selector.find('li.icon-remove.jqGroup').bind('click', function () {
        $tr = $(this).closest('tr');
        jConfirm('Are you sure you want to delete this group?', 'Confirmation Dialog', function (r) {
            if (r) {
                $.ajaxDelete({
                    url: deleteGroupUrl,
                    data: { id: $tr.data('id') },
                    callback: function () { $tr.hide(); }
                });
            }
        });
    });

    $selector.bind('addUser', function (e, response) {
        if (response.HasError) {
            mynotes.displayErrorMessage(response.Message);
        } else {
            $(mynotes.constants.PopupView).modal('hide');
            $.ajaxGet({ url: userListUrl });
        }
    });
};

addGroupCallback = function (response) {
    if (response.HasError) {
        mynotes.displayErrorMessage(response.Message);
    } else {
        $groupListTbody = $('#groupListTbody');
        newsno = parseInt($('tr:last', $groupListTbody).children('td:first').html()) + 1;
        obj = { sno: newsno, id: response.Result, name: $('#Name').val(), IsSysAccount: 'No' };
        $(mynotes.constants.PopupView).modal('hide');
        $groupListTbody.append($('#groupListTmpl').render(obj));
        $(mynotes.constants.PopupView).html('');
    }
}