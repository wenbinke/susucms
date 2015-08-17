
namespace SusuCMS.Data
{
    public class SiteLabel : Label, ISiteId
    {
        public int SiteId { get; set; }

        public virtual Site Site { get; set; }
    }
}
