using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusuCMS.Data
{
    public interface ISortable
    {
        int DisplayOrder { get; set; }
    }
}
