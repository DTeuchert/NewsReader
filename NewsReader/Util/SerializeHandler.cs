using System.IO;
using System.Xml.Serialization;
using NewsReader.Model;
using NewsReader.Model.Enum;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NewsReader.Util
{
    class SerializeHandler
    {
        private const string LIST_KEY = @"Resources\Links.xml";
        private const string TABS_KEY = @"Resources\Tabs.xml";

        public static RSSLinkCollection Load_Links()
        {
            if (File.Exists(LIST_KEY))
            {
                using (var reader = new StreamReader(LIST_KEY))
                {
                    var xs = new XmlSerializer(typeof(RSSLinkCollection));
                    try
                    {
                        return (RSSLinkCollection)xs.Deserialize(reader);
                    }
                    catch(Exception)
                    {
                        return new RSSLinkCollection();
                    }
                }
            }
            return new RSSLinkCollection();
        }
        public static void Save_Links(RSSLinkCollection list)
        {
            using (var writer = new StreamWriter(LIST_KEY))
            {
                var xs = new XmlSerializer(typeof(RSSLinkCollection));
                xs.Serialize(writer, list);
            }
        }

        public static Dictionary<RSSCategory, bool> Load_Tabs()
        {
            if (File.Exists(TABS_KEY))
            {
                using (var reader = new StreamReader(TABS_KEY))
                {
                    var xs = new XmlSerializer(typeof(DictionaryItem[]));

                    try
                    {
                        return ((DictionaryItem[])xs.Deserialize(reader))
                            .ToDictionary(i => i.id, i => i.value);
                    }
                    catch(Exception)
                    {
                        return new Dictionary<RSSCategory, bool>
                        {
                            { RSSCategory.General, true },
                            { RSSCategory.Sport, true },
                            { RSSCategory.Technology, true },
                            { RSSCategory.Health, true },
                            { RSSCategory.Economy, true },
                            { RSSCategory.Career, true },
                            { RSSCategory.International, true },
                            { RSSCategory.Politics, true },
                            { RSSCategory.Cultural, true }
                        };
                    }
                }
            }
            return new Dictionary<RSSCategory, bool>
            {
                { RSSCategory.General, true },
                { RSSCategory.Sport, true },
                { RSSCategory.Technology, true },
                { RSSCategory.Health, true },
                { RSSCategory.Economy, true },
                { RSSCategory.Career, true },
                { RSSCategory.International, true },
                { RSSCategory.Politics, true },
                { RSSCategory.Cultural, true }
            }; 
        }
        public static void Save_Tabs(Dictionary<RSSCategory, bool> list)
        {
            using (var writer = new StreamWriter(TABS_KEY))
            {
                var xs = new XmlSerializer(typeof(DictionaryItem[]));
                xs.Serialize(writer, list.Select(kv => new DictionaryItem() { id = kv.Key, value = kv.Value }).ToArray());
            }
        }
    }
}
