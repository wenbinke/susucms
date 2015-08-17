$(function () {
    sc.design.LabelConfig = function () {
        this.editLabelTitle = null;
    };

    sc.design.Label = function (config) {
        var _this = this;

        this.editLabel = function (labelType, labelId) {
            var url = '/Admin/Label/Edit' + '?labelType=' + labelType + '&id=' + labelId;
            openWindow('EditLabelWindow', url, config.editLabelTitle, 640, 320);
        };
    };
});