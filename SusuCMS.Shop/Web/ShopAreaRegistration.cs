using System.Web.Mvc;

namespace SusuCMS.Shop.Web
{
    public class ShopAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Shop";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Shop_default",
                "Admin/{siteId}/Shop/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { siteId = @"\d+" },
                new[] { "SusuCMS.Shop.Controllers" }
            );
        }
    }
}
