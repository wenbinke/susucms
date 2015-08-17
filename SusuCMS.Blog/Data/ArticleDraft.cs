using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusuCMS.Data;

namespace SusuCMS.Blog.Data
{
    public class ArticleDraft : ISiteId
    {
        public int Id { get; set; }

        public int SiteId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Time { get; set; }
    }
}
