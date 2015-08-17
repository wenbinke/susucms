using System.Linq;
using System.Web;
using System.Web.Routing;
using System;


namespace SusuCMS.Web.Routing
{
    public class PageRouteHandler : System.Web.Mvc.MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var dataContext = WorkContext.GetDataContext();

            FrontContext.Current = new FrontContext
            {
                DataContext = dataContext
            };

            var parser = new PageRouteParser(dataContext, requestContext);
            var matchedSites = parser.GetMatchedSites();
            foreach (var matchedSite in matchedSites)
            {
                var site = matchedSite.Site;
                FrontContext.Current.Site = site;
                FrontContext.Current.Site.MatchedUrl = matchedSite.SiteUrl;

                var page = parser.GetMatchedPage(matchedSite);
                if (page != null)
                {
                    if (site.RedirectToMainUrl)
                    {
                        var mainUrl = site.Urls.First();
                        if (site.MatchedUrl.Name != mainUrl.Name)
                        {
                            var newPageUrl = parser.PageUrl;
                            var subFolder = site.MatchedUrl.GetSubFolder();
                            if (!String.IsNullOrWhiteSpace(subFolder))
                            {
                                newPageUrl = newPageUrl.Substring(subFolder.Length);
                            }

                            Redirect301(string.Format("{0}://{1}/{2}",
                                HttpContext.Current.Request.IsSecureConnection ? "https" : "http",
                                mainUrl.Name, newPageUrl));
                           
                            return base.GetHttpHandler(requestContext);
                        }
                    }

                    FrontContext.Current.Page = page.Page;

                    parser.MergeRouteData(page.RouteValues, requestContext.RouteData.Values);

                    break;
                }
            }

            return base.GetHttpHandler(requestContext);
        }

        private static void Redirect301(string url)
        {
            HttpContext.Current.Response.StatusCode = 301;
            HttpContext.Current.Response.RedirectLocation = url;
            HttpContext.Current.Response.End();
        }
    }
}