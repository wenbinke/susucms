using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Handlers;
using System.Web.Routing;
using SusuCMS.Web;
using System.IO;
using SusuCMS.Helpers;
using SusuCMS.Data;
using SusuCMS.Data.Enums;


namespace SusuCMS.Web.Admin.Controllers
{
    [AdminAuthorize(Permission.Pages)]
    public class PageController : AdminControllerBase
    {
        private SelectList _templates;
        public SelectList Templates
        {
            get
            {
                if (_templates == null)
                {
                    _templates = new SelectList(CurrentSite.GetTemplateNames());
                }

                return _templates;
            }
        }

        public ActionResult Index()
        {
            var pages = DataContext.Pages.WithSiteId<Page>(CurrentSiteId).ToTreeList();

            return View(pages.AsQueryable());
        }

        public ActionResult Create()
        {
            return View(new Page
            {
                SiteId = CurrentSiteId,
                MetaFieldsJson = System.Web.Helpers.Json.Encode(new List<MetaField> { 
                    new MetaField { Name = "keywords" }, 
                    new MetaField { Name = "description" }
                })
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Page model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreationTime = DateTime.Now;
                    var home = DataContext.Pages.WithSiteId(CurrentSiteId).FirstOrDefault(i => i.ParentId == null);
                    model.ParentId = home.Id;
                    DataContext.Pages.Add(model);
                    SaveChanges();

                    ShowSuccess(MessageResource.CreateSuccess);

                    return RedirectToAction("Index");
                }
                catch
                {

                    ShowError(MessageResource.CreateFailed);
                }
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var page = FindPage(id);
            if (page == null)
            {
                return HttpNotFound();
            }

            return View(page);
        }

        [HttpPost, ValidateInput(false), ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var page = FindPage(id);
            TryUpdateModel(page);

            if (ModelState.IsValid)
            {
                try
                {
                    SaveChanges();

                    ShowSuccess(MessageResource.UpdateSuccess);

                    return RedirectToIndex();
                }
                catch
                {
                    ShowError(MessageResource.UpdateFailed);
                }
            }

            return View(page);
        }

        [PreviewPage]
        public ActionResult Preview(int id)
        {
            var page = FindPage(id);
            if (page == null)
            {
                return HttpNotFound();
            }

            DesignHelper.SetPreview(CurrentSite, page);

            return View(FrontContext.Current.TemplatePath, page);
        }

        public ActionResult Design(int id)
        {
            var page = FindPage(id);
            if (page == null)
            {
                return HttpNotFound();
            }

            DesignHelper.SetPreview(CurrentSite, page);

            return View(page);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public string Design(int id, FormCollection collection)
        {
            var result = true;
            try
            {
                var page = FindPage(id);
                var pageWidgetsJson = collection["PageWidgetsJson"];
                if (!String.IsNullOrWhiteSpace(pageWidgetsJson))
                {
                    var list = System.Web.Helpers.Json.Decode<List<PageWidget>>(pageWidgetsJson);
                    foreach (var item in page.Widgets.ToArray())
                    {
                        var widget = list.FirstOrDefault(i => i.WidgetId == item.WidgetId);
                        if (widget == null)
                        {
                            page.Widgets.Remove(item);
                        }
                        else
                        {
                            item.ZoneName = widget.ZoneName;
                            item.DisplayOrder = widget.DisplayOrder;
                        }
                    }

                    foreach (var item in list)
                    {
                        if (!page.Widgets.Any(i => i.WidgetId == item.WidgetId))
                        {
                            page.Widgets.Add(new PageWidget
                            {
                                PageId = id,
                                WidgetId = item.WidgetId,
                                ZoneName = item.ZoneName,
                                DisplayOrder = item.DisplayOrder
                            });
                        }
                    }
                }

                CurrentSite.Theme = collection["ThemeName"];

                SaveChanges();
            }
            catch
            {
                result = false;
            }

            return result.ToString().ToLower();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Page page)
        {
            try
            {
                DataContext.Delete(page);
                SaveChanges();

                ShowSuccess(MessageResource.DeleteSuccess);
            }
            catch
            {
                ShowError(MessageResource.DeleteFailed);
            }

            return RedirectToIndex();
        }

        public ActionResult ToggleOnline(int id)
        {
            try
            {
                var page = FindPage(id);

                page.IsOnline = !page.IsOnline;
                SaveChanges();

                ShowSuccess(MessageResource.SetOnlineOrOfflineSuccess);

                return RedirectToIndex();
            }
            catch
            {
                ShowError(MessageResource.SetOnlineOrOfflineFailed);
            }

            return RedirectToIndex();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Move(int id, int? parentId)
        {
            var list = DataContext.Pages.WithSiteId(CurrentSiteId).ToList();
            var page = list.FirstOrDefault(i => i.Id == id);
            try
            {
                // check if the page to be moved is the parent of the page that move to.
                if (parentId != null)
                {
                    var parent = list.WithSiteId(CurrentSiteId).FirstOrDefault(i => i.Id == parentId.Value);
                    while (parent != null)
                    {
                        if (parent.Id == id)
                        {
                            ShowError(MessageResource.PageCannotMoveUnderChild);
                            return Redirect(Request.UrlReferrer.AbsoluteUri);
                        }
                        else
                        {
                            if (parent.ParentId.HasValue)
                            {
                                parent = list.FirstOrDefault(i => i.Id == parent.ParentId);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                page.ParentId = parentId;
                SaveChanges();
                ShowSuccess(MessageResource.UpdateSuccess);
            }
            catch
            {
                ShowError(MessageResource.UpdateFailed);
            }

            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        private Page FindPage(int id)
        {
            return DataContext.Pages.WithSiteId(CurrentSiteId).FirstOrDefault(i => i.Id == id);
        }
    }
}
