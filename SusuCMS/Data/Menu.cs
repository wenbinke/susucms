using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SusuCMS.Data
{
    public class Menu : ITreeNode<Menu>, ISiteId, ISortable
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        public string Url { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "DisplayOrder")]
        public int DisplayOrder { get; set; }

        public int SiteId { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "ParentMenuItem")]
        public int? ParentId { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "OpenInNewWindow")]
        public bool OpenInNewWindow { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "IsOnline")]
        public bool IsOnline { get; set; }

        public virtual Site Site { get; set; }

        public virtual Menu Parent { get; set; }

        public virtual ICollection<Menu> Children { get; set; }
    }
}
