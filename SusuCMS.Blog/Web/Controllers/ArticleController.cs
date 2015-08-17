using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SusuCMS.Blog.Web;
using SusuCMS;
using SusuCMS.Blog.Data;

namespace SusuCMS.Blog.Web.Controllers
{
    [Authorize]
    public class ArticleController : BlogControllerBase
    {
        public ActionResult Index(int? categoryId, int? tagId)
        {
            var list = BlogDataContext.Articles.Include("Tags").Include("Category").WithSiteId(CurrentSiteId);
            if (categoryId != null && categoryId.Value != 0)
            {
                list = list.Where(i => i.CategoryId == categoryId);
            }

            if (tagId != null && tagId.Value != 0)
            {
                list = list.Where(i => i.Tags.Any(j => j.Id == tagId.Value));
            }

            return View(list.OrderByDescending(i => i.CreationTime));
        }

        public ActionResult Search(string keywords)
        {
            var list = BlogDataContext.Articles.WithSiteId(CurrentSiteId)
                .Where(i => i.Title.Contains(keywords)
                    || i.Content.Contains(keywords)
                    || i.Author.Contains(keywords));

            return View("Index", list.OrderByDescending(i => i.CreationTime));
        }

        #region Create

        public ActionResult Create()
        {
            return View(new Article());
        }

        [HttpPost, ValidateInput(false), ValidateAntiForgeryToken]
        public ActionResult Create(Article model, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Tags = new List<Tag>();
                    var tagNames = System.Web.Helpers.Json.Decode<IList<string>>(form["TagsJson"]);
                    var siteTags = BlogDataContext.Tags.WithSiteId(CurrentSiteId);
                    foreach (var name in tagNames)
                    {
                        var tag = siteTags.FirstOrDefault(i => i.Name == name);
                        if (tag == null)
                        {
                            model.Tags.Add(new Tag { Name = name, SiteId = CurrentSiteId });
                        }
                        else
                        {
                            model.Tags.Add(tag);
                        }
                    }

                    model.CreationTime = DateTime.Now;
                    model.UpdateTime = DateTime.Now;

                    BlogDataContext.Articles.Add(model);
                    SaveChanges();

                    CalculateCount(model);

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

        #endregion


        public ActionResult Edit(int id)
        {
            var article = FindArticle(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            return View(article);
        }

        [HttpPost, ValidateInput(false), ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var article = FindArticle(id);
            TryUpdateModel(article);

            if (ModelState.IsValid)
            {
                try
                {
                    var tagNames = System.Web.Helpers.Json.Decode<IList<string>>(form["TagsJson"]);
                    foreach (var item in article.Tags.ToArray())
                    {
                        if (!tagNames.Any(i => i == item.Name))
                        {
                            article.Tags.Remove(item);
                        }
                    }

                    var siteTags = BlogDataContext.Tags.WithSiteId(CurrentSiteId);
                    foreach (var name in tagNames)
                    {
                        var tag = siteTags.FirstOrDefault(i => i.Name == name);
                        if (tag == null)
                        {
                            article.Tags.Add(new Tag { Name = name, SiteId = CurrentSiteId });
                        }
                        else
                        {
                            article.Tags.Add(tag);
                        }
                    }

                    article.UpdateTime = DateTime.Now;
                    SaveChanges();

                    CalculateCount(article);

                    ShowSuccess(SusuCMS.MessageResource.UpdateSuccess);

                    return RedirectToIndex();
                }
                catch
                {
                    ShowError(SusuCMS.MessageResource.UploadFileFailed);
                }
            }

            return View(article);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Article article)
        {
            try
            {
                BlogDataContext.Delete(article);
                SaveChanges();

                CalculateCount(article);

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
            var article = FindArticle(id);
            try
            {
                article.IsOnline = !article.IsOnline;
                SaveChanges();

                CalculateCount(article);

                ShowSuccess(SusuCMS.MessageResource.SetOnlineOrOfflineSuccess);

                return RedirectToIndex();
            }
            catch
            {
                ShowError(SusuCMS.MessageResource.SetOnlineOrOfflineFailed);
            }

            return RedirectToIndex();
        }

        private IList<int> GetCategoryIds(FormCollection form)
        {
            var list = new List<int>();
            foreach (var item in form.AllKeys)
            {
                if (item.StartsWith("category") && form[item].Contains("true"))
                {
                    list.Add(Convert.ToInt32(item.Replace("category", "")));
                }
            }

            return list;
        }

        private void CalculateCount(Article article)
        {
            if (article.Category != null)
            {
                article.Category.ArticleCount = BlogDataContext.Articles.WithSiteId(CurrentSiteId)
                    .Count(i => i.IsOnline && i.CategoryId == article.CategoryId);
            }

            foreach (var item in article.Tags)
            {
                item.ArticleCount = BlogDataContext.Articles.WithSiteId(CurrentSiteId)
                    .Count(i => i.Tags.Any(j => j.Id == item.Id) && i.IsOnline);
            }

            SaveChanges();
        }

        private Article FindArticle(int id)
        {
            return BlogDataContext.Articles.WithSiteId(CurrentSiteId).FirstOrDefault(i => i.Id == id);
        }
    }
}
