$(function () {
    sc.design.Theme = function (page) {
        var _this = this;

        this.initialize = function () {
            $('.theme-list a').live('click', function () {
                var current = $(this);
                var themeName = current.data('theme-name');
                var oldThemeName = $('#ThemeName').val();
                if (themeName != oldThemeName) {
                    $('.theme-list li').removeClass('current');
                    current.parent().addClass('current');
                    page.changeTheme(themeName);
                    $('#ThemeName').val(themeName);
                }
            });
        };
    };
});