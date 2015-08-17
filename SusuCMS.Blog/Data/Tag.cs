using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusuCMS.Data;

namespace SusuCMS.Blog.Data
{
    public class Tag : ISiteId
    {
        public Tag()
        {
            Articles = new List<Article>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int SiteId { get; set; }

        public int ArticleCount { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
