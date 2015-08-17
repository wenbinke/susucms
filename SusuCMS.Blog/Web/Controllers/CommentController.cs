using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SusuCMS.Blog.Data.Enums;

namespace SusuCMS.Blog.Web.Controllers
{
    public class CommentController : BlogControllerBase
    {
        public ActionResult Index(string status)
        {
            var list = BlogDataContext.Comments.WithSiteId(CurrentSiteId);
            if (!String.IsNullOrWhiteSpace(status))
            {
                var statusValue = (int)Enum.Parse(typeof(CommentStatus), status);
                list = list.Where(i => i.Status == statusValue);
            }

            return View(list.OrderByDescending(i => i.CreationTime));
        }

        public ActionResult Search(string keywords)
        {
            var list = BlogDataContext.Comments.WithSiteId(CurrentSiteId)
                .Where(i => i.Content.Contains(keywords)
                || i.Author.Contains(keywords) 
                || i.Email.Contains(keywords)
                || i.Url.Contains(keywords));

            return View("Index", list.OrderByDescending(i => i.CreationTime));
        }
    }
}
