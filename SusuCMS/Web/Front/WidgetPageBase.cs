using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SusuCMS.Data;
using SusuCMS.Services;

namespace SusuCMS.Web.Front
{
    public abstract class WidgetPageBase<TModel> : FrontPageBase<TModel>
    {
        public Widget CurrentWidget { get; private set; }

        public IHtmlString WidgetLabel(string key, bool editableInDesignPage = true)
        {
            var serivce = new LabelService(DataContext);
            return serivce.GetWidgetLabel(CurrentWidget, key, editableInDesignPage);
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
            CurrentWidget = ViewBag.CurrentWidget as Widget;
        }
    }

    public abstract class WidgetPageBase : WidgetPageBase<dynamic>
    {
    }
}
