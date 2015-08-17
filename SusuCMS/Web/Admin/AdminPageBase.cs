using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SusuCMS.Web.Admin;
using SusuCMS.Web.Front.Validation;
using SusuCMS.Data;
using SusuCMS.Web.Admin.Controllers;

namespace SusuCMS.Web.Admin
{
    public abstract class AdminPageBase<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public Site CurrentSite { get; private set; }
        public AdminUser CurrentUser { get; private set; }
        public DataContext DataContext { get; private set; }
        public ValidationHelper Validation { get; private set; }
        public int CurrentSiteId { get; private set; }

        public string GetControllerName()
        {
            return ViewContext.RouteData.GetRequiredString("controller").ToLower();
        }

        public string GetActionName()
        {
            return ViewContext.RouteData.GetRequiredString("action").ToLower();
        }

        public string GetAreaName()
        {
            return ViewContext.RouteData.DataTokens["area"].ToString().ToLower();
        }

        public override void InitHelpers()
        {
            base.InitHelpers();

            var controller = ViewContext.Controller as AdminControllerBase;
            if (controller != null)
            {
                CurrentSite = controller.CurrentSite;
                CurrentSiteId = controller.CurrentSiteId;
                CurrentUser = controller.CurrentUser;
                DataContext = WorkContext.GetDataContext();
            }

            Validation = new ValidationHelper(ViewContext.HttpContext, ViewData.ModelState);
        }
    }

    public abstract class AdminPageBase : AdminPageBase<dynamic>
    {
    }
}
