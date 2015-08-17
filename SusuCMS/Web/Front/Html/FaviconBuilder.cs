using System.IO;
using System.Web.Mvc;

namespace SusuCMS.Web.Front.Html
{
    public class FaviconBuilder
    {
        private FrontContext _frontContext;
        private HtmlHelper _htmlHelper;

        public FaviconBuilder(FrontContext frontContext, HtmlHelper htmlHelper)
        {
            _frontContext = frontContext;
            _htmlHelper = htmlHelper;
        }

        public MvcHtmlString GetHtml()
        {
            var path = _frontContext.Site.GetFaviconPath();
            var viewContext = _htmlHelper.ViewContext;
            if (File.Exists(viewContext.HttpContext.Server.MapPath(path)))
            {
                var urlHelper = new UrlHelper(viewContext.RequestContext);
                var html = string.Format(@"<link href=""{0}"" type=""image/ico"" rel=""shortcut icon"" />", urlHelper.Content(path));

                return MvcHtmlString.Create(html);
            }

            return MvcHtmlString.Create(string.Empty);
        }
    }
}
