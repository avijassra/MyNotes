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
        itemId = $(this).closest('tr').attr('id');
        $.ajaxGet({
            url: updateGroupUrl,
            data: { id: itemId },
            callback: function (response) {
                $updTr = $('#upd_tr_' + itemId);
                $('#upd_td_' + itemId).html(response.Result);
                $('div.updPnl', $updTr).attr('data-id', itemId);
                showUpdatePanel($this, $updTr, itemId);
            }
        });
    });

    $('#btnNewUser').unbind('click').bind('click', function () {
        $.ajaxGet({ url: addUserUrl });
    });

    $('span.icon-edit.jqUser').unbind('click').bind('click', function () {
        $this = $(this);
        itemId = $(this).closest('tr').attr('id');
        hideUpdatePanels();
        $.ajaxGet({
            url: updateUserUrl,
            data: { id: itemId },
            callback: function (response) {
                $updTr = $('#upd_tr_' + itemId);
                $('#upd_td_' + itemId).html(response.Result);
                $('div.updPnl', $updTr).attr('data-id', itemId);
                showUpdatePanel($this, $updTr, itemId);
            }
        });
    });

    $('a.jqCancelUpdate').unbind('click').bind('click', function (e) {
        hideUpdatePanels();
        e.stopPropagation();
    });

    $('span.icon-trash').unbind('click').bind('click', function () {
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
                            $('#upd_tr_' + itemId).remove();
                        }
                    }
                });
            }
        });
    });

    $('span.icon-repeat').unbind('click').bind('click', function () {
        $this = $(this);
        hideUpdatePanels();

        jConfirm('Are you sure you want to reset the password?', 'Confirmation Dialog', function (r) {
            if (r) {
                itemId = $this.closest('tr').attr('id');
                $.ajaxPut({
                    url: resetPwdUrl,
                    data: { id: itemId }
                });
            }
        });
    });

    $('span.icon-lock').unbind('click').bind('click', function () {
        $this = $(this);
        $tr = $this.closest('tr');
        hideUpdatePanels();
        canUserBeLocked = !$tr.hasClass('locked'); // check if locked class is present, means user is currently locked and can be only unlocked

        jConfirm(mynotes.stringFormat('Are you sure you want to {0} this user?', [canUserBeLocked ? 'lock' : 'unlock']), 'Confirmation Dialog', function (r) {
            if (r) {
                itemId = $tr.attr('id');
                $.ajaxPut({
                    url: userLockStatusUrl,
                    data: { id: itemId, isLocked: canUserBeLocked },
                    callback: function (response) {
                        if (canUserBeLocked) {
                            $tr.addClass('locked');
                        } else {
                            $tr.removeClass('locked');
                        }
                    }
                });
            }
        });
    });
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
    $('#name_' + response.Result).html($('#Name').val());
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

updateUserCallback = function (response) {
    $('#uname_' + response.Result).html($('#Username').val());
    $('#name_' + response.Result).html(mynotes.stringFormat('{0} {1}', [$('#Firstname').val(), $('#Lastname').val()]));
    $('#nname_' + response.Result).html($('#Nickname').val() == '' ? $('#Firstname').val() : $('#Nickname').val());
    $('#gname_' + response.Result).html($('#GroupId option:selected').text());
    hideUpdatePanels();
}