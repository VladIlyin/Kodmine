using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Kodmine.Core.Extensions
{
    public static class Extension
    {
        public static string SurroundWithDiv (this string str)
        {
            return "<div>" + str + "</div>";
        }

        public static string TakePostTeaser(this string str)
        {
            var match = Regex.Match(str, @"<span.+class=.+post-teaser.+<\/span>");
            
            if (match.Success)
                return match.Value;

            match = Regex.Match(str, @"<p.+<\/p>");

            if (match.Success)
                return match.Value;

            return string.Empty;
        }
    }
}
