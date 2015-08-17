using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using SusuCMS.Web;
using SusuCMS.Data;

namespace SusuCMS
{
    public static class MenuExtensions
    {
        public static bool IsAncestor(this Menu menu, Menu child)
        {
            var parent = child.Parent;
            while (parent != null)
            {
                if (menu.Id == parent.Id)
                {
                    return true;
                }

                parent = parent.Parent;
            }

            return false;
        }

        public static bool IsCurrent(this Menu menu)
        {
            var currentUrl = HttpContext.Current.Request.Url.AbsolutePath;
            var urlHelper = new FrontUrlHelper(FrontContext.Current);
            var menuUrl = urlHelper.GetClientUrl(menu.Url);

            return menuUrl.Equals(currentUrl, StringComparison.OrdinalIgnoreCase);
        }

        public static XElement ToXElement(this Menu menu)
        {
            return new XElement("Menu",
                new XElement("Id", menu.Id),
                new XElement("Name", menu.Name),
                new XElement("Url", menu.Url),
                new XElement("DisplayOrder", menu.DisplayOrder),
                new XElement("ParentId", menu.ParentId),
                new XElement("IsOnline", menu.IsOnline),
                new XElement("OpenInNewWindow", menu.OpenInNewWindow)
            );
        }
    }
}
