$(function () {
    sc.design.Page = function () {
        var _this = this;
        var _preview = $('#page-preview');
        var _contents = _preview.contents();

        this.initialize = function () {

            $(window).resize(_adjustHeight).load(_adjustHeight);

        };

        this.viewType = 'RealView';     // 'RealView' Or 'CompactView'

        this.findWidget = function (widgetId) {
            return _contents.find('.s-widget[data-widget-id="' + widgetId + '"]');
        };

        this.removeWidget = function (widgetId) {
            _this.findWidget(widgetId).remove();
        };

        this.addWidget = function (widgetId) {
            $.get('/Admin/Widget/Preview/' + widgetId + '?viewType=' + _this.viewType, function (html) {
                var firstZone = contents.find('.s-zone[data-zone-name="content"]').length > 0
                        ? contents.find('.s-zone[data-zone-name="content"]')
                        : contents.find('.s-zone:eq(0)');

                var $result = $(html);
                $result.find('a').attr('href', 'javascript://');

                firstZone.prepend($result);
            });
        };

        this.getWidgets = function () {
            var list = new Array();
            _contents.find('.s-zone').each(function () {
                var zoneName = $(this).data('zone-name');

                var i = 0;
                $(this).find('.s-widget').each(function () {
                    list.push({
                        ZoneName: zoneName,
                        DisplayOrder: i++,
                        WidgetId: $(this).data('widget-id')
                    });
                });
            });
        };

        this.changeTheme = function (themeName) {
            $.get('/Admin/Theme/RenderTheme', { themeName: themeName }, function (result) {
                var styleSheet = _contents.find('link[href^="/asset.axd?id="]');
                styleSheet.replaceWith(result);
            });
        };

        this.changeViewType = function () {

        };

        this.reload = function () {
            _preview.attr('src', _preview.attr('src'));
        };

        this.refreshLabel = function (labelType, labelId, value) {
            _contents.find('.s-label[data-label-id="' + labelId + '"][data-label-type="' + labelType + '"]').html(value);
        };

        this.refreshWidget = function (widgetId) {

            $.get('/Admin/Widget/Preview/' + widgetId + '?viewType=' + _this.viewType, function (html) {

                var $result = $(html);
                $result.find('a').attr('href', 'javascript://');

                _this.findWidget(widgetId).replaceWith($result);
            });
        };

        function _adjustHeight() {
            _preview.height($(window).height() - $('.toolbar').height());
        }
    }
});