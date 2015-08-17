$(function () {
    sc.design.WidgetConfig = function () {
        this.editWidgetTitle = null;
        this.createWidgetTitle = null;
        this.deleteConfirmMessage = null;
    };

    sc.design.Widget = function (config, page) {
        var _this = this;

        var _container = $('#widget-container');

        this.initialize = function () {
            _initCreateWidget();
            _initEditWidget();
            _addWidgetToPage();
        };

        this.createWidget = function (html) {
            $('.widget-list', _container).append(html);
            sc.ui.Form.transform('.widget-list li:last');
        };

        this.changeWidgetName = function () {
            $('#widget-' + widgetId, _container).parent().prev().text(name);
        };

        function _initCreateWidget() {
            $('.create', _container).click(function () {
                sc.ui.Telerik.openWindow('CreateWidgetWindow', '/Admin/Widget/Create', config.createWidgetTitle, 650, 300);
            });
        }

        function _initEditWidget() {
            $('.edit', _container).live('click', function () {
                var checkbox = $(this).closest('li').find('input:checkbox');
                var id = checkbox.data('widget-id');

                var url = '/Admin/Widget/Edit' + id;

                // Set window width and height
                var width = parseInt(checkbox.data('window-width'));
                if (width == 0) {
                    width = 500;
                }

                var height = parseInt(checkbox.data('window-height'));
                if (height == 0) {
                    height = 220;
                }

                sc.ui.Telerik.openWindow('EditWidgetWindow', url, config.editWidgetTitle, width, height);
            });
        }

        function _addWidgetToPage() {
            $('.widget-list li input:checkbox', _container).live('change', function () {
                var widgetId = $(this).data('widget-id');

                var contents = $('#page-preview').contents();
                if ($(this).is(':checked')) {
                    page.addWidget(widgetId);
                } else {
                    page.removeWidget(widgetId);
                }
            });
        }

        function _deleteWidget() {

            var token = $('input[name=__RequestVerificationToken]', _container).val();

            $('.delete', _container).live('click', function () {

                if (confirm(config.deleteConfirmMessage)) {

                    var li = $(this).closest('li');
                    var widgetId = li.find('input:checkbox').data('widget-id');

                    var data = {
                        id: widgetId,
                        __RequestVerificationToken: token
                    };

                    $.post('/Admin/Wdiget/Delete', data, function (result) {

                        if (result.IsSuccess) {
                            sc.ui.Message.showSuccess(result.Message);
                            li.remove();

                            sc.design.Page.removeWidget(widgetId);
                        } else {
                            sc.ui.Message.showError(result.Message);
                        }
                    });
                }

            });
        }
    };
});