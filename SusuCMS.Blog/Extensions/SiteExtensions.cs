using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusuCMS.Blog.Web;
using SusuCMS.Data;
using SusuCMS.Blog.Data;

namespace SusuCMS.Blog
{
    public static class SiteExtensions
    {
        public static PermalinkSetting GetPermalinkSetting(this Site site)
        {
            var setting = site.GetData<PermalinkSetting>(BlogAreaRegistration.ModuleName, "PermalinkSetting");
            if (setting == null)
            {
                // return default setting
                setting = new PermalinkSetting
                {
                    ArticleUrl = "article/{ArticleId}/{Slug}",
                    CategoryUrl = "category/{CategoryId}/{Slug}",
                    TagUrl = "tag/{TagId}/{Slug}",
                    ArchiveUrl = "archive{Year}/{Month}"
                };
            }

            return setting;
        }

        public static void SetPermalinkSetting(this Site site, PermalinkSetting setting)
        {
            site.SetData(BlogAreaRegistration.ModuleName, "PermalinkSetting", setting);
        }
    }
}
