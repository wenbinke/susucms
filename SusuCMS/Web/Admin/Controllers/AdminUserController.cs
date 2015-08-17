using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using SusuCMS.Web.Admin.Models;
using SusuCMS.Services;
using SusuCMS.Data;
using SusuCMS.Data.Enums;


namespace SusuCMS.Web.Admin.Controllers
{
    [AdminAuthorize(Permission.Users)]
    public class AdminUserController : AdminControllerBase
    {
        public ActionResult Index(int? page)
        {
            return View(DataContext.AdminUsers);
        }

        public ActionResult Create()
        {
            return View(new CreateAdminUserModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(CreateAdminUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var service = new PermissionService(DataContext);
                    service.CreateUser(new AdminUser
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = model.Password,
                        RoleId = model.RoleId
                    });
                    ShowSuccess(MessageResource.CreateSuccess);

                    return RedirectToIndex();
                }
                catch (Exception ex)
                {
                    LogError(ex.ToString());
                    ShowError(MessageResource.CreateFailed);
                }
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var user = FindUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new EditAdminUserModel
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    RoleId = user.RoleId,
                    IsRoot = user.IsRoot
                });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditAdminUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = FindUser(id);
                    if (user != null)
                    {
                        user.Email = model.Email;
                        user.RoleId = model.RoleId;
                        if (!String.IsNullOrWhiteSpace(model.Password))
                        {
                            user.ChangePassword(model.Password);
                        }
                        SaveChanges();

                        ShowSuccess(MessageResource.UpdateSuccess);
                    }

                    return RedirectToIndex();
                }
                catch (Exception ex)
                {
                    LogError(ex.ToString());
                    ShowError(MessageResource.UpdateFailed);
                }
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AdminUser user)
        {
            try
            {
                DataContext.Delete(user);
                SaveChanges();

                ShowSuccess(MessageResource.DeleteSuccess);

                return RedirectToIndex();
            }
            catch (Exception ex)
            {
                LogError(ex.ToString());
                ShowError(MessageResource.DeleteFailed);
            }

            return RedirectToIndex();
        }

        public ActionResult UserNameAvailable(string username)
        {
           var result = !DataContext.AdminUsers.Any(i => i.UserName == username);

           return Json(result, JsonRequestBehavior.AllowGet);
        }

        private AdminUser FindUser(int id)
        {
            return DataContext.AdminUsers.FirstOrDefault(i => i.Id == id);
        }
    }
}
