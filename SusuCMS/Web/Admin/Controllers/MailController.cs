using System.Linq;
using System.Web.Mvc;
using SusuCMS.Data;
using SusuCMS.Data.Enums;
using SusuCMS.Services;

namespace SusuCMS.Web.Admin.Controllers
{
    [AdminAuthorize(Permission.Sites)]
    public class MailController : AdminControllerBase
    {
        public ActionResult Index()
        {
            var list = DataContext.Mails.WithSiteId(CurrentSiteId).OrderByDescending(i => i.CreationTime);

            return View(list);
        }

        public ActionResult Resend(int id)
        {
            try
            {
                var mail = DataContext.Mails.WithSiteId(CurrentSiteId).FirstOrDefault(i => i.Id == id);
                var mailService = new MailService(DataContext);
                var mailSetting = CurrentSite.GetMailSetting();
                if (mailSetting == null)
                {
                    ShowError(MessageResource.ConfigMailSettingFirst);
                }
                else
                {

                    mailService.SendMail(mailSetting, mail);
                    SaveChanges();

                    ShowSuccess(MessageResource.ResendMailSuccess);
                }
            }
            catch
            {
                ShowError(MessageResource.ResendMailFailed);
            }

            return RedirectToIndex();
        }


        public ActionResult Setting()
        {
            var setting = CurrentSite.GetMailSetting();
            if (setting == null)
            {
                setting = new MailSetting { Port = 25 };
            }

            return View(setting);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Setting(MailSetting model)
        {
            try
            {
                CurrentSite.SetMailSetting(model);
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
