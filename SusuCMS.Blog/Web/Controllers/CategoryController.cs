using System.Linq;
using System.Web.Mvc;
using SusuCMS.Blog;
using System.Collections.Generic;
using SusuCMS.Blog.Web;
using System.Data;
using SusuCMS.Blog.Data;

namespace SusuCMS.Blog.Web.Controllers
{
    public class CategoryController : BlogControllerBase
    {
        public ActionResult Index()
        {
            var list = BlogDataContext.Categories.WithSiteId(CurrentSiteId).ToTreeList();

            return View(list.AsQueryable());
        }

        public ActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BlogDataContext.Categories.Add(model);
                    SaveChanges();

                    ShowSuccess(SusuCMS.MessageResource.CreateSuccess);

                    return RedirectToIndex();
                }
                catch
                {
                    ShowError(SusuCMS.MessageResource.CreateFailed);
                }
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var category = FindCategory(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var category = FindCategory(id);
            TryUpdateModel(category);

            if (ModelState.IsValid)
            {
                try
                {
                    SaveChanges();

                    ShowSuccess(SusuCMS.MessageResource.UpdateSuccess);

                    return RedirectToIndex();
                }
                catch
                {
                    ShowError(SusuCMS.MessageResource.UpdateFailed);
                }
            }

            return View(category);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                BlogDataContext.Delete(category);
                SaveChanges();
                ShowSuccess(SusuCMS.MessageResource.DeleteSuccess);

                return RedirectToIndex();
            }
            catch
            {
                ShowError(SusuCMS.MessageResource.DeleteFailed);
            }

            return RedirectToIndex();
        }

        public ActionResult ToggleOnline(int id)
        {
            try
            {
                var category = FindCategory(id);
                category.IsOnline = !category.IsOnline;
                SaveChanges();
                ShowSuccess(SusuCMS.MessageResource.SetOnlineOrOfflineSuccess);

                return RedirectToIndex();
            }
            catch
            {
                ShowError(SusuCMS.MessageResource.SetOnlineOrOfflineFailed);
            }

            return RedirectToIndex();
        }

        private Category FindCategory(int id)
        {
            return BlogDataContext.Categories.WithSiteId(CurrentSiteId).FirstOrDefault(i => i.Id == id);
        }
    }
}
