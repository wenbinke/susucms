using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using SusuCMS.Helpers;
using SusuCMS.AppStart;
using System.Web.Optimization;

namespace SusuCMS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "robots_txt",
                "robots.txt",
                new { controller = "Front", action = "Robots" },
                new[] { "SusuCMS.Web" }
            ).RouteHandler = new SusuCMS.Web.Routing.PageRouteHandler();

            routes.MapRoute(
                "page_default",
                "{*PageUrl}",
                new { controller = "Front", action = "Page" },
                new[] { "SusuCMS.Web" }
            ).RouteHandler = new SusuCMS.Web.Routing.PageRouteHandler();
        }

        protected void Application_Start()
        {
            Starter.Initialize();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object o, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            LogHelper.AddSystemErrorLog(Server.GetLastError().ToString());
        }
    }
}