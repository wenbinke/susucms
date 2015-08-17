
namespace SusuCMS.Data
{
    public class PageWidget
    {
        public int PageId { get; set; }

        public int WidgetId { get; set; }

        public string ZoneName { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Page Page { get; set; }

        public virtual Widget Widget { get; set; }
    }
}
