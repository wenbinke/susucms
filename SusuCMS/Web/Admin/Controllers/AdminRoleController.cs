using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using SusuCMS.Data;
using SusuCMS.Data.Enums;



namespace SusuCMS.Web.Admin.Controllers
{
    [AdminAuthorize(Permission.Users)]
    public class AdminRoleController : AdminControllerBase
    {
        public ActionResult Index()
        {
            return View(DataContext.AdminRoles);
        }

        public ActionResult Create()
        {
            return View(new AdminRole());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(AdminRole model, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.UpdatePermissions(GetPermissions(form));

                    DataContext.AdminRoles.Add(model);
                    SaveChanges();

                    ShowSuccess(MessageResource.CreateSuccess);

                    return RedirectToIndex();
                }
                catch
                {
                    ShowError(MessageResource.CreateFailed);
                }
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var role = FindRole(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var role = FindRole(id);
            TryUpdateModel(role);

            if (ModelState.IsValid)
            {
                if (!role.IsRoot)
                {
                    try
                    {
                        role.UpdatePermissions(GetPermissions(form));

                        SaveChanges();

                        ShowSuccess(MessageResource.UpdateSuccess);

                        return RedirectToIndex();
                    }
                    catch
                    {
                        ShowError(MessageResource.UpdateFailed);
                    }
                }
            }

            return View(role);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AdminRole role)
        {
            try
            {
                DataContext.Delete(role);
                SaveChanges();
                ShowSuccess(MessageResource.DeleteSuccess);

                return RedirectToIndex();
            }
            catch
            {
                ShowError(MessageResource.DeleteFailed);
            }

            return RedirectToIndex();
        }

        private AdminRole FindRole(int id)
        {
            return DataContext.AdminRoles.FirstOrDefault(i => i.Id == id);
        }

        private IList<Permission> GetPermissions(FormCollection form)
        {
            var list = new List<Permission>();
            foreach (var item in form.AllKeys)
            {
                if (item.StartsWith("permission") && form[item].Contains("true"))
                {
                    list.Add((Permission)Convert.ToInt32(item.Replace("permission", "")));
                }
            }

            return list;
        }
    }
}
