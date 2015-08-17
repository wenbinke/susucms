using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusuCMS;
using System.Web;
using System.Web.Caching;
using System.Xml.Linq;
using System.Threading;
using SusuCMS.Data;

namespace SusuCMS.Web.Admin
{
    public abstract class WidgetAdminPageBase : AdminPageBase<Widget>
    {
        public string Localize(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }

            var cultureName = Thread.CurrentThread.CurrentUICulture.Name;
            var localizeCacheKey = string.Format("{0}_Resources.{1}", Model.Name, cultureName);
            var dictionary = HttpRuntime.Cache[localizeCacheKey] as IDictionary<string, string>;
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, string>();
                var path = GetResourcePath(cultureName);
                if (System.IO.File.Exists(path))
                {
                    var doc = XDocument.Load(path);
                    foreach (var item in doc.Descendants("data"))
                    {
                        dictionary.Add(item.Attribute("name").Value, item.Element("value").Value);
                    }

                    // make cache
                    var dependency = new CacheDependency(path);
                    HttpRuntime.Cache.Insert(localizeCacheKey, dictionary, dependency, Cache.NoAbsoluteExpiration, TimeSpan.Zero);
                }
            }

            return dictionary.ContainsKey(key) ? dictionary[key] : key;
        }

        private string GetResourcePath(string cultureName)
        {
            string path;
            if (Model.IsSystem)
            {
                path = string.Format("~/Widgets/{0}/Resources/Admin.{1}.resx", Model.Name, cultureName);
            }
            else
            {
                path = string.Format("~/Sites/{0}/Widgets/{1}/Resources/Admin.{2}.resx", Model.Site.Template, Model.Name, cultureName);
            }

            return HttpContext.Current.Server.MapPath(path);
        }
    }
}
