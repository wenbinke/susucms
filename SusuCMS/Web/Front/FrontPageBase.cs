using System.Web;
using System.Web.Mvc;
using SusuCMS.Data;
using SusuCMS.Services;

namespace SusuCMS.Web.Front
{
    public abstract class FrontPageBase<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public Site CurrentSite { get; private set; }
        public Page CurrentPage { get; private set; }
        public DataContext DataContext { get; private set; }
        public FrontHtmlHelper FrontHtml { get; private set; }
        public FrontUrlHelper FrontUrl { get; private set; }
        public FrontControllerBase FrontController { get; private set; }
        public SusuCMS.Web.Front.Validation.ValidationHelper Validation { get; private set; }

        public override void InitHelpers()
        {
            base.InitHelpers();

            var context = FrontContext.Current;
            if (context != null)
            {
                CurrentSite = context.Site;

                CurrentPage = context.Page;
                DataContext = context.DataContext;
                FrontHtml = new FrontHtmlHelper(Html, context);
                FrontUrl = new FrontUrlHelper(context);
            }

            FrontController = ViewContext.Controller as FrontControllerBase;
            Validation = new SusuCMS.Web.Front.Validation.ValidationHelper(ViewContext.HttpContext, ViewData.ModelState);
        }

        public IHtmlString SiteLabel(string key, bool editableInDesignPage = true)
        {
            var service = new LabelService(DataContext);
            return service.GetSiteLabel(CurrentSite, key, editableInDesignPage);
        }

        public IHtmlString PageLabel(string key, bool editableInDesignPage = true)
        {
            var service = new LabelService(DataContext);
            return service.GetPageLabel(CurrentPage, key, editableInDesignPage);
        }
    }

    public abstract class FrontPageBase : FrontPageBase<dynamic>
    {
    }
}
