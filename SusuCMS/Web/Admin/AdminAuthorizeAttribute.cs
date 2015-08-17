using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using SusuCMS.Data.Enums;


namespace SusuCMS.Web.Admin
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly Permission _permission;

        public AdminAuthorizeAttribute(Permission permission)
        {
            _permission = permission;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            if (AdminContext.Current.CurrentUser.HasPermission(_permission))
            {
                return true;
            }

            httpContext.Response.StatusCode = 403; 

            return false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 403)
            {
                filterContext.Result = new RedirectResult("~/Admin/NoPermission");
            }
        } 
    }
}
