using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.ComponentModel;

namespace SusuCMS
{
    public static class StringExtensions
    {
        public static string Summary(this string text, int length = 100, string more = "...")
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            var result = text.StripHtml();
            if (result.Length < length)
            {
                return result;
            }
            else
            {
                return result.Substring(0, length) + more;
            }
        }

        public static string StripHtml(this string htmlString)
        {
            if (String.IsNullOrWhiteSpace(htmlString))
            {
                return htmlString;
            }

            var pattern = @"<(.|\n)*?>";

            return Regex.Replace(htmlString, pattern, string.Empty);
        }

        public static string AsSlug(this string text)
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                text = text.Trim().ToLower();

                // remove accents, swap ñ for n, etc
                var from = "àáäâèéëêìíïîòóöôùúüûñç·/_,:;".ToCharArray();
                var to = "aaaaeeeeiiiioooouuuunc------".ToCharArray();

                for (int i = 0; i < from.Length; i++)
                {
                    text = text.Replace(from[i], to[i]);
                }

                text = Regex.Replace(text, @"[^a-zA-Z0-9\u4e00-\u9fa5]+", i => i.Index == 0 ? "" : "-");

                return RemoveExtraHyphen(text);
            }

            return string.Empty;
        }

        private static string RemoveExtraHyphen(string text)
        {
            if (text.Contains("--"))
            {
                text = text.Replace("--", "-");
                return RemoveExtraHyphen(text);
            }

            return text;
        }
    }
}
