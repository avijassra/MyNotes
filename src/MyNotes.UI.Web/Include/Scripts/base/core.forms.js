$(function () {
    $('form').live('submit', function () {
        $this = $(this);

        if ($this.hasClass('jqAjaxForm')) {
            submitJqueryForm($this);
            return false;
        }
        else {
            form.submit();
        }
    });
});

submitJqueryForm = function ($this) {
    frmData = $this.metadata({ type: 'attr', name: 'data-options' });
    if (frmData.isAjax) {
        postUrl = $this.attr('action');
        postData = $this.serialize();
        $.ajaxPost({url: postUrl, data: postData, callback: frmData.callback});
    } else {
        $this[0].submit();
    }
};