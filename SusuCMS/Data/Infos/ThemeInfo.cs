using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SusuCMS.Data.Infos
{
    public class ThemeInfo
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string PreviewImagePath { get; set; }

        public bool EnableCommonCss { get; set; }

        public List<string> CssFiles { get; set; }
    }
}
