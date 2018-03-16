using NewsReader.Util;
using System;
using System.Collections.Generic;

namespace NewsReader.Model
{
    [Serializable]
    public class ConfigurationModel
    {
        public string LanguageCode { get; set; }
        public List<DictionaryItem> VisibleCategories { get; set; }
        public RSSLinkCollection RSSLinks { get; set; }
    }
}
