using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using SusuCMS.Data;
using SusuCMS.Data.Enums;
using SusuCMS.Web.Front.Html;

namespace SusuCMS.Web
{
    public class FrontHtmlHelper
    {
        private HtmlHelper _htmlHelper;
        private FrontContext _frontContext;
        public ScriptBuilder Script { get; private set; }
        public ThemeBuilder Theme { get; private set; }
        public MetadataBuilder Metadata { get; private set; }
        public FaviconBuilder Favicon { get; private set; }
        public HtmlTitleBuilder HtmlTitle { get; private set; }

        public FrontHtmlHelper(HtmlHelper htmlHelper, FrontContext frontContext)
        {
            _htmlHelper = htmlHelper;
            _frontContext = frontContext;
            Script = new ScriptBuilder(_frontContext, _htmlHelper);
            Theme = new ThemeBuilder(_frontContext, _htmlHelper);
            Metadata = new MetadataBuilder(_frontContext);
            Favicon = new FaviconBuilder(_frontContext, _htmlHelper);
            HtmlTitle = new HtmlTitleBuilder(_frontContext);
        }

        public IHtmlString Shared(string name)
        {
            var path = string.Format("~/Sites/{0}/Views/Shared/{1}.cshtml",
                _frontContext.Site.Template, name);

            return _htmlHelper.Partial(path);
        }

        public IHtmlString HeadContent()
        {
            var builder = new StringBuilder();
            builder.Append(Metadata.GetHtml());
            builder.Append(Favicon.GetHtml());
            builder.Append(Theme.GetHtml());
            builder.Append(Script.GetHtml());

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}
