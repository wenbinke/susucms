using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Optimization;

namespace SusuCMS.AppStart
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterCss(bundles);
            RegisterJs(bundles);
        }

        private static void RegisterCss(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Areas/Admin/Content/base").Include(
                "~/Areas/Admin/Content/reset.css",
                "~/Areas/Admin/Content/base.css",
                "~/Areas/Admin/Content/form.css",
                "~/Areas/Admin/Content/button.css",
                "~/Areas/Admin/Content/helptip.css"));

            bundles.Add(new StyleBundle("~/Areas/Admin/Content/admin").Include(
                "~/Areas/Admin/Content/admin.css"));

            bundles.Add(new StyleBundle("~/Areas/Admin/Content/design").Include(
                "~/Areas/Admin/Content/design.css"));


            bundles.Add(new StyleBundle("~/Areas/Admin/Content/dialog").Include(
                "~/Areas/Admin/Content/dialog.css"));

            bundles.Add(new StyleBundle("~/Content/telerik").Include(
                "~/Content/telerik.common.css",
                "~/Content/telerik.office2010blue.css",
                "~/Content/telerik.rtl.css"));

            bundles.Add(new StyleBundle("~/Content/siteNotFound").Include(
                "~/Content/reset.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Areas/Admin/Content/login").Include(
                "~/Areas/Admin/Content/reset.css",
                "~/Areas/Admin/Content/base.css",
                "~/Areas/Admin/Content/button.css",
                "~/Areas/Admin/Content/login.css"));
        }

        private static void RegisterJs(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                "~/Scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryui").Include(
                "~/Scripts/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery.treeTable").Include(
                "~/Scripts/jquery.treeTable.js"));

            bundles.Add(new ScriptBundle("~/Scripts/codeMirrorjs").Include(
                "~/Scripts/codeMirror/codemirror.js"));

            bundles.Add(new ScriptBundle("~/Scripts/telerik").Include(
                "~/Scripts/telerik.all.js"));

            bundles.Add(new ScriptBundle("~/Scripts/validate").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/design").Include(
                "~/Scripts/sc/design/designer.js",
                "~/Scripts/sc/design/label.js",
                "~/Scripts/sc/design/page.js",
                "~/Scripts/sc/design/theme.js",
                "~/Scripts/sc/design/toolbar.js",
                "~/Scripts/sc/design/widget.js"));

            bundles.Add(new ScriptBundle("~/Scripts/admin").Include(
                "~/Scripts/jquery.poshytip.min.js",
                "~/Scripts/knockout-2.1.0.js",
                "~/Scripts/jquery.tmpl.min.js",
                "~/Scripts/json2.js",
                "~/Scripts/jquery.sform.js",
                "~/Scripts/sc/core.js",
                "~/Scripts/sc/string.js",
                "~/Scripts/sc/ext/validator.js",
                "~/Scripts/sc/ui/form.js",
                "~/Scripts/sc/ui/message.js",
                "~/Scripts/sc/ui/telerik.js",
                "~/Scripts/admin.js"));
        }
    }
}
