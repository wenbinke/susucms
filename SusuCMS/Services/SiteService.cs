using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using SusuCMS.Data.Infos;
using SusuCMS.Data;
using SusuCMS.Helpers;

namespace SusuCMS.Services
{
    public class SiteService : ServiceBase<DataContext>
    {
        public SiteService(DataContext dataContext) : base(dataContext) { }

        public static IList<SiteInfo> GetSiteInfos()
        {
            var rootPath = HttpContext.Current.Server.MapPath("~/Sites");

            var list = new List<SiteInfo>();
            foreach (var folder in Directory.EnumerateDirectories(rootPath))
            {
                list.Add(GetSiteInfoByFolderPath(folder));
            }

            return list;
        }

        public static SiteInfo GetSiteInfo(string name)
        {
            var folder = HttpContext.Current.Server.MapPath(string.Format("~/Sites/{0}", name));
            return GetSiteInfoByFolderPath(folder);
        }

        private static SiteInfo GetSiteInfoByFolderPath(string folderPath)
        {
            var configFilePath = Path.Combine(folderPath, "site.config");

            var info = XmlHelper.Deserialize<SiteInfo>(configFilePath);
            info.Name = Path.GetFileName(folderPath);

            return info;
        }

        public void CreateSite(Site site)
        {
            var siteInfo = GetSiteInfo(site.Template);

            var now = DateTime.Now;
            CreateDefaulPages(site, siteInfo, now);
            CreateDefaultMenus(site, siteInfo);

            site.Theme = siteInfo.DefaultTheme;
            site.CreationTime = DateTime.Now;

            DataContext.Sites.Add(site);
            SaveChanges();
        }

        private void CreateDefaulPages(Site site, SiteInfo siteInfo, DateTime now)
        {
            var homePageInfo = siteInfo.DefaultPages.First(i => i.IsHomePage);
            var homePage = new Page
            {
                Name = homePageInfo.Name,
                Template = homePageInfo.Template,
                Url = homePageInfo.Url,
                CreationTime = now,
                IsOnline = true
            };
            site.Pages.Add(homePage);

            foreach (var page in siteInfo.DefaultPages.Where(i => !i.IsHomePage))
            {
                site.Pages.Add(new Page
                {
                    Name = page.Name,
                    Template = page.Template,
                    Url = page.Url,
                    Parent = homePage,
                    CreationTime = now,
                    IsOnline = true
                });
            }
        }

        private void CreateDefaultMenus(Site site, SiteInfo siteInfo)
        {
            foreach (var item in siteInfo.DefaultMenus)
            {
                site.Menus.Add(new Menu
                    {
                        Name = item.Name,
                        Url = item.Url,
                        IsOnline = true
                    });
            }
        }

        #region Export

        public XDocument Export(Site site, string fileName)
        {
            var xml = new XDocument();
            var root = new XElement("SusuCMS");

            root.Add(site.ToXElement());
            root.Add(ExportList<Menu>("Menus", site.Menus, i => i.ToXElement()));
            root.Add(ExportList<Widget>("Widgets", site.Widgets, i => i.ToXElement()));
            root.Add(ExportList<Page>("Pages", site.Pages, i => i.ToXElement()));
            root.Add(ExportList<SiteLabel>("SiteLabels", site.Labels, i => i.ToXElement()));
            root.Add(ExportList<PageLabel>("PageLabels", site.Pages.SelectMany(i => i.Labels), i => i.ToXElement()));
            root.Add(ExportList<WidgetLabel>("WidgetLabels", site.Widgets.SelectMany(i => i.Labels), i => i.ToXElement()));
            root.Add(ExportList<PageWidget>("PageWidgets", site.Pages.SelectMany(i => i.Widgets), i => i.ToXElement()));

            xml.Add(root);

            return xml;
        }

        private XElement ExportList<T>(string name, IEnumerable<T> list, Func<T, XElement> toXElement) where T : class
        {
            var node = new XElement(name);
            foreach (var item in list)
            {
                node.Add(toXElement(item));
            }

            return node;
        }

