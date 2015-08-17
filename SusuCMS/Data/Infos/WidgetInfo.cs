using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusuCMS.Data.Infos
{
    public class WidgetInfo
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public int AdminWindowHeight { get; set; }

        public int AdminWindowWidth { get; set; }

        public bool IsSystem { get; set; }
    }
}
