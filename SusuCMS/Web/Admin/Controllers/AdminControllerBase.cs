using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using SusuCMS.Data;
using SusuCMS.Data.Enums;
using SusuCMS.Helpers;
using SusuCMS.Services;


namespace SusuCMS.Web.Admin.Controllers
{
    public class AdminControllerBase : Controller
    {
        private DataContext _dataContext;
        public DataContext DataContext
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = WorkContext.GetDataContext();
                }

                return _dataContext;
            }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            var adminContext = new AdminContext
            {
                CurrentUser = DataContext.AdminUsers.SingleOrDefault(i => i.UserName == HttpContext.User.Identity.Name)
            };

            if (CurrentSiteId != 0)
            {
                adminContext.CurrentSite = DataContext.Sites.FirstOrDefault(i => i.Id == CurrentSiteId);
            }

            AdminContext.Current = adminContext;

            InitAdminPanelLanguage();
        }

        private void InitAdminPanelLanguage()
        {
            if (Request.Cookies["AdminPanelLanguage"] != null)
            {
                var cultureName = Request.Cookies["AdminPanelLanguage"].Value;

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            }
            else
            {
                var systemInfo = SystemService.GetSystemInfo();
                if (systemInfo != null)
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(systemInfo.DefaultCultureName);
                }
            }
        }

        protected virtual void SaveChanges()
        {
            DataContext.SaveChanges();
        }

        private AdminUser _currentUser;
        public AdminUser CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = AdminContext.Current.CurrentUser;
                }

                return _currentUser;
            }
        }

        public int CurrentSiteId
        {
            get
            {
                var values = ControllerContext.RouteData.Values;
                if (values.ContainsKey("siteId"))
                {
                    return Convert.ToInt32(values["siteId"]);
                }

                return 0;
            }
        }

        private Site _currentSite;
        public Site CurrentSite
        {
            get
            {
                if (_currentSite == null)
                {
                    _currentSite = AdminContext.Current.CurrentSite;
                }

                return _currentSite;
            }
        }

        protected void LogError(string message)
        {
            var url = "http://" + Request.Headers["host"] + Request.RawUrl;

            LogHelper.AddSystemLog(LogType.Error, "Url: " + url + "<br/>" + message);
        }

        protected ActionResult RedirectToIndex()
        {
            var returnUrl = Request["returnUrl"];
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index");
        }

        protected new ActionResult HttpNotFound()
        {
            return RedirectToAction("NotFound", "Shared", new { area = "admin" });
        }

        protected void ShowSuccess(string message)
        {
            TempData["Message"] = message;
        }

        protected void ShowError(string message)
        {
            TempData["ErrorMessage"] = message;
        }
    }
}
