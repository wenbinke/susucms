using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusuCMS.Data
{
    public class User : ISiteId
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Nickname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int Gender { get; set; }

        public string HeadImageUrl { get; set; }

        public DateTime CreationTime { get; set; }

        public int SiteId { get; set; }
    }
}
