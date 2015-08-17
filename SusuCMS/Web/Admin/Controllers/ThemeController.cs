using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml.Serialization;
using SusuCMS.Web.Admin.Models;
using SusuCMS.Data.Enums;


namespace SusuCMS.Web.Admin.Controllers
{
    [AdminAuthorize(Permission.Pages)]
    public class ThemeController : AdminControllerBase
    {
        public ActionResult CustomCss()
        {
            CurrentSite.EnsureCustomCss();

            var path = HttpContext.Server.MapPath(CurrentSite.GetCustomCssPath());
            using (var reader = new StreamReader(path))
            {
                ViewBag.Content = reader.ReadToEnd();
            }

            ViewBag.EnableCustomCss = CurrentSite.EnableCustomCss;

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult CustomCss(bool enableCustomCss, string content)
        {
            try
            {
                CurrentSite.EnsureCustomCss();

                var path = HttpContext.Server.MapPath(CurrentSite.GetCustomCssPath());
                using (var writer = new StreamWriter(path))
                {
                    writer.Write(content);
                }
                CurrentSite.EnableCustomCss = enableCustomCss;
                SaveChanges();

                ShowSuccess(MessageResource.UpdateSuccess);
            }
            catch
            {
                ShowError(MessageResource.UpdateFailed);
            }

            ViewBag.Content = content;
            ViewBag.EnableCustomCss = enableCustomCss;

            return View();
        }

        public ActionResult RenderTheme(string themeName)
        {
            return PartialView("RenderTheme", themeName);
        }
    }
}
