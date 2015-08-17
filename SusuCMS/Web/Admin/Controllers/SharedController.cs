using System.Web.Mvc;

namespace SusuCMS.Web.Admin.Controllers
{
    [Authorize]
    public class SharedController : AdminControllerBase
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;

            return View();
        }

        public ActionResult NoPermission()
        {
            Response.StatusCode = 403;

            return View();
        }
    }
}
