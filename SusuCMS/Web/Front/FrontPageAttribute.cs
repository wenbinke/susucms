using System.Web.Mvc;

namespace SusuCMS.Web
{
    public class FrontPageAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);

            var site = FrontContext.Current.Site;
            if (site != null)
            {
                filterContext.RequestContext.HttpContext.Response.Write(site.AnalyticsCode);
            }
        }
    }
}
