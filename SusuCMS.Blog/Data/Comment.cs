using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SusuCMS.Data;

namespace SusuCMS.Blog.Data
{
    public class Comment : ITreeNode<Comment>, ISiteId
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }

        public DateTime CreationTime { get; set; }

        public int UserId { get; set; }

        public string IP { get; set; }

        public string Author { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "Email")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "InvalidEmail")]
        public string Email { get; set; }

        public string Url { get; set; }

        public int Status { get; set; }

        public string UserAgent { get; set; }

        public int SiteId { get; set; }

        public virtual Article Article { get; set; }

        public virtual Comment Parent { get; set; }

        public virtual ICollection<Comment> Children { get; set; }
    }
}
