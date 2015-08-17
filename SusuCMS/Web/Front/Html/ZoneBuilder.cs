using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SusuCMS.Data;
using System.Web.Mvc;

namespace SusuCMS.Web.Front.Html
{
    public class ZoneBuilder
    {
        private FrontContext _frontContext;
        private HtmlHelper _htmlHelper;
        private IDictionary<string, Dictionary<Widget, IHtmlString>> _zoneMap;
        private WidgetBuilder _widgetBuilder;

        public ZoneBuilder(FrontContext frontContext, HtmlHelper htmlHelper)
        {
            _frontContext = frontContext;
            _htmlHelper = htmlHelper;
            _widgetBuilder = new WidgetBuilder(_htmlHelper);

            var viewData = _frontContext.ViewData;
            if (viewData.ZoneMap == null)
            {
                viewData.ZoneMap = GetZoneMap();
            }

            _zoneMap = viewData.ZoneMap;
        }

        private IDictionary<string, Dictionary<Widget, IHtmlString>> GetZoneMap()
        {
            var map = new Dictionary<string, Dictionary<Widget, IHtmlString>>();
            var zoneGroups = _frontContext.Page.Widgets.GroupBy(i => i.ZoneName);
            foreach (var item in zoneGroups)
            {
                var htmls = new Dictionary<Widget, IHtmlString>();
                foreach (var pageWidget in item)
                {
                    htmls.Add(pageWidget.Widget, GetWidgetHtml(pageWidget.Widget));
                }
                map.Add(item.Key, htmls);
            }

            return map;
        }

        private IHtmlString GetWidgetHtml(Widget widget)
        {
            if (_frontContext.IsPreviewMode)
            {
                return _widgetBuilder.GetPreviewHtml(widget, _frontContext.WidgetViewType);
            }
            else
            {
                return _widgetBuilder.GetHtml(widget);
            }
        }

        public IHtmlString GetHtml(string name)
        {
            var builder = new StringBuilder();
            var widgetHtmls = _zoneMap[name];
            foreach (var item in widgetHtmls)
            {
                builder.Append(item.Value);
            }

            if (_frontContext.IsPreviewMode)
            {
                return GetPreviewHtml(name, builder.ToString());
            }
            else
            {
                return MvcHtmlString.Create(builder.ToString());
            }
        }

        private IHtmlString GetPreviewHtml(string name, string html)
        {
            return MvcHtmlString.Create(string.Format(@"<var data-zone-name=""{0}"" class=""s-zone"">{1}</var>", name, html));
        }
    }
}
