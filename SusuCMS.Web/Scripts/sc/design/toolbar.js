$(function () {
    sc.design.ToolbarConfig = function () {
        this.widgetConfig = null;
    };

    sc.design.Toolbar = function (config, page) {
        var _this = this;

        this.widget = null;

        var _widget;
        this.initialize = function () {
            _initTabs();
            _initButtons();

            _widget = new sc.design.Widget(config.widgetConfig, page);
            _widget.initialize();

            this.widget = _widget;
        };

        function _initTabs() {
            var links = $('.toolbar .tabs a');

            links.click(function () {
                links.removeClass('current');

                var index = $(this).index();
                var container = $('.containers .container').eq(index);
                if (container.is(':hidden')) {
                    $(this).addClass('current');
                    container.show().siblings('.container').hide();
                } else {
                    container.hide();
                }
            });

            // initialize container left position
            $('.containers .container').each(function () {
                var index = $(this).index();
                var link = links.eq(index);
                $(this).css('left', link.position().left);
            });
        }

        function _initButtons() {
            $('form').submit(function () {

                $('#PageWidgetsJson').val(JSON.stringify(page.getWidgets()));

                return true;
            });
        }
    };
});