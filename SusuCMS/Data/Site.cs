using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace SusuCMS.Data
{
    public class Site
    {
        public Site()
        {
            Pages = new List<Page>();
            Menus = new List<Menu>();
            Widgets = new List<Widget>();
            Labels = new List<SiteLabel>();
        }

        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Template")]
        public string Template { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "Theme")]
        public string Theme { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "CreationTime")]
        public DateTime CreationTime { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "IsOnline")]
        public bool IsOnline { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "CultureName")]
        public string CultureName { get; set; }

        [AllowHtml, Display(ResourceType = typeof(DisplayResource), Name = "AnalyticsCode")]
        public string AnalyticsCode { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "EnableCustomCss")]
        public bool EnableCustomCss { get; set; }

        public bool IsDeleted { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource), Name = "Urls")]
        public string UrlsJson { get; set; }

        [Display(ResourceType = typeof(DisplayResource), Name = "RedirectToMainUrl")]
        public bool RedirectToMainUrl { get; set; }

        // for routing
        [NotMapped]
        public SiteUrl MatchedUrl { get; set; }

        private IList<SiteUrl> _urls;
        public IList<SiteUrl> Urls
        {
            get
            {
                if (_urls == null)
                {
                    if (!String.IsNullOrWhiteSpace(UrlsJson))
                    {
                        _urls = Json.Decode<IList<SiteUrl>>(UrlsJson);
                    }
                    else
                    {
                        _urls = new List<SiteUrl>();
                    }
                }

                return _urls;
            }
        }

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

        public virtual ICollection<SiteData> Data { get; set; }

        public virtual ICollection<Page> Pages { get; set; }

        public virtual ICollection<Widget> Widgets { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }

        public virtual ICollection<SiteLabel> Labels { get; set; }
    }
}
