handler.common = function () {
    $('li.jqTab').unbind('click').bind('click', function () {
        $this = $(this);
        $ul = $this.parent('ul');
        $('li', $ul).removeClass('active');
        $this.addClass('active');
    });

    $('input[data-val="true"],select[data-val="true"]').each(function () {
        $(this).closest('div.jqInput').prev('div.jqLabel').children('label').removeClass('requiredField').addClass('requiredField');
    });

    $('a.jqValErr').popover();
};

failedFormValidationCallback = function (id) {
    $('div.field-validation-error').tooltip();
};