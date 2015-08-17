using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SusuCMS.Data.Enums;
using SusuCMS.Data;



namespace SusuCMS.Web.Admin.Controllers
{
    [AdminAuthorize(Permission.Pages)]
    public class LabelController : AdminControllerBase
    {
        public ActionResult Index(string labelType, int? relativeId)
        {
            var type = LabelType.SiteLabel;
            Enum.TryParse<LabelType>(labelType, out type);

            ViewBag.LabelType = type;

            IQueryable<Label> list = null;
            switch (type)
            {
                case LabelType.SiteLabel:
                    list = DataContext.SiteLabels.WithSiteId(CurrentSiteId);
                    break;
                case LabelType.PageLabel:
                    if (relativeId.HasValue)
                    {
                        list = DataContext.PageLabels.Where(i => i.Page.SiteId == CurrentSiteId && i.PageId == relativeId.Value);
                    }
                    else
                    {
                        list = DataContext.PageLabels.Where(i => i.Page.SiteId == CurrentSiteId);
                    }
                    break;
                case LabelType.WidgetLabel:
                    if (relativeId.HasValue)
                    {
                        list = DataContext.WidgetLabels.Where(i => i.Widget.SiteId == CurrentSiteId && i.WidgetId == relativeId.Value);
                    }
                    else
                    {
                        list = DataContext.WidgetLabels.Where(i => i.Widget.SiteId == CurrentSiteId);
                    }
                    break;
            }

            return View(list);
        }

        public ActionResult Edit(LabelType labelType, int id)
        {
            var label = FindLabel(labelType, id);
            if (label == null)
            {
                return HttpNotFound();
            }

            return View(label);
        }

        [HttpPost, ValidateInput(false), ValidateAntiForgeryToken]
        public ActionResult Edit(LabelType labelType, int id, FormCollection collection)
        {
            var label = FindLabel(labelType, id);
            TryUpdateModel(label, new[] { "Value" });

            if (ModelState.IsValid)
            {
                try
                {
                    SaveChanges();
                }
                catch
                {
                    return Json(new { IsSuccess = false, Message = MessageResource.UpdateFailed });
                }
            }

            return Json(new { IsSuccess = true, Message = MessageResource.UpdateSuccess });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(LabelType labelType2, int id)
        {
            try
            {
                var label = FindLabel(labelType2, id);
                DataContext.Delete(label);
                SaveChanges();

                ShowSuccess(MessageResource.DeleteSuccess);
            }
            catch
            {
                ShowError(MessageResource.DeleteFailed);
            }

            return RedirectToIndex();
        }

        private Label FindLabel(LabelType labelType, int id)
        {
            Label label = null;
            switch (labelType)
            {
                case LabelType.SiteLabel:
                    label = DataContext.SiteLabels.WithSiteId(CurrentSiteId).FirstOrDefault(i => i.Id == id);
                    break;
                case LabelType.PageLabel:
                    label = DataContext.PageLabels.FirstOrDefault(i => i.Id == id && i.Page.SiteId == CurrentSiteId);
                    break;
                case LabelType.WidgetLabel:
                    label = DataContext.WidgetLabels.FirstOrDefault(i => i.Id == id && i.Widget.SiteId == CurrentSiteId);
                    break;
            }

            return label;
        }
    }
}
