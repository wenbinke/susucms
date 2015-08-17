using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Optimization;

namespace SusuCMS.Blog.AppStart
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterCss(bundles);
        }

        private static void RegisterCss(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Areas/Blog/Content/admin").Include(
                "~/Areas/Blog/Content/admin.css"));

        }
    }
}
