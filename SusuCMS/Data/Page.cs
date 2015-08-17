using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace SusuCMS.Data
{
    public class Page : ITreeNode<Page>, ISiteId
    {
        public Page()
        {
            Labels = new List<PageLabel>();
            Widgets = new List<PageWidget>();
        }

        public int Id { get; set; }

        public int SiteId { get; set; }

        public int? ParentId { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Template")]
        public string Template { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "HtmlTitle")]
        public string HtmlTitle { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "MetaFields")]
        public string MetaFieldsJson { get; set; }

        private IList<MetaField> _metaFields;
        public IList<MetaField> MetaFields
        {
            get
            {
                if (_metaFields == null)
                {
                    if (!String.IsNullOrWhiteSpace(MetaFieldsJson))
                    {
                        _metaFields = Json.Decode<IList<MetaField>>(MetaFieldsJson);
                    }
                    else
                    {
                        _metaFields = new List<MetaField>();
                    }
                }

                return _metaFields;
            }
        }

        public DateTime CreationTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Url")]
        //[RegularExpression("^([a-zA-Z0-9_\\+-]+/{0,1})*$",
        //    ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "InvalidUrl")]
        public string Url { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "IsOnline")]
        public bool IsOnline { get; set; }

        [NotMapped]
        public bool IsHomePage
        {
            get
            {
                return ParentId == null;
            }
        }

        public virtual Site Site { get; set; }

        public virtual Page Parent { get; set; }

        public virtual ICollection<Page> Children { get; set; }

        public virtual ICollection<PageWidget> Widgets { get; set; }

        public virtual ICollection<PageLabel> Labels { get; set; }
    }
}
