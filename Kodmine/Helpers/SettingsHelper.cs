using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodmine.Helpers
{
    public static class SettingsHelper
    {
        public static IDictionary<string, string> GetHtmlCleanerRegex(IConfiguration config)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string val = "", key = "";
            byte i = 0;

            while (key != null)
            {
                key = config[$"HtmlCleanerRegEx:{i}:0"];
                val = config[$"HtmlCleanerRegEx:{i}:1"];

                if (key != null)
                {
                    dict.Add(key, val);
                }

                i++;
            }

            return dict;

        }
    }
}
