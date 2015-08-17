using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc.UI;

namespace SusuCMS.Web.Front.Html
{
    public class ScriptBuilder
    {
        private HtmlHelper _htmlHelper;
        private FrontContext _frontContext;
        private ScriptContext _scriptContext;

        public ScriptBuilder(FrontContext frontContext, HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
            _frontContext = frontContext;

            var viewData = _frontContext.ViewData;
            if (viewData.ScriptContext == null)
            {
                viewData.ScriptContext = new ScriptContext();
            }
            _scriptContext = viewData.ScriptContext;
        }

        public ScriptBuilder Register(string virtualPath)
        {
            var lowerPath = virtualPath.ToLower();
            if (!_scriptContext.ScriptPaths.Contains(lowerPath))
            {
                _scriptContext.ScriptPaths.Add(lowerPath);
            }

            return this;
        }

        public ScriptBuilder jQuery(bool enable)
        {
            _scriptContext.jQueryEnabled = enable;
            return this;
        }

        public ScriptBuilder jQueryValidation(bool enable)
        {
            _scriptContext.jQueryValidationEnabled = enable;
            return this;
        }

        public ScriptBuilder Combined(bool enable)
        {
            _scriptContext.CombinedEnabled = enable;
            return this;
        }

        public ScriptBuilder Compressed(bool enable)
        {
            _scriptContext.CompressedEnabled = enable;
            return this;
        }

        public IHtmlString GetHtml()
        {
            var registrar = _htmlHelper.Telerik().ScriptRegistrar().jQuery(false).jQueryValidation(false);

            if (_scriptContext.jQueryEnabled)
            {
                registrar.DefaultGroup(g => g.Add("jquery.min.js"));
            }

            if (_scriptContext.jQueryValidationEnabled)
            {
                registrar.DefaultGroup(g =>
                    g.Add("jquery.unobtrusive-ajax.min.js")
                        .Add("jquery.validate.min.js")
                        .Add("jquery.validate.unobtrusive.min.js"));
            }

            foreach (var item in _scriptContext.ScriptPaths)
            {
                registrar.DefaultGroup(g => g.Add(item));
            }

            var html = registrar.DefaultGroup(g => g.Combined(_scriptContext.CombinedEnabled)
                .Compress(_scriptContext.CompressedEnabled)).ToHtmlString();

            return new MvcHtmlString(html);
        }

        class ScriptContext
        {
            public ScriptContext()
            {
                ScriptPaths = new List<string>();
            }

            public bool jQueryEnabled { get; set; }
            public bool jQueryValidationEnabled { get; set; }
            public bool CompressedEnabled { get; set; }
            public bool CombinedEnabled { get; set; }
            public List<string> ScriptPaths { get; set; }
        }
    }
}
