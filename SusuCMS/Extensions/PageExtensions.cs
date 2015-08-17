using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SusuCMS.Data;

namespace SusuCMS
{
    public static class PageExtensions
    {
        public static XElement ToXElement(this Page page)
        {
            return new XElement("Page",
                new XElement("Id", page.Id),
                new XElement("Name", page.Name),
                new XElement("ParentId", page.ParentId),
                new XElement("HtmlTitle", page.HtmlTitle),
                new XElement("CreationTime", page.CreationTime),
                new XElement("HtmlTitle", page.HtmlTitle),
                new XElement("IsHomePage", page.IsHomePage),
                new XElement("MetaFieldsJson", page.MetaFieldsJson),
                new XElement("Template", page.Template),
                new XElement("Url", page.Url),
                new XElement("IsOnline", page.IsOnline)
            );
        }
    }
}
