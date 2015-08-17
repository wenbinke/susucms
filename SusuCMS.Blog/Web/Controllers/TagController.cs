using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SusuCMS.Blog.Web;

namespace SusuCMS.Blog.Web.Controllers
{
    public class TagController : BlogControllerBase
    {
        [HttpPost]
        public string Delete(int id)
        {
            var result = true;
            try
            {
                var tag = BlogDataContext.Tags.WithSiteId(CurrentSiteId).First(i => i.Id == id);
                tag.Articles.Clear();
                
                BlogDataContext.Delete(tag);
                SaveChanges();
            }
            catch(Exception ex)
            {
                LogError(ex.ToString());

                result = false;
            }

            return result.ToString().ToLower();
        }
    }
}
