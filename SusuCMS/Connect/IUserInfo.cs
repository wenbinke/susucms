using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SusuCMS.Data.Enums;


namespace SusuCMS.Connect
{
    public interface IUserInfo
    {
        string Id { get; }

        string NickName { get; }

        string Email { get; }

        string HeadImageUrl { get; }

        Gender Gender { get; }
    }
}
