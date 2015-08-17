using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SusuCMS.Data
{
    [NotMapped]
    public class UserSetting
    {
        public List<AppInfoConfig> AppInfoConfigs { get; set; }
    }

    [NotMapped]
    public class AppInfoConfig
    {
        public bool Enabled { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string Scope { get; set; }
        public string RedirectUri { get; set; }
    }
}
