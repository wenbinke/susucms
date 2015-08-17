using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Helpers;
using SusuCMS.Data;


namespace SusuCMS.Blog.Data
{
    public class Article : ISiteId
    {
        public Article()
        {
            Tags = new List<Tag>();
        }

        public int Id { get; set; }

        [Display(ResourceType = typeof(SusuCMS.DisplayResource), Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(SusuCMS.MessageResource), ErrorMessageResourceName = "Required")]
        public string Title { get; set; }

        [AllowHtml]
        [Display(ResourceType = typeof(SusuCMS.DisplayResource), Name = "Content")]
        public string Content { get; set; }

        [Display(ResourceType = typeof(SusuCMS.DisplayResource), Name = "CreationTime")]
        public DateTime CreationTime { get; set; }

        [Display(ResourceType = typeof(SusuCMS.DisplayResource), Name = "UpdateTime")]
        public DateTime UpdateTime { get; set; }

        [Display(ResourceType = typeof(SusuCMS.DisplayResource), Name = "IsOnline")]
        public bool IsOnline { get; set; }

        public int SiteId { get; set; }

        public int Hits { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "CommentCount")]
        public int CommentCount { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "EnableComment")]
        public bool EnableComment { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "Author")]
        public string Author { get; set; }

        [Required(ErrorMessageResourceType = typeof(SusuCMS.MessageResource), ErrorMessageResourceName = "Required")]
        public string Slug { get; set; }

        [Display(ResourceType = typeof(SusuCMS.DisplayResource), Name = "MetaFields")]
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

        [Display(ResourceType = typeof(DisplayResource), Name = "Category")]
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "Tags")]
        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
