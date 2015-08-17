using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SusuCMS
{
    public abstract class WorkContextBase
    {
        protected static T GetDataContext<T>(object key) where T : class
        {
            if (HttpContext.Current.Items.Contains(key))
            {
                return HttpContext.Current.Items[key] as T;
            }

            var context = Activator.CreateInstance<T>();
            HttpContext.Current.Items.Add(key, context);

            return context;
        }
    }
}
