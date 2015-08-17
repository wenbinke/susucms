using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using SusuCMS.Data;
using SusuCMS.Data.Infos;
using SusuCMS.Helpers;
using SusuCMS.Services;
using SusuCMS.Web.Admin;

namespace SusuCMS
{
    public static class SiteExtensions
    {
        public static SiteInfo GetSiteInfo(this Site site)
        {
            return SiteService.GetSiteInfo(site.Template);
        }

        public static string GetDataFolderPath(this Site site)
        {
            return string.Format("~/Cms_Data/{0}", site.Id);
        }

        public static string GetCustomCssPath(this Site site)
        {
            return string.Format("{0}/custom.css", GetDataFolderPath(site));
        }

        public static string GetRobotsPath(this Site site)
        {
            return string.Format("{0}/robots.txt", GetDataFolderPath(site));
        }

        public static string GetFaviconPath(this Site site)
        {
            return string.Format("{0}/favicon.ico", GetDataFolderPath(site));
        }

        public static void EnsureDataFolder(this Site site)
        {
            PathHelper.EnsureFolder(GetAbsolutePath(GetDataFolderPath(site)));
        }

        public static void EnsureCustomCss(this Site site)
        {
            EnsureDataFolder(site);
            PathHelper.EnsureFile(GetAbsolutePath(GetCustomCssPath(site)));
        }

        public static void EnsureRobots(this Site site)
        {
            EnsureDataFolder(site);
            PathHelper.EnsureFile(GetAbsolutePath(GetRobotsPath(site)));
        }

        private static string GetAbsolutePath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        private static JavaScriptSerializer _serializer = new JavaScriptSerializer();
        public static T GetData<T>(this Site site, string moduleName, string key)
        {
            var dictionary = FindSiteData(site, moduleName, key);
            if (dictionary != null)
            {
                return _serializer.Deserialize<T>(dictionary.Value);
            }

            return default(T);
        }

        public static void SetData<T>(this Site site, string moduleName, string key, T value)
        {
            var dictionary = FindSiteData(site, moduleName, key);
            if (dictionary != null)
            {
                dictionary.Value = _serializer.Serialize(value);
            }
            else
            {
                site.Data.Add(new SiteData
                {
                    ModuleName = moduleName,
                    Value = _serializer.Serialize(value),
                    Key = key
                });
            }
        }

        private static SiteData FindSiteData(Site site, string module, string key)
        {
            return site.Data.FirstOrDefault(i => i.ModuleName == module && i.Key == key);
        }

        public static MailSetting GetMailSetting(this Site site)
        {
            return site.GetData<MailSetting>(AdminAreaRegistration.ModuleName, "MailSetting");
        }

        public static void SetMailSetting(this Site site, MailSetting setting)
        {
            site.SetData(AdminAreaRegistration.ModuleName, "MailSetting", setting);
        }

        public static UserSetting GetUserSetting(this Site site)
        {
            return site.GetData<UserSetting>(AdminAreaRegistration.ModuleName, "UserSetting");
        }

        public static ThemeInfo GetThemeInfo(this Site site)
        {
            return new ThemeService(site).GetThemeInfo(site.Theme);
        }

        public static void SetUserSetting(this Site site, UserSetting setting)
        {
            site.SetData(AdminAreaRegistration.ModuleName, "UserSetting", setting);
        }

        public static IList<string> GetTemplateNames(this Site site)
        {
            var folder = string.Format("~/Sites/{0}/Views/Templates", site.Template);
            var info = new DirectoryInfo(GetAbsolutePath(folder));

            return info.GetFiles().Select(i => i.Name.Substring(0, i.Name.LastIndexOf('.'))).ToList();  // trim end .cshtml
        }

        public static XElement ToXElement(this Site site)
        {
            return new XElement("Site",
                new XElement("Id", site.Id),
                new XElement("Name", site.Name),
                new XElement("CreationTime", site.CreationTime),
                new XElement("Theme", site.Theme),
                new XElement("IsOnline", site.IsOnline),
                new XElement("CultureName", site.CultureName),
                new XElement("AnalyticsCode", site.AnalyticsCode),
                new XElement("Template", site.Template),
                new XElement("EnableCustomCss", site.EnableCustomCss),
                new XElement("UrlsJson", site.UrlsJson)
            );
        }
    }
}
