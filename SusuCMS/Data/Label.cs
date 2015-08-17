using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SusuCMS.Data
{
    public abstract class Label
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Key")]
        public string Key { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "Value")]
        public string Value { get; set; }
    }
}
