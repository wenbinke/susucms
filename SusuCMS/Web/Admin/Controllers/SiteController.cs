using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using SusuCMS.Web;
using System.Data.Entity.Validation;
using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using SusuCMS.Services;
using SusuCMS.Data;
using SusuCMS.Data.Enums;

namespace SusuCMS.Web.Admin.Controllers
{
    [AdminAuthorize(Permission.Sites)]
    public class SiteController : AdminControllerBase
    {
        private SelectList _templates;
        public SelectList Templates
        {
            get
            {
                if (_templates == null)
                {
                    var list = SiteService.GetSiteInfos();
                    _templates = new SelectList(list, "Name", "DisplayName");
                }

                return _templates;
            }
        }

        public ActionResult Index()
        {
            return View(DataContext.Sites.Where(i => !i.IsDeleted));
        }

        #region Create

        public ActionResult Create()
        {
            var site = new Site();
            site.UrlsJson = System.Web.Helpers.Json.Encode(new List<SiteUrl> { new SiteUrl() });

            return View(site);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Site model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var service = new SiteService(DataContext);
                    service.CreateSite(model);

                    ShowSuccess(MessageResource.CreateSuccess);

                    return RedirectToIndex();
                }
                catch (Exception e)
                {
                    LogError(e.ToString());
                    ShowError(MessageResource.CreateFailed);
                }
            }

            return View(model);
        }

        #endregion

        #region Edit

        public ActionResult Edit(int id)
        {
            var site = FindSite(id);
            if (site == null)
            {
                return HttpNotFound();
            }

            return View(site);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var site = FindSite(id);
            TryUpdateModel(site);
            if (ModelState.IsValid)
            {
                try
                {
                    SaveChanges();

                    ShowSuccess(MessageResource.UpdateSuccess);

                    return RedirectToIndex();
                }
                catch (Exception ex)
                {
                    LogError(ex.ToString());
                    ShowError(MessageResource.UpdateFailed);
                }
            }

            return View(site);
        }

        #endregion

        #region Delete

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var site = FindSite(id);
                site.IsDeleted = true;
                SaveChanges();

                ShowSuccess(MessageResource.DeleteSuccess);

                return RedirectToIndex();
            }
            catch (Exception ex)
            {
                LogError(ex.ToString());
                ShowError(MessageResource.DeleteFailed);
            }

            return RedirectToIndex();
        }

        #endregion

        public ActionResult Home()
        {
            if (CurrentSite == null)
            {
                return HttpNotFound();
            }

            return View(CurrentSite);
        }

        public ActionResult List()
        {
            return PartialView(DataContext.Sites.Where(i => !i.IsDeleted));
        }

        public string NameAvailable(string name, int id)
        {
            return (!DataContext.Sites.Any(i => i.Name == name && i.Id != id)).ToString().ToLower();
        }

        #region ToggleOnline
        public ActionResult ToggleOnline(int id)
        {
            try
            {
                var site = FindSite(id);
                site.IsOnline = !site.IsOnline;
                SaveChanges();

                ShowSuccess(MessageResource.SetOnlineOrOfflineSuccess);

                return RedirectToIndex();
            }
            catch (Exception ex)
            {
                LogError(ex.ToString());
                ShowError(MessageResource.SetOnlineOrOfflineFailed);
            }

            return RedirectToIndex();
        }

        #endregion

        public ActionResult Export(int id)
        {
            var site = FindSite(id);

            return View(site);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Export(int id, string fileName)
        {
            var site = FindSite(id);
            try
            {
                if (ModelState.IsValid)
                {
                    var siteService = new SiteService(DataContext);
                    var xml = siteService.Export(site, fileName);

                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
                    Response.Write(xml);
                    Response.End();

                   // ShowSuccess(MessageResource.ExportSuccess);
                }
            }
            catch (Exception ex)
            {
                LogError(ex.ToString());
                ShowError(MessageResource.ExportFailed);
            }

            return View(site);
        }

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var siteService = new SiteService(DataContext);
                    siteService.Import(XDocument.Load(file.InputStream));

                    ShowSuccess(MessageResource.ImportSuccess);
                }
            }
            catch (Exception ex)
            {
                LogError(ex.ToString());
                ShowError(MessageResource.ImportFailed);
            }

            return View();
        }

        private Site FindSite(int id)
        {
            return DataContext.Sites.FirstOrDefault(i => i.Id == id && !i.IsDeleted);
        }
    }
}
