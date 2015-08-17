using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml.Serialization;
using System.Resources;
using System.Xml.Linq;
using SusuCMS.Helpers;
using SusuCMS.Data;
using SusuCMS.Data.Enums;



namespace SusuCMS.Web.Admin.Controllers
{
    [AdminAuthorize(Permission.Pages)]
    public class WidgetController : AdminControllerBase
    {
        public ActionResult Create()
        {
            return View(new Widget());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Widget model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DataContext.Widgets.Add(model);
                    SaveChanges();
                }
                catch (Exception ex)
                {
                    LogError(ex.ToString());
                }
            }

            return PartialView("Option", model);
        }

        public ActionResult Edit(int id)
        {
            return View(FindWidget(id));
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var widget = FindWidget(id);
            TryUpdateModel(widget, new[] { "DisplayName", "DataJson", "LocalizeJson" });

            if (ModelState.IsValid)
            {
                try
                {
                    SaveChanges();
                }
                catch (Exception ex)
                {
                    LogError(ex.ToString());
                    return Json(new { IsSuccess = false, Message = MessageResource.UpdateFailed });
                }
            }

            return Json(new { IsSuccess = true, Message = MessageResource.UpdateSuccess });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Widget widget)
        {
            try
            {
                var labels = DataContext.WidgetLabels.Where(i => i.WidgetId == id);
                DataContext.Delete(labels);

                var pageWidgets = DataContext.PageWidgets.Where(i => i.WidgetId == id);
                DataContext.Delete(pageWidgets);

                DataContext.Delete(widget);
                SaveChanges();
            }
            catch (Exception ex)
            {
                LogError(ex.ToString());
                return Json(new { IsSuccess = false, Message = MessageResource.DeleteFailed });
            }

            return Json(new { IsSuccess = true, Message = MessageResource.DeleteSuccess });
        }

        public ActionResult Preview(int id)
        {
            var widget = FindWidget(id);
            DesignHelper.SetPreview(CurrentSite, new Page());

            return PartialView(widget);
        }

        private Widget FindWidget(int id)
        {
            return DataContext.Widgets.WithSiteId(CurrentSiteId).FirstOrDefault(i => i.Id == id);
        }
    }
}
