using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Telerik.Web.Mvc.UI;

namespace SusuCMS.Web.Admin.Controllers
{
    [Authorize]
    public class ImageBrowserController : EditorFileBrowserController
    {
        private const string contentFolderRoot = "~/Cms_Data";
        private const string prettyName = "Images";

        public override string[] ContentPaths
        {
            get
            {
                return new[] { CreateSiteFolder() };
            }
        }

        private int CurrentSiteId
        {
            get
            {
                var values = ControllerContext.RouteData.Values;
                if (values.ContainsKey("siteId"))
                {
                    return Convert.ToInt32(values["siteId"]);
                }

                return 0;
            }
        }

        private string CreateSiteFolder()
        {
            var virtualPath = Path.Combine(contentFolderRoot, CurrentSiteId.ToString(), prettyName);
            var path = Server.MapPath(virtualPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return virtualPath;
        }
    }
}
