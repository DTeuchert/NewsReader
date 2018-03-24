using NewsReader.Model;
using NewsReader.Util;
using NewsReader.Model.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace NewsReader.Service
{
    class ConfigurationService
    {
        private const string configurationFile = @"Resources\Config.xml";

        private static void SaveConfiguration(ConfigurationModel configuration)
        {
            using (var writer = new StreamWriter(configurationFile))
            {
                var xs = new XmlSerializer(typeof(ConfigurationModel));
                xs.Serialize(writer, configuration);
            }
        }
        private static ConfigurationModel LoadConfiguration()
        {
            if (File.Exists(configurationFile))
            {
                using (var reader = new StreamReader(configurationFile))
                {
                    var xs = new XmlSerializer(typeof(ConfigurationModel));
                    try
                    {
                        return (ConfigurationModel)xs.Deserialize(reader);
                    }
                    catch (Exception)
                    { }
                }
            }
            else
            {
                SaveConfiguration(Default);
            }
            return Default;
        }

        public static string Language
        {
            get { return (LoadConfiguration()).LanguageCode; } 
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                var configuration = LoadConfiguration();
                configuration.LanguageCode = value;
                SaveConfiguration(configuration);
            }
        }

        public static RSSLinkCollection Links
        {
            get { return (LoadConfiguration()).RSSLinks; }
            set
            {
                if (value == null) return;
                var configuration = LoadConfiguration();
                configuration.RSSLinks= value;
                SaveConfiguration(configuration);
            }
        }

        public static Dictionary<RSSCategory, bool> VisibleCategories
        {
            get {
                return (LoadConfiguration()).VisibleCategories
                    .ToDictionary(i => i.id, i => i.value);
            }
            set
            {
                if (value == null) return;
                var configuration = LoadConfiguration();
                configuration.VisibleCategories = value
                    .Select(kv => new DictionaryItem() { id = kv.Key, value = kv.Value }).ToList();
                SaveConfiguration(configuration);
            }
        }

        private static ConfigurationModel Default
        {
            get
            {
                return new ConfigurationModel
                {
                    LanguageCode = "de-DE",
                    RSSLinks = new RSSLinkCollection
                    {
                        new RSSLink
                        {
                            Title = "Welt",
                            Link = "https://www.welt.de/feeds/topnews.rss"
                        }
                    },
                    VisibleCategories = new List<DictionaryItem>
                    {
                        new DictionaryItem{ id = RSSCategory.General, value = true },
                        new DictionaryItem{ id = RSSCategory.Sport, value = true },
                        new DictionaryItem{ id = RSSCategory.Technology, value = true },
                        new DictionaryItem{ id = RSSCategory.Health, value = true },
                        new DictionaryItem{ id = RSSCategory.Economy, value = true },
                        new DictionaryItem{ id = RSSCategory.Career, value = true },
                        new DictionaryItem{ id = RSSCategory.International, value = true },
                        new DictionaryItem{ id = RSSCategory.Politics, value = true },
                        new DictionaryItem{ id = RSSCategory.Cultural, value = true },
                    }
                };
            }
        }
    }
}
