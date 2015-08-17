using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using SusuCMS.Services;

namespace SusuCMS.Web.Front.Html
{
    public class ThemeBuilder
    {
        private HtmlHelper _htmlHelper;
        private FrontContext _frontContext;

        public ThemeBuilder(FrontContext frontContext, HtmlHelper htmlHelper)
        {
            _frontContext = frontContext;
            _htmlHelper = htmlHelper;
        }

        public IHtmlString GetHtml()
        {
            var service = new ThemeService(_frontContext.Site);
            return service.GetHtml(_htmlHelper);
        }
    }
}
