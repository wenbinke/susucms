using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SusuCMS.Data;
using SusuCMS.Data.Enums;

namespace SusuCMS.Web.Admin.Controllers
{
    [AdminAuthorize(Permission.Users)]
    public class UserController : AdminControllerBase
    {
        public ActionResult Index(int? page)
        {
            return View(DataContext.Users.WithSiteId(CurrentSiteId));
        }

        public ActionResult Setting()
        {
            var setting = CurrentSite.GetUserSetting();

            return View(setting);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Setting(string json)
        {
            try
            {
                var model = System.Web.Helpers.Json.Decode<UserSetting>(json);
                CurrentSite.SetUserSetting(model);
                SaveChanges();

                ShowSuccess(MessageResource.UpdateSuccess);
            }
            catch
            {
                ShowError(MessageResource.UpdateFailed);
            }

            return RedirectToIndex();
        }
    }
}
