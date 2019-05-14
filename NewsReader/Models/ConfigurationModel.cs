using System;
using System.Collections.Generic;
using NewsReader.Util;

namespace NewsReader.Models
{
    [Serializable]
    public class ConfigurationModel
    {
        public string LanguageCode { get; set; }
        public List<DictionaryItem> VisibleCategories { get; set; }
        public RssLinkCollection Links { get; set; }
    }
}
