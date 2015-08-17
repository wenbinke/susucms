(function () {
    sc.ui.Form = {
        initWatermark: function () {
            $('.watermark input, .watermark textarea').each(function () {
                if ($(this).val() != '') {
                    $(this).closest('.watermark').find('label').hide();
                }
            });

            $('.watermark input, .watermark textarea').focus(function () {
                $(this).closest('.watermark').find('label').hide();
            }).blur(function () {
                var input = $(this);
                setTimeout(function () {
                    if (input.val() == '') {
                        input.closest('.watermark').find('label').show();
                    }
                }, 100);
            });
        },
        transform: function (selector) {
            $(selector).transForm();
        }
    };
})();