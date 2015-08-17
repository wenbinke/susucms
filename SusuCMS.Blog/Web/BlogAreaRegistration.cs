using System.Web.Mvc;
using System.Web.Optimization;
using SusuCMS.Blog.AppStart;

namespace SusuCMS.Blog.Web
{
    public class BlogAreaRegistration : AreaRegistration
    {
        internal static string ModuleName = "Blog";

        public override string AreaName
        {
            get
            {
                return "Blog";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Blog_default",
                "Admin/{SiteId}/Blog/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { siteId = @"\d+" },
                new[] { "SusuCMS.Blog.Web.Controllers" }
            );

            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
