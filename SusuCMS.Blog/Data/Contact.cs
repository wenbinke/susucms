using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SusuCMS.Data;

namespace SusuCMS.Blog.Data
{
    public class Contact : ISiteId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public DateTime CreationTime { get; set; }

        public int SiteId { get; set; }
    }
}
