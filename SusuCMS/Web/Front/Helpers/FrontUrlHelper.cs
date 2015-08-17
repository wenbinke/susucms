using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web;

namespace SusuCMS.Web
{
    public class FrontUrlHelper
    {
        private FrontContext _frontContext;

        public FrontUrlHelper(FrontContext frontContext)
        {
            _frontContext = frontContext;
        }

        public string GetClientUrl(string url = null)
        {
            var site = _frontContext.Site;
            var subFolder = site.MatchedUrl.GetSubFolder();
            if (String.IsNullOrWhiteSpace(url))
            {
                url = "/" + subFolder;
            }
            else if (!url.ToLower().StartsWith("http://") && !url.ToLower().StartsWith("https://"))
            {
                if (String.IsNullOrWhiteSpace(subFolder))
                {
                    url = "/" + url;
                }
                else
                {
                    url = "/" + subFolder + "/" + url;
                }
            }

            return url;
        }

        public string GetClientUrl(string url, RouteValueDictionary values)
        {
            var site = _frontContext.Site;
            var subFolder = site.MatchedUrl.GetSubFolder();
            if (!String.IsNullOrWhiteSpace(subFolder))
            {
                url = subFolder + "/" + url;
            }

            var route = new Route(url, new MvcRouteHandler());
            var data = route.GetVirtualPath(HttpContext.Current.Request.RequestContext, values);
            if (data == null)
            {
                return "/";
            }

            return "/" + data.VirtualPath;
        }

        public string GetClientUrl(string url, object routeValues)
        {
            return GetClientUrl(url, new RouteValueDictionary(routeValues));
        }
    }
}
