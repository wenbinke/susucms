(function () {
    function _removeExtraHyphen(str) {
        if (str.indexOf('--') != -1) {
            str = str.replace(/--/g, '-');
            return removeExtraHyphen(str);
        }

        return str;
    }

    sc.String = {
        format: function (text) {
            if (text) {
                var len = arguments.length;
                for (i = 1; i < len; i++) {
                    var reg = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
                    var arg = arguments[i];
                    if (!arg) {
                        arg = "";
                    }
                    text = text.replace(reg, arg);
                }
            }

            return text;
        },
        getSlug: function (text) {
            text = text.replace(/^\s+|\s+$/g, ''); // trim
            text = text.toLowerCase();

            // remove accents, swap ñ for n, etc
            var from = "àáäâèéëêìíïîòóöôùúüûñç·/_,:;";
            var to = "aaaaeeeeiiiioooouuuunc------";
            for (var i = 0; i < from.length; i++) {
                text = text.replace(new RegExp(from.charAt(i), 'g'), to.charAt(i));
            }

            text = text.replace(/[^a-zA-Z0-9\u4e00-\u9fa5]/g, '-');

            return _removeExtraHyphen(text);
        }
    };
})();