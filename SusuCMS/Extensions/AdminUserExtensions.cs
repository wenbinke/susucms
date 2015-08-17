using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using SusuCMS.Data;

namespace SusuCMS
{
    public static class AdminUserExtensions
    {
        public static void ChangePassword(this AdminUser user, string newPassword)
        {
            user.Salt = Crypto.GenerateSalt();
            user.Password = Crypto.HashPassword(string.Concat(newPassword, user.Salt));
        }

        public static bool VerifyPassword(this AdminUser user, string password)
        {
            return Crypto.VerifyHashedPassword(user.Password, string.Concat(password, user.Salt));
        }
    }
}
