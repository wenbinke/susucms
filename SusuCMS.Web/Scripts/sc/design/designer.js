$(function () {
    sc.design.DesignerConfig = function () {
        this.toolbarConfig = null;
    };

    sc.design.Designer = function (config, page) {
        var _this = this;

        this.toolbar = null;
 
        this.initialize = function () {
            _this.toolbar = new sc.design.Toolbar(config.toolbarConfig, page);
            _this.toolbar.initialize();
        };
    };
});