using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;

namespace Trakttv
{
    /// <summary>
    /// Methods used to transform to and from JSON
    /// </summary>
    public static class JSONExtensions
    {
        /// <summary>
        /// Creates a list based on a JSON Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonArray"></param>
        /// <returns></returns>
        public static IEnumerable<T> FromJSONArray<T>(this string jsonArray)
        {
            if (string.IsNullOrEmpty(jsonArray)) return new List<T>();

            try
            {
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonArray)))
                {
                    var ser = new DataContractJsonSerializer(typeof(IEnumerable<T>));
                    var result = (IEnumerable<T>)ser.ReadObject(ms);

                    if (result == null)
                    {
                        return new List<T>();
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Creates an object from JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJSON<T>(this string json)
        {
            if (string.IsNullOrEmpty(json)) return default(T);

            try
            {
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json.ToCharArray())))
                {
                    var ser = new DataContractJsonSerializer(typeof(T));
                    return (T)ser.ReadObject(ms);
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Turns an object into JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSON(this object obj)
        {
            if (obj == null) return string.Empty;
            using (var ms = new MemoryStream())
            {
                var ser = new DataContractJsonSerializer(obj.GetType());
                ser.WriteObject(ms, obj);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public static string ToSlug(this string item)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"[{0}]+", invalidChars);
            return Regex.Replace(item, invalidReStr, string.Empty).ToLower().Replace(" ", "-");
        }

        public static bool IsNumber(this string number)
        {
            double retValue;
            return double.TryParse(number, out retValue);
        }

        public static string StripHTML(this string htmlString)
        {
            if (string.IsNullOrEmpty(htmlString)) return string.Empty;

            string pattern = @"<(.|\n)*?>";
            return Regex.Replace(htmlString, pattern, string.Empty);
        }

        public static string RemapHighOrderChars(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            // hack to remap high order unicode characters with a low order equivalents
            // for now, this allows better usage of clipping. This can be removed, once the skin engine can properly render unicode without falling back to sprites
            // as unicode is more widely used, this will hit us more with existing font rendering only allowing cached font textures with clipping

            input = input.Replace((char)8211, '-');  //	–
            input = input.Replace((char)8212, '-');  //	—
            input = input.Replace((char)8216, '\''); //	‘
            input = input.Replace((char)8217, '\''); //	’
            input = input.Replace((char)8220, '"');  //	“
            input = input.Replace((char)8221, '"');  //	”
            input = input.Replace((char)8223, '"');  // ‟
            input = input.Replace((char)8226, '*');  //	•
            input = input.Replace(((char)8230).ToString(), "...");  //	…

            return input;
        }

        public static string SurroundWithDoubleQuotes(this string text)
        {
            return SurroundWith(text, "\"");
        }

        public static string SurroundWith(this string text, string ends)
        {
            return ends + text + ends;
        }
    }
}
