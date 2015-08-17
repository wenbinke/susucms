using System.Web.Mvc;
using SusuCMS.Web;
using SusuCMS.Web.Admin;
using SusuCMS.Web.Admin.Controllers;

namespace SusuCMS.Blog.Web
{
    [Authorize]
    public class BlogControllerBase : AdminControllerBase
    {
        private BlogDataContext _dataContext;
        public BlogDataContext BlogDataContext
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = BlogWorkContext.GetDataContext();
                }

                return _dataContext;
            }
        }

        protected override void SaveChanges()
        {
            BlogDataContext.SaveChanges();
        }
    }
}
