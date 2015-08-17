using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;
using SusuCMS.Data;

namespace SusuCMS.Web.Routing
{
    internal class PageRouteParser
    {
        private DataContext _dataContext;
        private RequestContext _requestContext;
        public string Host { get; private set; }
        public string PageUrl { get; private set; }

        public PageRouteParser(DataContext dataContext, RequestContext requestContext)
        {
            _dataContext = dataContext;
            _requestContext = requestContext;

            Host = _requestContext.HttpContext.Request.Headers["host"];
            PageUrl = (string)_requestContext.RouteData.Values["PageUrl"];
        }

        /// <summary>
        /// http://{siteUrl}/{pageUrl}
        /// </summary>
        public IList<MatchedSite> GetMatchedSites()
        {
            var urlWithHost = string.Format("{0}/{1}", Host, PageUrl).TrimEnd("/".ToCharArray());

            var list = new List<MatchedSite>();
            foreach (var site in GetOnlineSites())
            {
                foreach (var siteUrl in site.Urls)
                {
                    var regex = new Regex(string.Format("^{0}", siteUrl.Name), RegexOptions.IgnoreCase);
                    if (regex.IsMatch(urlWithHost))
                    {
                        list.Add(new MatchedSite
                        {
                            SiteUrl = siteUrl,
                            Site = site,
                            IsHomePage = urlWithHost.Equals(siteUrl.Name, StringComparison.OrdinalIgnoreCase)
                        });
                    }
                }
            }

            return list;
        }

        private IQueryable<Site> GetOnlineSites()
        {
            return _dataContext.Sites.Where(i => i.IsOnline && !i.IsDeleted);
        }

        public MatchedPage GetMatchedPage(MatchedSite matchedSite)
        {
            var siteUrl = matchedSite.SiteUrl;
            if (matchedSite.IsHomePage) // home page
            {
                var homePage = GetOnlinePages(matchedSite.Site.Id).FirstOrDefault(i => i.ParentId == null);
                return new MatchedPage
                {
                    Page = homePage,
                    RouteValues = new RouteValueDictionary()
                };
            }
            else
            {
                var subFolder = siteUrl.GetSubFolder();
                if (!String.IsNullOrWhiteSpace(subFolder))
                {
                    subFolder += "/";
                }

                var routes = new RouteCollection();
                var pages = GetOnlinePages(matchedSite.Site.Id);
                foreach (var page in pages)
                {
                    routes.Add(new Route(subFolder + page.Url, null, null,
                        new RouteValueDictionary { { "pageId", page.Id } },
                        new MvcRouteHandler()));
                }

                var data = routes.GetRouteData(_requestContext.HttpContext);
                if (data != null)
                {
                    var pageId = (int)data.DataTokens["pageId"];
                    return new MatchedPage
                    {
                        Page = pages.First(i => i.Id == pageId),
                        RouteValues = data.Values
                    };
                }
            }

            return null;
        }

        private IQueryable<Page> GetOnlinePages(int siteId)
        {
            return _dataContext.Pages.WithSiteId(siteId).Where(i => i.IsOnline);
        }

        public void MergeRouteData(RouteValueDictionary source, RouteValueDictionary destination)
        {
            foreach (var item in source)
            {
                if (destination.Any(i => i.Key == item.Key))
                {
                    destination[item.Key] = item.Value;
                }
                else
                {
                    destination.Add(item.Key, item.Value);
                }
            }
        }
    }

    internal class MatchedPage
    {
        public Page Page { get; set; }

        public RouteValueDictionary RouteValues { get; set; }
    }

    internal class MatchedSite
    {
        public SiteUrl SiteUrl { get; set; }

        public Site Site { get; set; }

        public bool IsHomePage { get; set; }
    }
}
