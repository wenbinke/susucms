using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SusuCMS.Blog.Data
{
    [NotMapped]
    public class PermalinkSetting
    {
        [Required(ErrorMessageResourceType = typeof(SusuCMS.MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "ArticleUrl")]
        public string ArticleUrl { get; set; }

        [Required(ErrorMessageResourceType = typeof(SusuCMS.MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "CategoryUrl")]
        public string CategoryUrl { get; set; }

        [Required(ErrorMessageResourceType = typeof(SusuCMS.MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "TagUrl")]
        public string TagUrl { get; set; }

        [Required(ErrorMessageResourceType = typeof(SusuCMS.MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "ArchiveUrl")]
        public string ArchiveUrl { get; set; }
    }
}
