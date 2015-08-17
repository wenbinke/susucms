using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Xml.Serialization;
using SusuCMS.Data.Infos;
using SusuCMS.Helpers;

namespace SusuCMS.Services
{
    public class SystemService
    {
        private static string _systemInfoVirtualPath = "~/App_Data/SystemInfo.config";

        public static SystemInfo GetSystemInfo()
        {
            var path = HttpContext.Current.Server.MapPath(_systemInfoVirtualPath);

            return XmlHelper.Deserialize<SystemInfo>(path);
        }

        public static void UpdateSystemInfo(SystemInfo info)
        {
            var path = HttpContext.Current.Server.MapPath(_systemInfoVirtualPath);
            XmlHelper.Serialize(path, info);
        }
    }
}
