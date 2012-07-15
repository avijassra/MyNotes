$(function () {
    $.ajaxGet({ url: groupListUrl });
});

handler.admin = function ($selector) {
    $selector.find('#btnNewGroup').bind('click', function () {
        $('tr.jqUpdateFrm').fadeOut('slow');
        $('tr.jqUpdateFrm td').html('');
        $.ajaxGet({ url: addGroupUrl });
    });

    $selector.find('#btnNewUser').bind('click', function () {
        $.ajaxGet({ url: addUserUrl });
    });

    $selector.find('li.icon-edit.jqGroup').bind('click', function () {
        $currentItem = $(this).closest('tr');
        id = $currentItem.attr('id');
        $('tr.jqUpdateFrm').fadeOut('slow');
        $('tr.jqUpdateFrm td').html('');
        $.ajaxGet({
            url: updateGroupUrl,
            callback: function (response) {
                $updTr = $('#' + id + '_upd_tr');
                $('#' + id + '_upd_td').html(response.Result);
                $('#Id').val($currentItem.attr('id'));
                $('#Name').val($currentItem.find('td:nth-child(2)').html());
                $updTr.fadeIn('slow');
            }
        });
    });

    $selector.find('li.icon-remove.jqGroup').bind('click', function () {
        $tr = $(this).closest('tr');
        jConfirm('Are you sure you want to delete this group?', 'Confirmation Dialog', function (r) {
            if (r) {
                itemId = $tr.attr('id');
                $('tr.jqUpdateFrm').fadeOut('slow');
                $('tr.jqUpdateFrm td').html('');
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

    $selector.find('#cancelUpdateGroup').bind('click', function (e) {
        $this = $(this);
        $this.closest('tr').fadeOut('slow');
        $this.closest('td').html('');
        e.stopPropagation();
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
        newsno = parseInt($('tr:last', $groupListTbody).prev('tr').children('td:first').html()) + 1;
        obj = { sno: newsno, id: response.Result, name: $('#Name').val(), IsSysAccount: 'No' };
        $(mynotes.constants.PopupView).modal('hide');
        $groupListTbody.append($('#groupListTmpl').render(obj));
        $(mynotes.constants.PopupView).html('');
    }
}

updateGroupCallback = function (response) {
    if (response.HasError) {
        mynotes.displayErrorMessage(response.Message);
    } else {
        $currentItem = $('#' + response.Result);
        $updTd = $('#' + response.Result + '_upd_td');
        $('#' + response.Result + '_name').html($('#Name').val());
        $('#' + response.Result + '_upd_tr').slideUp('slow');
    }
}