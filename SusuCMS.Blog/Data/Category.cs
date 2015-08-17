using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SusuCMS.Data;

namespace SusuCMS.Blog.Data
{
    public class Category : ITreeNode<Category>, ISiteId
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(SusuCMS.MessageResource), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(SusuCMS.DisplayResource), Name = "IsOnline")]
        public bool IsOnline { get; set; }

        public int SiteId { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "ArticleCount")]
        public int ArticleCount { get; set; }

        [Display(Name = "Parent", ResourceType = typeof(DisplayResource))]
        public int? ParentId { get; set; }

        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> Children { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
