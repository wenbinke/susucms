using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SusuCMS.Blog.Data;

namespace SusuCMS.Blog.Web.Controllers
{
    public class ContactController : BlogControllerBase
    {
        public ActionResult Index()
        {
            var list = BlogDataContext.Contacts.WithSiteId(CurrentSiteId);

            return View(list);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Contact contact)
        {
            try
            {
                BlogDataContext.Delete(contact);

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
    }
}
