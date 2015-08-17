using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SusuCMS.Data;

namespace SusuCMS
{
    public static class XElementExtensions
    {
        public static Site ToSite(this XElement xml)
        {
            return new Site
            {
                Id = Convert.ToInt32(xml.Element("Id").Value),
                Name = xml.Element("Name").Value,
                Theme = xml.Element("Theme").Value,
                CreationTime = Convert.ToDateTime(xml.Element("CreationTime").Value),
                IsOnline = Convert.ToBoolean(xml.Element("IsOnline").Value),
                CultureName = xml.Element("CultureName").Value,
                AnalyticsCode = xml.Element("AnalyticsCode").Value,
                Template = xml.Element("Template").Value,
                EnableCustomCss = Convert.ToBoolean(xml.Element("EnableCustomCss").Value),
                UrlsJson = xml.Element("UrlsJson").Value
            };
        }

        public static Menu ToMenu(this XElement xml)
        {
            return new Menu
            {
                Id = Convert.ToInt32(xml.Element("Id").Value),
                Name = xml.Element("Name").Value,
                ParentId = GetNullableId(xml.Element("ParentId").Value),
                Url = xml.Element("Url").Value,
                IsOnline = Convert.ToBoolean(xml.Element("IsOnline").Value),
                DisplayOrder = Convert.ToInt32(xml.Element("DisplayOrder").Value),
                OpenInNewWindow = Convert.ToBoolean(xml.Element("OpenInNewWindow").Value)
            };
        }

        private static int? GetNullableId(string value)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
               return Convert.ToInt32(value);
            }

            return null;
        }

        public static Widget ToWidget(this XElement xml)
        {
            return new Widget
            {
                Id = Convert.ToInt32(xml.Element("Id").Value),
                Name = xml.Element("Name").Value,
                DisplayName = xml.Element("DisplayName").Value,
                DataJson = xml.Element("DataJson").Value,
                IsSystem = Convert.ToBoolean(xml.Element("IsSystem").Value)
            };
        }

        public static SiteLabel ToSiteLabel(this XElement xml)
        {
            return new SiteLabel
            {
                Id = Convert.ToInt32(xml.Element("Id").Value),
                Key = xml.Element("Key").Value,
                Value = xml.Element("Value").Value
            };
        }

        public static PageLabel ToPageLabel(this XElement xml)
        {
            return new PageLabel
            {
                Id = Convert.ToInt32(xml.Element("Id").Value),
                PageId = Convert.ToInt32(xml.Element("PageId").Value),
                Key = xml.Element("Key").Value,
                Value = xml.Element("Value").Value
            };
        }

        public static WidgetLabel ToWidgetLabel(this XElement xml)
        {
            return new WidgetLabel
            {
                Id = Convert.ToInt32(xml.Element("Id").Value),
                WidgetId = Convert.ToInt32(xml.Element("WidgetId").Value),
                Key = xml.Element("Key").Value,
                Value = xml.Element("Value").Value
            };
        }

        public static Page ToPage(this XElement xml)
        {
            return new Page
            {
                Id = Convert.ToInt32(xml.Element("Id").Value),
                Name = xml.Element("Name").Value,
                ParentId = GetNullableId(xml.Element("ParentId").Value),
                HtmlTitle = xml.Element("HtmlTitle").Value,
                CreationTime = Convert.ToDateTime(xml.Element("CreationTime").Value),
                IsOnline = Convert.ToBoolean(xml.Element("IsOnline").Value),
                Url = xml.Element("Url").Value,
                Template = xml.Element("Template").Value,
                MetaFieldsJson = xml.Element("MetaFieldsJson").Value
            };
        }

        public static PageWidget ToPageWidget(this XElement xml)
        {
            return new PageWidget
            {
                WidgetId = Convert.ToInt32(xml.Element("WidgetId").Value),
                PageId = Convert.ToInt32(xml.Element("PageId").Value),
                DisplayOrder = Convert.ToInt32(xml.Element("DisplayOrder").Value),
                ZoneName = xml.Element("ZoneName").Value
            };
        }
    }
}
