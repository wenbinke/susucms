using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using SusuCMS.Data;

namespace SusuCMS.Web
{
    public class FrontControllerBase : Controller
    {
        protected Site CurrentSite;
        protected Page CurrentPage;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            CurrentSite = FrontContext.Current.Site;
            CurrentPage = FrontContext.Current.Page;
        }

        public void UpdatePageModel<TModel>(TModel model, string[] includeProperties) where TModel : class
        {
            this.UpdateModel<TModel>(model, includeProperties);
        }

        public void UpdatePageModel<TModel>(TModel model) where TModel : class
        {
            this.UpdateModel<TModel>(model);
        }

        public void TryUpdatePageModel<TModel>(TModel model) where TModel : class
        {
            this.TryUpdateModel<TModel>(model);
        }

        protected ActionResult SiteNotFound()
        {
            return View("~/Views/Site404.cshtml");
        }

        protected ActionResult PageNotFound()
        {
            Response.StatusCode = 404;

            return View(string.Format("~/Sites/{0}/Views/Shared/404.cshtml", CurrentSite.Template));
        }
    }
}
