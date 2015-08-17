using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusuCMS.Data
{
    public class PageLabel : Label
    {
        public int PageId { get; set; }

        public virtual Page Page { get; set; }
    }
}
