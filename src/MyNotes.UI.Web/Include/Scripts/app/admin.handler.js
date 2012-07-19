$(function () {
    $.ajaxGet({ url: groupListUrl });
});

handler.admin = function () {
    $('#btnNewGroup').unbind('click').bind('click', function () {
        hideUpdatePanels();
        $.ajaxGet({ url: addGroupUrl });
    });

    $('li.icon-edit.jqGroup').unbind('click').bind('click', function () {
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

    $('li.icon-remove.jqGroup').unbind('click').bind('click', function () {
        $tr = $(this).closest('tr');
        hideUpdatePanels();
        jConfirm('Are you sure you want to delete this group?', 'Confirmation Dialog', function (r) {
            if (r) {
                itemId = $tr.attr('id');
                $.ajaxDelete({
                    url: deleteGroupUrl,
                    data: { id: itemId },
                    callback: function () {
                        $tr.remove();
                        $('#' + itemId + '_upd_tr').remove();
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

    $('li.icon-edit.jqUser').unbind('click').bind('click', function () {
        $this = $(this);
        $tr = $(this).closest('tr');
        id = $tr.attr('id');
        hideUpdatePanels();
        $.ajaxGet({
            url: updateUserUrl,
            callback: function (response) {
                $this.addClass('disabled');
                $updTr = $('#' + id + '_upd_tr');
                $('#' + id + '_upd_td').html(response.Result).css('height', '0px');
                $('#Id').val($currentItem.attr('id'));
                $('#Name').val($currentItem.find('td:nth-child(2)').html());
                showUpdatePanel($this, $updTr)
            }
        });
    });

    $('li.icon-remove.jqUser').unbind('click').bind('click', function () {
        $tr = $(this).closest('tr');
        hideUpdatePanels();
        jConfirm('Are you sure you want to delete this group?', 'Confirmation Dialog', function (r) {
            if (r) {
                itemId = $tr.attr('id');
                $.ajaxDelete({
                    url: deleteGroupUrl,
                    data: { id: itemId },
                    callback: function () {
                        $tr.remove();
                        $('#' + itemId + '_upd_tr').remove();
                    }
                });
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
    $('li.icon-edit').show();
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

groupMapping = function () {
        
}

userMapping = function () {

}

addGroupCallback = function (response) {
    $groupListTbody = $('#groupListTbody');
    newsno = $('td.jqSN').length + 1;
    obj = { sno: newsno, id: response.Result, name: $('#Name').val(), IsSysAccount: 'No' };
    $(mynotes.constants.PopupView).modal('hide');
    $groupListTbody.append($('#groupListTmpl').render(obj));
    $(mynotes.constants.PopupView).html('');
}

updateUserCallback = function (response) {
    $currentItem = $('#' + response.Result);
    $updTd = $('#' + response.Result + '_upd_td');
    $('#' + response.Result + '_name').html($('#Name').val());
    $('#' + response.Result + '_upd_tr').slideUp('slow');
}