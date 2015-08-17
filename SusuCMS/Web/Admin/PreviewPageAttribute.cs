using System.Web.Mvc;

namespace SusuCMS.Web.Admin
{
    public class PreviewPageAttribute : ActionFilterAttribute
    {
        private const string _clientScript = @"<script type=""text/javascript"">
    window.jQuery || document.write('<script type=""text/javascript"" src=""{0}""><\/script>');
</script>
<script type=""text/javascript"">
    $.ui || document.write('<script type=""text/javascript"" src=""{1}""><\/script>');
</script>
<script type=""text/javascript"">
    $(function () {{
        $('a').attr('href', 'javascript://');
        $('button, input[type=""submit""], input[type=""button""]').click(function(){{return false;}});
        $('head').append($('<link />', {{ rel: 'stylesheet', href: '{2}', type: 'text/css' }}));
        $('var.s-zone').sortable({{ revert: true, connectWith: '.s-zone', scroll: false, tolerance: 'pointer'}}).disableSelection();
        
        /* Change view type
        -----------------------------------------------------------------*/
        var type = parent.$('#change-view-type').data('current-view');
        if (type == 'compact-view') {{
            $('.s-widget .s-compact-view').show();
            $('.s-widget .s-real-view').hide();
        }} else {{
            $('.s-widget .s-compact-view').hide();
            $('.s-widget .s-real-view').show();
        }}

        /* Label editor
        -----------------------------------------------------------------*/
        var $tooltip = $('<div id=""s-label-tooltip""><a href=""javascript://""><span>{3}</span></a></div>');
        $tooltip.mouseleave(function (e) {{
            hideToolTip($(this).data('label'));
        }});

        $tooltip.click(function(){{
            var label = $(this).data('label');
            parent.editLabel(label.data('label-type'), label.data('label-id'));
            hideToolTip(label);
        }});

        $('body').append($tooltip.hide());
        
        $('var.s-label').live({{
            mouseenter: function (e) {{
                var $label = $(this);
                if ($label.data('is-in') != true){{

                    $label.data('is-in', true);
                    $tooltip.css({{
                        'top': e.pageY + 'px',
                        'left': e.pageX + 'px'
                    }}).show();

                    $tooltip.data('label', $label);
                }}
            }}, 
            mouseleave: function (e) {{
                if (!$(e.relatedTarget).is('#s-label-tooltip') 
                        && !$(e.relatedTarget).is('#s-label-tooltip *')){{ 
                    hideToolTip($(this));
                }}
            }}
        }});

        function hideToolTip($label) {{
            $label.data('is-in', false);
            $tooltip.hide(); 
        }}
    }});
</script>";

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);

            var url = new UrlHelper(filterContext.RequestContext);
            var formatted = string.Format(_clientScript, url.Content("~/Scripts/jquery.min.js"),
                url.Content("~/Scripts/jquery-ui.min.js"), url.Content("~/Areas/Admin/Content/iframe.css"),
                DisplayResource.EditLabel);
            filterContext.RequestContext.HttpContext.Response.Write(formatted);
        }
    }
}
