using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using SusuCMS.Data;
using SusuCMS.Data.Infos;
using SusuCMS.Helpers;
using Telerik.Web.Mvc.UI;

namespace SusuCMS.Services
{
    public class ThemeService
    {
        private Site _site;

        public ThemeService(Site site)
        {
            _site = site;
        }

        public IList<ThemeInfo> GetThemeInfos()
        {
            var rootPath = HttpContext.Current.Server.MapPath(string.Format("~/Sites/{0}/Themes", _site.Template));

            var list = new List<ThemeInfo>();
            foreach (var folder in Directory.EnumerateDirectories(rootPath))
            {
                list.Add(GetThemeInfo(Path.GetFileName(folder)));
            }

            return list;
        }

        public ThemeInfo GetThemeInfo(string themeName)
        {
            var themePath = GetThemePath(themeName);
            var configFilePath = string.Format("{0}/Theme.config", themePath);

            var info = XmlHelper.Deserialize<ThemeInfo>(HttpContext.Current.Server.MapPath(configFilePath));
            info.Name = themeName;

            var preview = string.Format("{0}/preview.png", themePath);
            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(preview)))
            {
                info.PreviewImagePath = preview;
            }
            else
            {
                info.PreviewImagePath = "~/Areas/Admin/Content/images/theme-preview.png";
            }

            return info;
        }

        public string GetThemePath(string themeName)
        {
            return string.Format("~/Sites/{0}/Themes/{1}", _site.Template, themeName);
        }

        public IList<string> GetCssFiles(string themeName)
        {
            var themeInfo = GetThemeInfo(themeName);
            return themeInfo.CssFiles;
        }

        public IHtmlString GetHtml(HtmlHelper htmlHelper)
        {
            return GetHtml(htmlHelper, _site.Theme);
        }

        public IHtmlString GetHtml(HtmlHelper htmlHelper, string themeName)
        {
            var builder = htmlHelper.Telerik().StyleSheetRegistrar();

            var themeInfo = GetThemeInfo(themeName);
            if (themeInfo.EnableCommonCss)
            {
                builder.DefaultGroup(i => i.Add("~/Content/common.css"));
            }

            var themePath = GetThemePath(themeName);
            foreach (var file in themeInfo.CssFiles)
            {
                builder.DefaultGroup(i => i.Add(string.Format("{0}/{1}", themePath, file)));
            }

            if (_site.EnableCustomCss)
            {
                var customCssPath = _site.GetCustomCssPath();
                if (File.Exists(HttpContext.Current.Server.MapPath(customCssPath)))
                {
                    builder.DefaultGroup(i => i.Add(customCssPath));
                }
            }

            builder.DefaultGroup(i => i.Combined(true).Compress(true));

            return new MvcHtmlString(builder.ToHtmlString());
        }
    }
}
