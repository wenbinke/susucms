$(function () {
    sc.ui.Telerik = {
        openWindow: function (id, url, title, width, height) {
            if ($('#' + id).length > 0) {
                $('#' + id).remove();
            }

            var window = $.telerik.window.create({
                title: title,
                html: $.format('<iframe src="{0}" frameborder="0" style="border: 0; width: 100%;height: 96%;min-height: 100px;"></iframe>', url),
                actions: ["Refresh", "Maximize", "Close"],
                modal: true,
                width: width,
                height: height,
                minHeight: 120,
                minWidth: 200,
                visible: false
            });

            window.attr('id', id);
            window.data('tWindow').center().open();
        },
        showHtmlSource: function (e) {
            e.stopPropagation();
            e.preventDefault();

            var editor = $(e.target).closest('.t-editor').data('tEditor');
            var html = editor.value();

            var htmlSourcePopup = $('<div class="html-view"><div class="textarea t-state-default"><textarea></textarea></div>' +
                                    '<div class="t-button-wrapper"><button id="html-update" class="t-button">Update</button></div></div>')
                            .css('display', 'none')
                            .tWindow({
                                title: 'Html code',
                                modal: true,
                                resizable: false,
                                draggable: true,
                                width: 700,
                                onLoad: function () {
                                    var $popup = $(this);
                                    $popup.find('.textarea')
                                            .focus()
                                            .end()
                                            .find('#html-update')
                                            .click(function () {
                                                var value = $popup.find('textarea').val();
                                                editor.value(value);
                                                htmlSourcePopup.close();
                                            });
                                },
                                onClose: function () {
                                    editor.focus();
                                },
                                effects: $.telerik.fx.toggle.defaults()
                            }).data('tWindow');

            $(htmlSourcePopup.element).find('textarea').text(html);
            sc.ui.Form.transform('.html-view');

            htmlSourcePopup.center().open();
        }
    };
});