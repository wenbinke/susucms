using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SusuCMS.Data;

namespace SusuCMS.Web.Admin
{
    public class AdminContext
    {
        private static readonly object _adminContextKey = new object();
        public static AdminContext Current
        {
            get
            {
                if (HttpContext.Current.Items.Contains(_adminContextKey))
                {
                    return HttpContext.Current.Items[_adminContextKey] as AdminContext;
                }

                return null;
            }
            set
            {
                if (HttpContext.Current.Items.Contains(_adminContextKey))
                {
                    HttpContext.Current.Items[_adminContextKey] = value;
                }
                else
                {
                    HttpContext.Current.Items.Add(_adminContextKey, value);
                }
            }
        }

        public AdminUser CurrentUser { get; set; }
        public Site CurrentSite { get; set; }
    }
}
