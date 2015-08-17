using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SusuCMS.Data;

namespace SusuCMS
{
    public static class PageWidgetExtensions
    {
        public static XElement ToXElement(this PageWidget widget)
        {
            return new XElement("PageWidget",
                new XElement("PageId", widget.PageId),
                new XElement("WidgetId", widget.WidgetId),
                new XElement("ZoneName", widget.ZoneName),
                new XElement("DisplayOrder", widget.DisplayOrder)
            );
        }
    }
}
