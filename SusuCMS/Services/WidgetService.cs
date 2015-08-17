using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using SusuCMS.Data;
using SusuCMS.Data.Infos;
using SusuCMS.Helpers;

namespace SusuCMS.Services
{
    public class WidgetService
    {
        private Site _site;

        public WidgetService(Site site)
        {
            _site = site;
        }

        public IList<WidgetInfo> GetWidgetInfos()
        {
            var list = new List<WidgetInfo>();

            list.AddRange(GetWidgetInfos("~/Widgets", true));
            list.AddRange(GetWidgetInfos(string.Format("~/Sites/{0}/Widgets", _site.Template), false));

            return list;
        }

        private IList<WidgetInfo> GetWidgetInfos(string path, bool isSystem)
        {
            var list = new List<WidgetInfo>();
            var widgetsFolder = HttpContext.Current.Server.MapPath(path);
            if (Directory.Exists(widgetsFolder))
            {
                var folders = new DirectoryInfo(widgetsFolder).GetDirectories();
                foreach (var folder in folders)
                {
                    var info = GetWidgetInfo(folder, isSystem);
                    if (info != null)
                    {
                        list.Add(info);
                    }
                }
            }

            return list;
        }

        private WidgetInfo GetWidgetInfo(DirectoryInfo widgetFolder, bool isSystem)
        {
            var filePath = Path.Combine(widgetFolder.FullName, "Widget.config");
            if (File.Exists(filePath))
            {
                var info = XmlHelper.Deserialize<WidgetInfo>(filePath);
                info.Name = widgetFolder.Name;
                info.IsSystem = isSystem;

                return info;
            }
            else
            {
                throw new Exception(filePath + " not exist.");
            }
        }

        public WidgetInfo GetWidgetInfo(string name, bool isSystem)
        {
            var path = HttpContext.Current.Server.MapPath(GetWidgetFolderPath(name, isSystem));
            var folder = new DirectoryInfo(path);
            var info = GetWidgetInfo(folder, isSystem);
            if (info != null)
            {
                return info;
            }

            return null;
        }

        public string GetWidgetFolderPath(string name, bool isSystem)
        {
            if (isSystem)
            {
                return string.Format("~/Widgets/{0}", name);
            }

            return string.Format("~/Sites/{0}/Widgets/{1}", _site.Template, name);
        }

        public string GetWidgetPath(Widget widget)
        {
            var folder = GetWidgetFolderPath(widget.Name, widget.IsSystem);

            return string.Format("{0}/Widget.cshtml", folder);
        }

        public string GetAdminPath(Widget widget)
        {
            var folder = GetWidgetFolderPath(widget.Name, widget.IsSystem);

            return string.Format("{0}/Admin.cshtml", folder);
        }
    }
}
