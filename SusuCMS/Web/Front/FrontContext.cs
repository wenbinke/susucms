using System.Web;
using SusuCMS.Data;
using SusuCMS.Data.Enums;

namespace SusuCMS.Web
{
    public class FrontContext
    {
        private static readonly object _frontContextKey = new object();
        public static FrontContext Current
        {
            get
            {
                var items = HttpContext.Current.Items;
                if (items.Contains(_frontContextKey))
                {
                    return items[_frontContextKey] as FrontContext;
                }

                return null;
            }
            set
            {
                var items = HttpContext.Current.Items;
                items[_frontContextKey] = value;
            }
        }

        private string _templatePath;
        public string TemplatePath
        {
            get
            {
                if (_templatePath == null)
                {
                    _templatePath = string.Format("~/Sites/{0}/Views/Templates/{1}.cshtml", Site.Template, Page.Template);
                }

                return _templatePath;
            }
        }

        public bool IsPreviewMode { get; set; }
        public WidgetViewType WidgetViewType { get; set; }
        internal dynamic ViewData { get; set; }
        public Site Site { get; set; }
        public Page Page { get; set; }
        public DataContext DataContext { get; set; }
    }
}