        #endregion

        #region Import

        public Site Import(XDocument xml)
        {
            var site = ImportSite(xml);
            ImportMenus(xml, site);
            ImportWidgets(xml, site);
            ImportPages(xml, site);
            ImportSiteLabels(xml, site);
            ImportWidgetLabels(xml, site);
            ImportPageLabels(xml, site);
            ImportPageWidgets(xml, site);

            SaveChanges();

            return site;
        }

        private Site ImportSite(XDocument xml)
        {
            var site = GetXElement(xml, "Site").ToSite();
            DataContext.Sites.Add(site);

            return site;
        }

        private void ImportMenus(XDocument xml, Site site)
        {
            var menuNodes = GetXElement(xml, "Menus").Elements("Menu");
            var list = new List<Menu>();
            foreach (var node in menuNodes)
            {
                list.Add(node.ToMenu());
            }

            ImportTreeList(null, list.Where(i => i.ParentId == null));

            foreach (var item in list)
            {
                site.Menus.Add(item);
            }
        }

        private void ImportTreeList<T>(T parent, IEnumerable<T> list) where T : class, ITreeNode<T>
        {
            foreach (var item in list)
            {
                var children = list.Where(i => i.ParentId == item.Id);
                item.Parent = parent;

                ImportTreeList(item, children);
            }
        }

        private void ImportWidgets(XDocument xml, Site site)
        {
            var widgetNodes = GetXElement(xml, "Widgets").Elements("Widget");
            var list = new List<Widget>();
            foreach (var node in widgetNodes)
            {
                site.Widgets.Add(node.ToWidget());
            }
        }

        private void ImportPages(XDocument xml, Site site)
        {
            var pageNodes = GetXElement(xml, "Pages").Elements("Page");
            var list = new List<Page>();
            foreach (var node in pageNodes)
            {
                list.Add(node.ToPage());
            }

            ImportTreeList(null, list.Where(i => i.ParentId == null));

            foreach (var item in list)
            {
                site.Pages.Add(item);
            }
        }

        private void ImportSiteLabels(XDocument xml, Site site)
        {
            var labelNodes = GetXElement(xml, "SiteLabels").Elements("SiteLabel");
            var list = new List<SiteLabel>();
            foreach (var node in labelNodes)
            {
                var label = node.ToSiteLabel();
                site.Labels.Add(label);
            }
        }

        private void ImportPageLabels(XDocument xml, Site site)
        {
            var labelNodes = GetXElement(xml, "PageLabels").Elements("PageLabel");
            var list = new List<PageLabel>();
            foreach (var node in labelNodes)
            {
                var label = node.ToPageLabel();
                var page = site.Pages.First(i => i.Id == label.PageId);
                page.Labels.Add(label);
            }
        }

        private void ImportWidgetLabels(XDocument xml, Site site)
        {
            var labelNodes = GetXElement(xml, "WidgetLabels").Elements("WidgetLabel");
            var list = new List<WidgetLabel>();
            foreach (var node in labelNodes)
            {
                var label = node.ToWidgetLabel();
                var widget = site.Widgets.First(i => i.Id == label.WidgetId);
                widget.Labels.Add(label);
            }
        }

        private void ImportPageWidgets(XDocument xml, Site site)
        {
            var pageWidgetNodes = GetXElement(xml, "PageWidgets").Elements("PageWidget");
            var list = new List<PageWidget>();
            foreach (var node in pageWidgetNodes)
            {
                var pageWidget = node.ToPageWidget();
                pageWidget.Widget = site.Widgets.First(i => i.Id == pageWidget.WidgetId);
                pageWidget.Page = site.Pages.First(i => i.Id == pageWidget.PageId);

                DataContext.PageWidgets.Add(pageWidget);
            }
        }


        private XElement GetXElement(XDocument xml, string name)
        {
            return xml.Element("SusuCMS").Element(name);
        }

        #endregion
    }
}
