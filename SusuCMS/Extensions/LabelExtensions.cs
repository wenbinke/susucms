using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SusuCMS.Data;

namespace SusuCMS
{
    public static class LabelExtensions
    {
        public static XElement ToXElement(this SiteLabel label)
        {
            return new XElement("SiteLabel",
                new XElement("Id", label.Id),
                new XElement("Key", label.Key),
                new XElement("Value", label.Value)
            );
        }

        public static XElement ToXElement(this WidgetLabel label)
        {
            return new XElement("WidgetLabel",
                new XElement("Id", label.Id),
                new XElement("WidgetId", label.WidgetId),
                new XElement("Key", label.Key),
                new XElement("Value", label.Value)
            );
        }

        public static XElement ToXElement(this PageLabel label)
        {
            return new XElement("PageLabel",
                new XElement("Id", label.Id),
                new XElement("PageId", label.PageId),
                new XElement("Key", label.Key),
                new XElement("Value", label.Value)
            );
        }
    }
}
