using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Web;

namespace SusuCMS.Helpers
{
    public class XmlHelper
    {
        public static T Deserialize<T>(string physicalPath)
        {
            var reader = new StreamReader(physicalPath);
            var xml = new XmlSerializer(typeof(T));
            var obj = (T)xml.Deserialize(reader);
            reader.Close();

            return obj;
        }

        public static void Serialize<T>(string physicalPath, T obj)
        {
            var writer = new StreamWriter(physicalPath);
            var xml = new XmlSerializer(typeof(T));
            xml.Serialize(writer, obj);
            writer.Close();
        }
    }
}
