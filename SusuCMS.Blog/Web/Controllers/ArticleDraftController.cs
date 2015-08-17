using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SusuCMS.Blog.Data;

namespace SusuCMS.Blog.Web.Controllers
{
    public class ArticleDraftController : BlogControllerBase
    {
        public ActionResult List()
        {
            return PartialView(GetArticleDrafts().OrderByDescending(i => i.Time));
        }

        private IQueryable<ArticleDraft> GetArticleDrafts()
        {
            return BlogDataContext.ArticleDrafts.WithSiteId(CurrentSiteId);
        }

        [HttpPost]
        public ActionResult Save(ArticleDraft model)
        {
            var draft = GetArticleDrafts().FirstOrDefault(i => i.Id == model.Id);
            if (draft != null)
            {
                draft.Title = model.Title;
                draft.Content = model.Content;
                draft.Time = DateTime.Now;
            }
            else
            {
                model.SiteId = CurrentSiteId;
                model.Time = DateTime.Now;
                BlogDataContext.ArticleDrafts.Add(model);
            }

            SaveChanges();

            return Json(new { Model = model, DraftCount = GetArticleDrafts().Count() });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = true;
            try
            {
                var draft = GetArticleDrafts().First(i => i.Id == id);

                BlogDataContext.Delete(draft);
                SaveChanges();
            }
            catch (Exception ex)
            {
                LogError(ex.ToString());

                result = false;
            }

            return Json(new { IsSuccess = result, DraftCount = GetArticleDrafts().Count() });
        }

        public ActionResult Get(int id)
        {
            var draft = GetArticleDrafts().FirstOrDefault(i => i.Id == id);

            return Json(draft, JsonRequestBehavior.AllowGet);
        }
    }
}
