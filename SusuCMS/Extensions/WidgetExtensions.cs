using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SusuCMS.Data;

namespace SusuCMS
{
    public static class WidgetExtensions
    {
        public static XElement ToXElement(this Widget widget)
        {
            return new XElement("Widget",
                new XElement("Id", widget.Id),
                new XElement("Name", widget.Name),
                new XElement("DisplayName", widget.DisplayName),
                new XElement("DataJson", widget.DataJson),
                new XElement("IsSystem", widget.IsSystem)
            );
        }
    }
}
