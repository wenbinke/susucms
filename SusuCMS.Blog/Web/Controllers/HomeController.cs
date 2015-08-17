using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SusuCMS.Blog.Web;

namespace SusuCMS.Blog.Web.Controllers
{
    public class HomeController : BlogControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
