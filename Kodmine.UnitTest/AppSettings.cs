using System;
using System.Collections.Generic;
using System.Text;

namespace Kodmine.UnitTest
{
    public class AppSettings
    {
        public int PostPageCount { get; set; }
        public string PostKeysSeparator { get; set; }
        public List<List<string>> HtmlCleanerRegEx { get; set; }
    }
}
