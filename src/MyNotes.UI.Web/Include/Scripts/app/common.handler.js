handler.common = function () {
    $('li.jqTab').unbind('click').bind('click', function () {
        $this = $(this);
        $ul = $this.parent('ul');
        $('li', $ul).removeClass('active');
        $this.addClass('active');
    });
};