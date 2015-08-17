using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SusuCMS
{
    public static class EnumExtensions
    {
        private static Type _displayAttributeType = typeof(DisplayAttribute);
        public static string GetDisplayValue(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var objs = fieldInfo.GetCustomAttributes(_displayAttributeType, false);
            if (objs != null && objs.Length != 0)
            {
                return ((DisplayAttribute)objs[0]).GetName();
            }

            return enumValue.ToString();
        }
    }
}
