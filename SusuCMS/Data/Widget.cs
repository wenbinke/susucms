using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SusuCMS.Data
{
    public class Widget : ISiteId
    {
        public Widget()
        {
            Labels = new List<WidgetLabel>();
        }

        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "DisplayName")]
        public string DisplayName { get; set; }

        [AllowHtml]
        public string DataJson { get; set; }

        public int SiteId { get; set; }

        public bool IsSystem { get; set; }

        public virtual Site Site { get; set; }

        public virtual ICollection<WidgetLabel> Labels { get; set; }

        public dynamic GetData()
        {
            if (!String.IsNullOrWhiteSpace(DataJson))
            {
                return Json.Decode(DataJson);
            }

            return null;
        }
    }
}
