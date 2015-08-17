using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SusuCMS.Helpers;
using SusuCMS.Data;
using SusuCMS.Data.Enums;



namespace SusuCMS.Web.Admin.Controllers
{
    [AdminAuthorize(Permission.Pages)]
    public class MenuController : AdminControllerBase
    {
        public ActionResult Index()
        {
            var list = DataContext.Menus.WithSiteId(CurrentSiteId).OrderBy(i => i.DisplayOrder).ToTreeList();

            return View(list);
        }

        public ActionResult Create()
        {
            return View(new Menu());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Menu model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var maxDislayOrder = DataContext.Menus.WithSiteId(CurrentSiteId).Max(i => i.DisplayOrder);
                    model.DisplayOrder = maxDislayOrder + 1;
                    DataContext.Menus.Add(model);
                    SaveChanges();

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
            var menu = FindMenu(id);
            if (menu == null)
            {
                return HttpNotFound();
            }

            return View(menu);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var menu = FindMenu(id);
            TryUpdateModel(menu);

            if (ModelState.IsValid)
            {
                try
                {
                    SaveChanges();

                    ShowSuccess(MessageResource.UpdateSuccess);

                    return RedirectToIndex();
                }
                catch
                {
                    ShowError(MessageResource.UpdateFailed);
                }
            }

            return View(menu);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Menu menu)
        {
            try
            {
                DataContext.Delete(menu);
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

        public ActionResult ToggleOnline(int id)
        {
            try
            {
                var menu = FindMenu(id);
                menu.IsOnline = !menu.IsOnline;
                SaveChanges();
                ShowSuccess(MessageResource.SetOnlineOrOfflineSuccess);

                return RedirectToIndex();
            }
            catch
            {
                ShowError(MessageResource.SetOnlineOrOfflineFailed);
            }

            return RedirectToIndex();
        }

        public ActionResult MoveUp(int id)
        {
            try
            {
                var item = FindMenu(id);
                var menus = GetList(item.ParentId);
                var sortHelper = new SortHelper<Menu>(menus);
                sortHelper.MoveUp(item);
                SaveChanges();
                ShowSuccess(MessageResource.MoveSuccess);

                return RedirectToIndex();
            }
            catch (Exception ex)
            {
                LogError(ex.ToString());
                ShowError(MessageResource.MoveFailed);
            }

            return RedirectToIndex();
        }

        public ActionResult MoveDown(int id)
        {
            try
            {
                var item = FindMenu(id);
                var menus = GetList(item.ParentId);
                var sortHelper = new SortHelper<Menu>(menus);
                sortHelper.MoveDown(item);
                SaveChanges();
                ShowSuccess(MessageResource.MoveSuccess);

                return RedirectToIndex();
            }
            catch (Exception ex)
            {
                LogError(ex.ToString());
                ShowError(MessageResource.MoveFailed);
            }

            return RedirectToIndex();
        }

        private IList<Menu> GetList(int? parentId)
        {
            if (parentId.HasValue)
            {
                return DataContext.Menus.WithSiteId(CurrentSiteId)
                    .Where(i => i.ParentId == parentId.Value).OrderBy(i => i.DisplayOrder).ToList();
            }
            else
            {
                return DataContext.Menus.WithSiteId(CurrentSiteId)
                    .Where(i => i.ParentId == null)
                    .OrderBy(i => i.DisplayOrder).ToList();
            }
        }

        private Menu FindMenu(int id)
        {
            return DataContext.Menus.WithSiteId(CurrentSiteId).FirstOrDefault(i => i.Id == id);
        }

    }
}
