using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SusuCMS
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T each in source)
            {
                action(each);
            }
        }
    }
}
