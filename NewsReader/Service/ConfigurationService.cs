using NewsReader.Util;
using NewsReader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace NewsReader.Service
{
    class ConfigurationService
    {
        private const string configurationFile = @"Config.xml";

        private static void SaveConfiguration(ConfigurationModel configuration)
        {
            if (!File.Exists(configurationFile))
            {
                File.Create(configurationFile).Dispose();
            }
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

        public static RssLinkCollection Links
        {
            get { return (LoadConfiguration()).RssLinks; }
            set
            {
                if (value == null) return;
                var configuration = LoadConfiguration();
                configuration.RssLinks= value;
                SaveConfiguration(configuration);
            }
        }

        public static Dictionary<RssCategory, bool> VisibleCategories
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
                    RssLinks = new RssLinkCollection
                    {
                        new RssLink
                        {
                            Title = "Welt",
                            Link = "https://www.welt.de/feeds/topnews.rss"
                        }
                    },
                    VisibleCategories = new List<DictionaryItem>
                    {
                        new DictionaryItem{ id = RssCategory.General, value = true },
                        new DictionaryItem{ id = RssCategory.Sport, value = true },
                        new DictionaryItem{ id = RssCategory.Technology, value = true },
                        new DictionaryItem{ id = RssCategory.Health, value = true },
                        new DictionaryItem{ id = RssCategory.Economy, value = true },
                        new DictionaryItem{ id = RssCategory.Career, value = true },
                        new DictionaryItem{ id = RssCategory.International, value = true },
                        new DictionaryItem{ id = RssCategory.Politics, value = true },
                        new DictionaryItem{ id = RssCategory.Cultural, value = true },
                    }
                };
            }
        }
    }
}
