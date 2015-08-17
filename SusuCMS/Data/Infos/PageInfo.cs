using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusuCMS.Data.Infos
{
    public class PageInfo
    {
        public string Name { get; set; }
        public string Template { get; set; }
        public string Url { get; set; }
        public bool IsHomePage { get; set; }
    }
}
