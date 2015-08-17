using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using SusuCMS.Data;
using SusuCMS.Data.Enums;
using SusuCMS.Services;

namespace SusuCMS.Web.Front.Html
{
    public class WidgetBuilder
    {
        private HtmlHelper _htmlHelper;

        public WidgetBuilder(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public IHtmlString GetHtml(Widget widget)
        {
            var viewData = new ViewDataDictionary();
            viewData.Add("CurrentWidget", widget);
            var service = new WidgetService(widget.Site);

            return _htmlHelper.Partial(service.GetWidgetPath(widget), viewData);
        }

        public IHtmlString GetPreviewHtml(Widget widget, WidgetViewType viewType)
        {
            string result;
            if (viewType == WidgetViewType.CompactView)
            {
                result = string.Format(@"<var data-widget-id=""{0}"" class=""s-widget s-compact-view"">{1}</var>", widget.Id, widget.DisplayName);
            }
            else
            {
                result = string.Format(@"<var data-widget-id=""{0}"" class=""s-widget"">{1}</var>", widget.Id, GetHtml(widget).ToHtmlString());
            }

            return MvcHtmlString.Create(result);
        }
    }
}
