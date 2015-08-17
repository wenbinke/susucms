using System.Web.Mvc;

namespace SusuCMS.Web
{
    public class FrontController : FrontControllerBase
    {
        [FrontPage]
        public ActionResult Page()
        {
            if (CurrentSite == null)
            {
                return SiteNotFound();
            }

            if (CurrentPage == null)
            {
                return PageNotFound();
            }

            return View(FrontContext.Current.TemplatePath);
        }

        public ActionResult Robots()
        {
            if (CurrentSite == null)
            {
                return SiteNotFound();
            }

            return File(CurrentSite.GetRobotsPath(), "text/plain");
        }
    }
}
