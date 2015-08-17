using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using SusuCMS.Data.Enums;



namespace SusuCMS.Web.Admin.Controllers
{
    [AdminAuthorize(Permission.SiteSettings)]
    public class SiteSettingController : AdminControllerBase
    {
        public ActionResult Index()
        {
            CurrentSite.EnsureRobots();

            var path = HttpContext.Server.MapPath(CurrentSite.GetRobotsPath());
            using (var reader = new StreamReader(path))
            {
                ViewBag.Robots = reader.ReadToEnd();
            }

            return View(CurrentSite);
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Index(string analyticsCode, string robots, string metaFieldsJson)
        {
            try
            {
                var tempPath = GetTempFaviconPath();
                var faviconPath = Path.Combine(Server.MapPath(CurrentSite.GetFaviconPath()));
                if (System.IO.File.Exists(tempPath))
                {
                    if (System.IO.File.Exists(faviconPath))
                    {
                        System.IO.File.Delete(faviconPath);
                    }

                    System.IO.File.Move(tempPath, faviconPath);
                }

                CurrentSite.EnsureRobots();
                var path = HttpContext.Server.MapPath(CurrentSite.GetRobotsPath());
                using (var writer = new StreamWriter(path))
                {
                    writer.Write(robots);
                }

                CurrentSite.AnalyticsCode = analyticsCode;
                CurrentSite.MetaFieldsJson = metaFieldsJson;

                SaveChanges();

                ShowSuccess(MessageResource.UpdateSuccess);
            }
            catch
            {
                ShowError(MessageResource.UpdateFailed);
            }

            return View(CurrentSite);
        }

        [HttpPost]
        public void FaviconUpload(HttpPostedFileBase favicon)
        {
            favicon.SaveAs(GetTempFaviconPath());
        }

        public void FaviconRemove()
        {
            var path = GetTempFaviconPath();
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        private string GetTempFaviconPath()
        {
            return Path.Combine(Server.MapPath(CurrentSite.GetDataFolderPath()), "temp.ico");
        }
    }
}
