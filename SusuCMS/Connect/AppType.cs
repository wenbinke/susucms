using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SusuCMS.Connect
{
    public enum AppType
    {
        [Display(ResourceType = typeof(DisplayResource), Name = "AppType_Google")]
        Google = 0,
        [Display(ResourceType = typeof(DisplayResource), Name = "AppType_Live")]
        Live = 1,
        [Display(ResourceType = typeof(DisplayResource), Name = "AppType_Renren")]
        Renren = 2,
        [Display(ResourceType = typeof(DisplayResource), Name = "AppType_Sina")]
        Sina = 3,
        [Display(ResourceType = typeof(DisplayResource), Name = "AppType_Taobao")]
        Taobao = 4,
        [Display(ResourceType = typeof(DisplayResource), Name = "AppType_QQ")]
        QQ = 5,
        [Display(ResourceType = typeof(DisplayResource), Name = "AppType_TencentWeibo")]
        TencentWeibo = 6
    }
}
