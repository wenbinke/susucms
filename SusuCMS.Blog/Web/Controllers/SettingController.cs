using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SusuCMS.Blog.Data;

namespace SusuCMS.Blog.Web.Controllers
{
    public class SettingController : BlogControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Permalink()
        {
            var setting = CurrentSite.GetPermalinkSetting();

            return View(setting);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Permalink(PermalinkSetting model)
        {
            try
            {
                CurrentSite.SetPermalinkSetting(model);
                DataContext.SaveChanges();

                ShowSuccess(SusuCMS.MessageResource.UpdateSuccess);
            }
            catch
            {
                ShowError(SusuCMS.MessageResource.UpdateFailed);
            }

            return View(model);
        }
    }
}
