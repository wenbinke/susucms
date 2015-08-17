using System.Web;

namespace SusuCMS.Blog
{
    public class BlogWorkContext : WorkContextBase
    {
        private readonly static object _dataContextKey = new object();

        public static BlogDataContext GetDataContext()
        {
            return GetDataContext<BlogDataContext>(_dataContextKey);
        }
    }
}
