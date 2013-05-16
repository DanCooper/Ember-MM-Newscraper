using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace RottenTomatoes
{
    public static class Utility
    {
        // remove invalid characters for use in URL
        public static string EscapeString(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

#if WINDOWS_PHONE
            return Regex.Replace(s, "[" + Regex.Escape(new String(Path.GetInvalidPathChars())) + "]", "-");
#else
            return Regex.Replace(s, "[" + Regex.Escape(new String(Path.GetInvalidFileNameChars())) + "]", "-");
#endif
        }
    }
}
