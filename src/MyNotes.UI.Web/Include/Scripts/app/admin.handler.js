$(function () {
    $.ajaxGet({ url: groupListUrl });
});

handler.admin = function () {
    $('#btnNewGroup').unbind('click').bind('click', function () {
        hideUpdatePanels();
        $.ajaxGet({ url: addGroupUrl });
    });

    $('span.icon-edit.jqGroup').unbind('click').bind('click', function () {
        $this = $(this);
        id = $(this).closest('tr').attr('id');
        $.ajaxGet({
            url: updateGroupUrl,
            callback: function (response) {
                $updTr = $('#' + id + '_upd_tr');
                $('#' + id + '_upd_td').html(response.Result);
                $('div.updPnl', $updTr).attr('data-id', id);
                $('#Id', $updTr).val(id);
                $('#Name', $updTr).val($('#' + id + '_name').html());
                showUpdatePanel($this, $updTr, id);
            }
        });
    });

    $('span.icon-remove').unbind('click').bind('click', function () {
        $this = $(this);
        $tr = $this.closest('tr');
        hideUpdatePanels();

        if ($this.hasClass('jqGroup')) {
            confirmMsg = "Are you sure you want to delete this group?";
            deleteUrl = deleteGroupUrl;
        } else if ($this.hasClass('jqUser')) {
            confirmMsg = "Are you sure you want to delete this user?";
            deleteUrl = deleteUserUrl;
        }

        jConfirm(confirmMsg, 'Confirmation Dialog', function (r) {
            if (r) {
                itemId = $tr.attr('id');
                $.ajaxDelete({
                    url: deleteUrl,
                    data: { id: itemId },
                    callback: function (response) {
                        if (response.Result.HasError) {
                            jAlert(response.Result.Message);
                        } else {
                            $tr.remove();
                            $('#' + itemId + '_upd_tr').remove();
                        }
                    }
                });
            }
        });
    });

    $('#cancelUpdateGroup').unbind('click').bind('click', function (e) {
        hideUpdatePanels();
        e.stopPropagation();
    });

    $('#btnNewUser').unbind('click').bind('click', function () {
        $.ajaxGet({ url: addUserUrl });
    });

    $('span.icon-edit.jqUser').unbind('click').bind('click', function () {
        $this = $(this);
        id = $(this).closest('tr').attr('id');
        hideUpdatePanels();
        $.ajaxGet({
            url: updateUserUrl,
            callback: function (response) {
                $updTr = $('#' + id + '_upd_tr');
                $('#' + id + '_upd_td').html(response.Result);
                $('div.updPnl', $updTr).attr('data-id', id);
                $('#Id', $updTr).val(id);
                $('#Name', $updTr).val($('#' + id + '_name').html());
                showUpdatePanel($this, $updTr, id);
            }
        });
    });

    //    $.bind('addUser', function (e, response) {
    //        if (response.HasError) {
    //            mynotes.displayErrorMessage(response.Message);
    //        } else {
    //            $(mynotes.constants.PopupView).modal('hide');
    //            $.ajaxGet({ url: userListUrl });
    //        }
    //    });
};

hideUpdatePanels = function (id) {
    $('span.icon-edit').show();
    if(id)
        $updPnl = $('div.updPnl[data-id!="' + id + '"]');
    else
        $updPnl = $('div.updPnl');

    $updPnl
        .slideUp(function () {
            $this = $(this);
            $this.closest('tr').hide();
            $this.remove();
        });
}

showUpdatePanel = function ($this, $tr, id) {
    hideUpdatePanels(id);
    $tr.show();
    $('div.updPnl[data-id="'+id+'"]')
        .slideDown(function () {
            $this.hide();
        });
}

addGroupCallback = function (response) {
    $groupListTbody = $('#groupListTbody');
    newsno = $('td.jqSN').length + 1;
    obj = { sno: newsno, id: response.Result, name: $('#Name').val(), isSysAcc: 'No' };
    $(mynotes.constants.PopupView).modal('hide');
    $groupListTbody.append($('#groupListTmpl').render(obj));
    $(mynotes.constants.PopupView).html('');
}

updateGroupCallback = function (response) {
    $('#' + response.Result + '_name').html($('#Name').val());
    hideUpdatePanels();
}

addUserCallback = function (response) {
    $userListTbody = $('#userListTbody');
    newsno = $('td.jqSN').length + 1;
    obj = { 
        sno: newsno,
        id: response.Result,
        uname: $('#Username').val(),
        name: mynotes.stringFormat('{0} {1}', [$('#Firstname').val(), $('#Lastname').val()]),
        nname: ($('#Nickname').val() == '' ? $('#Firstname').val() : $('#Nickname').val()),
        gname: $('#GroupId option:selected').text()
    };
    $(mynotes.constants.PopupView).modal('hide');
    $userListTbody.append($('#userListTmpl').render(obj));
    $(mynotes.constants.PopupView).html('');
}