using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SusuCMS.Web
{
    // TODO: we need a install page for user to install cms.
    public class InstallController : Controller
    {
        public ActionResult Install()
        {
            return View();
        }
    }
}
