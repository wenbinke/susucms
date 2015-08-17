using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using SusuCMS.Web.Admin.Models;


namespace SusuCMS.Web.Admin.Controllers
{
    public class AccountController : AdminControllerBase
    {
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                CurrentUser.ChangePassword(model.NewPassword);
                SaveChanges();
                ShowSuccess(MessageResource.UpdateSuccess);
            }

            return View(model);
        }

        public string VerifyPassword(string oldPassword)
        {
            return CurrentUser.VerifyPassword(oldPassword).ToString().ToLower();
        }

        [Authorize]
        public ActionResult ChangeLanguage()
        {
            ViewBag.Language = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;

            return View();
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult ChangeLanguage(string language)
        {
            Response.Cookies.Set(new HttpCookie("AdminPanelLanguage", language));
            ShowSuccess(MessageResource.UpdateSuccess);

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = DataContext.AdminUsers.FirstOrDefault(i => i.UserName == model.UserName);
                if (user != null && user.VerifyPassword(model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("LoginError", MessageResource.UserNameOrPasswordIncorrect);
                }
            }

            return View(model);
        }
    }
}
