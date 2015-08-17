using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using SusuCMS.Web;
using SusuCMS.Data;
using SusuCMS.Data.Enums;

namespace SusuCMS.Services
{
    public class LabelService : ServiceBase<DataContext>
    {
        public LabelService(DataContext dataContext) : base(dataContext) { }

        private static readonly object _staticKey = new object();

        public IHtmlString GetSiteLabel(Site site, string key, bool editableInDesignPage)
        {
            var label = site.Labels.FirstOrDefault(i => i.Key == key);
            if (label == null)
            {
                lock (_staticKey)
                {
                    label = new SiteLabel
                    {
                        Key = key,
                        Value = key
                    };
                    site.Labels.Add(label);
                    SaveChanges();
                }
            }

            return Wrapper(LabelType.SiteLabel, label, editableInDesignPage);
        }

        public IHtmlString GetPageLabel(Page page, string key, bool editableInDesignPage)
        {
            var label = page.Labels.FirstOrDefault(i => i.Key == key);
            if (label == null)
            {
                lock (_staticKey)
                {
                    label = new PageLabel
                    {
                        Key = key,
                        Value = key
                    };
                    page.Labels.Add(label);
                    SaveChanges();
                }
            }

            return Wrapper(LabelType.PageLabel, label, editableInDesignPage);
        }

        public IHtmlString GetWidgetLabel(Widget widget, string key, bool editableInDesignPage)
        {
            var label = widget.Labels.FirstOrDefault(i => i.Key == key);
            if (label == null)
            {
                lock (_staticKey)
                {
                    label = new WidgetLabel
                    {
                        Key = key,
                        Value = key
                    };
                    widget.Labels.Add(label);
                    SaveChanges();
                }
            }

            return Wrapper(LabelType.WidgetLabel, label, editableInDesignPage);
        }

        private static IHtmlString Wrapper(LabelType labelType, Label label, bool editableInDesignPage)
        {
            var isEditMode = FrontContext.Current.IsPreviewMode;
            if (isEditMode && editableInDesignPage)
            {
                return new MvcHtmlString(string.Format(@"<var data-label-type=""{0}"" data-label-id=""{1}"" class=""s-label"">{2}</var>", labelType, label.Id, label.Value));
            }
            else
            {
                return new MvcHtmlString(label.Value);
            }
        }
    }
}
