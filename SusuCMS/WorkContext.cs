using System.Web;

namespace SusuCMS
{
    public class WorkContext : WorkContextBase
    {
        private readonly static object _dataContextKey = new object();

        public static DataContext GetDataContext()
        {
            return GetDataContext<DataContext>(_dataContextKey);
        }
    }
}
