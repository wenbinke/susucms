using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusuCMS.Web;
using SusuCMS.Data;

namespace SusuCMS.Helpers
{
    public static class DesignHelper
    {
        public static void SetPreview(Site site, Page page)
        {
            site.MatchedUrl = new SiteUrl();
            FrontContext.Current = new FrontContext
            {
                Site = site,
                Page = page,
                IsPreviewMode = true
            };
        }
    }
}
