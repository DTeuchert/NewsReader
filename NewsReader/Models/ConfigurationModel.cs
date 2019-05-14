using NewsReader.Util;
using System;
using System.Collections.Generic;

namespace NewsReader.Models
{
    [Serializable]
    public class ConfigurationModel
    {
        public string LanguageCode { get; set; }
        public List<DictionaryItem> VisibleCategories { get; set; }
        public RssLinkCollection RssLinks { get; set; }
    }
}
