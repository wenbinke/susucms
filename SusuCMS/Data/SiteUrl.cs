using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SusuCMS.Data
{
    [NotMapped]
    public class SiteUrl
    {
        public string Name { get; set; }

        public string GetSubFolder()
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                return string.Empty;
            }

            var index = Name.IndexOf("/");
            if (index > 0)
            {
                return Name.Substring(index + 1);
            }

            return string.Empty;
        }
    }
}
