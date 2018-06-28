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
            List<List<string>> HtmlCleanerRegExList = new List<List<string>>();
            config.GetSection("HtmlCleanerRegEx").Bind(HtmlCleanerRegExList);

            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (var item in HtmlCleanerRegExList)
            {
                dict.Add(item[0], item[1]);
            }

            return dict;

        }
    }
}
