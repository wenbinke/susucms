using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusuCMS.Data.Infos
{
    public class SiteInfo
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<string> ModuleNames { get; set; }
        public List<PageInfo> DefaultPages { get; set; }
        public string DefaultTheme { get; set; }
        public List<MenuInfo> DefaultMenus { get; set; }
    }
}
