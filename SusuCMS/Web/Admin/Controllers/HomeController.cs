using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using SusuCMS.Data.Enums;


namespace SusuCMS.Web.Admin.Controllers
{
    [HandleError, Authorize]
    public class HomeController : AdminControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        [AdminAuthorize(Permission.SystemSettings)]
        public ActionResult Setting()
        {
            return View();
        }
    }
}
