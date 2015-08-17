$(function () {
    $('a.helptip').poshytip({
        className: 'tip-yellowsimple',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 8,
        liveEvents: true
    });

    sc.ui.Form.transform('body');
    sc.ui.Form.initWatermark();
    $('.t-html').click(sc.ui.Telerik.showHtmlSource);
});

/* Calculate height 
------------------------------------------------------*/
function calcAdminHeight() {
    var headerHeight = $('#header').height();
    var subheaderHeight = $('#subheader').height() + 1;

    $('#main').height($(window).height() - headerHeight);
    $('#main-content').height($('#main').height() - subheaderHeight);
}