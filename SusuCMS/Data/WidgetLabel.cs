using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusuCMS.Data
{
    public class WidgetLabel : Label
    {
        public int WidgetId { get; set; }

        public virtual Widget Widget { get; set; }
    }
}
