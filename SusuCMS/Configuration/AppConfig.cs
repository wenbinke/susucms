using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusuCMS.Configuration
{
    public static class AppConfig
    {
        public const string DataFolderName = "Cms_Data";

        public const string FaviconLocationFormats = "~/Cms_Data/{0}/favicon.ico";    // {0} -> siteId
        public const string FaviconNotExistImage = "~/Areas/Admin/Content/images/transparent.png";

        public const string ThemeFolderName = "Themes";
    }
}
