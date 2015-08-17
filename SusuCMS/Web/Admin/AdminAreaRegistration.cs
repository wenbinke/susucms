using System.Web.Mvc;

namespace SusuCMS.Web.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        internal static string ModuleName = "Admin";

        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        private readonly string _adminControllers = "account|adminrole|adminuser|user|home|imagebrowser|label|media|menu|page|site|sitesetting|systemlog|theme|widget|mail";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "admin_login",
                "Admin/Login",
                new { controller = "Account", action = "Login" },
                null,
                new[] { "SusuCMS.Web.Admin.Controllers" }
            );

            context.MapRoute(
                "admin_home",
                "Admin/Home",
                new { controller = "Home", action = "Index" },
                null,
                new[] { "SusuCMS.Web.Admin.Controllers" }
            );

            context.MapRoute(
                "admin_nopermission",
                "Admin/NoPermission",
                new { controller = "Shared", action = "NoPermission" },
                null,
                new[] { "SusuCMS.Web.Admin.Controllers" }
            );

            context.MapRoute(
                "admin_site",
                "Admin/{SiteId}",
                new { controller = "Site", action = "Home" },
                new { siteId = @"\d+" },
                new[] { "SusuCMS.Web.Admin.Controllers" }
            );

            context.MapRoute(
                "admin_site_default",
                "Admin/{SiteId}/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new { siteId = @"\d+", controller = _adminControllers},
                new[] { "SusuCMS.Web.Admin.Controllers" }
            );

            context.MapRoute(
                "admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { controller = _adminControllers },
                new[] { "SusuCMS.Web.Admin.Controllers" }
            );
        }
    }
}
