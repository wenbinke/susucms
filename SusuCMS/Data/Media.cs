using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SusuCMS.Data
{
    [NotMapped]
    public class Media
    {
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "CreationTime")]
        public DateTime CreationTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "LastWriteTime")]
        public DateTime LastWriteTime { get; set; }

        public string Type { get; set; }

        public bool IsFolder { get; set; }

        public long Size { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Path")]
        public string Path { get; set; }
    }
}
