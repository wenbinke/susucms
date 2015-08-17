using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SusuCMS.Data;

namespace SusuCMS
{
    public static class SiteIdExtensions
    {
        public static IQueryable<T> WithSiteId<T>(this IQueryable<T> list, int siteId) where T : class, ISiteId
        {
            return list.Where(i => i.SiteId == siteId);
        }

        public static IQueryable<T> WithSiteId<T>(this DbSet<T> list, int siteId) where T : class, ISiteId
        {
            return list.Where(i => i.SiteId == siteId);
        }

        public static IEnumerable<T> WithSiteId<T>(this IEnumerable<T> list, int siteId) where T : class, ISiteId
        {
            return list.Where(i => i.SiteId == siteId);
        }
    }
}
