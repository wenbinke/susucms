using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SusuCMS.Data.Enums;



namespace SusuCMS.Web.Admin.Controllers
{
    [AdminAuthorize(Permission.SystemLogs)]
    public class SystemLogController : AdminControllerBase
    {
        public ActionResult Index()
        {
            return View(DataContext.SystemLogs.OrderByDescending(i => i.CreationTime));
        }

        public ActionResult Clear()
        {
            try
            {
                foreach (var item in DataContext.SystemLogs)
                {
                    DataContext.Delete(item);
                }

                SaveChanges();
                ShowSuccess(MessageResource.ClearLogsSuccess);
            }
            catch (Exception e)
            {
                LogError(e.ToString());
                ShowError(MessageResource.ClearLogsFailed);
            }

            return RedirectToIndex();
        }
    }
}
