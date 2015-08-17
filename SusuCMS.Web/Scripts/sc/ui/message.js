$(function () {
    function _showMessage(message, className) {
        var span = $.format('<span class="{0}">{1}</span>', className, message);
        var container = $('#message');
        if (container.length == 0) {
            container = $($.format('<div id="message">{0}</div>', span));
            container.appendTo('body');
        } else {
            container.empty().append(span);
        }

        container.fadeIn();

        setTimeout(function () {
            container.fadeOut();
        }, 5000);
    }

    sc.ui.Message = {
        showSuccess: function (message) {
            _showMessage(message, 'success');
        },
        showError: function (message) {
            _showMessage(message, 'error');
        },
        showLoading: function (message) {
            _showMessage(message, 'loading');
        }
    };
});