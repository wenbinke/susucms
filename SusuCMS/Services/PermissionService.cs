using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using System.Collections;
using SusuCMS.Data;

namespace SusuCMS.Services
{
    public class PermissionService : ServiceBase<DataContext>
    {
        public PermissionService(DataContext dataContext) : base(dataContext) { }

        public void CreateUser(AdminUser user, bool submit = true)
        {
            if (DataContext.AdminUsers.Any(i => i.UserName == user.UserName))
            {
                throw new InvalidOperationException("User exist.");
            }

            user.Salt = Crypto.GenerateSalt();
            user.Password = Crypto.HashPassword(string.Concat(user.Password, user.Salt));
            user.CreationTime = DateTime.Now;
            DataContext.AdminUsers.Add(user);
            SaveChanges(submit);
        }
    }
}
