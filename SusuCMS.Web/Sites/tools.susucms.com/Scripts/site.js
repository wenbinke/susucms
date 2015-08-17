$.format = function (format) {
    if (format) {
        var len = arguments.length;
        for (i = 1; i < len; i++) {
            var reg = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
            var arg = arguments[i];
            if (!arg) {
                arg = "";
            }
            format = format.replace(reg, arg);
        }
    }

    return format;
}

$(function () {
    $('.watermark input').each(function () {
        if ($(this).val() != '') {
            $(this).siblings('label').hide();
        }
    });
    $('.watermark input').focus(function () {
        $(this).siblings('label').hide();
    }).blur(function () {
        var input = $(this);
        setTimeout(function () {
            if (input.val() == '') {
                input.siblings('label').show();
            }
        }, 300);
    });
});