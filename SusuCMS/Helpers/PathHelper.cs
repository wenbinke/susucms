using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace SusuCMS.Helpers
{
    public static class PathHelper
    {
        public static void EnsureFolder(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                Directory.CreateDirectory(absolutePath);
            }
        }

        public static void EnsureFile(string absolutePath)
        {
            if (!File.Exists(absolutePath))
            {
                File.Create(absolutePath).Close();
            }
        }
    }
}
