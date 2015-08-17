using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusuCMS.Data
{
    public class SiteData : ISiteId
    {
        public int Id { get; set; }

        public int SiteId { get; set; }

        public string Value { get; set; }

        public string ModuleName { get; set; }

        public string Key { get; set; }
    }
}
