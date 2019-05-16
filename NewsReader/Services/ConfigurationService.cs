using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using NewsReader.Models;
using NewsReader.Util;

namespace NewsReader.Services
{
    internal class ConfigurationService
    {
        private const string ConfigurationFile = @"Config.xml";

        private static void SaveConfiguration(ConfigurationModel configuration)
        {
            if (!File.Exists(ConfigurationFile))
            {
                File.Create(ConfigurationFile).Dispose();
            }
            using (var writer = new StreamWriter(ConfigurationFile))
            {
                var xs = new XmlSerializer(typeof(ConfigurationModel));
                xs.Serialize(writer, configuration);
            }
        }
        private static ConfigurationModel LoadConfiguration()
        {
            if (File.Exists(ConfigurationFile))
            {
                using (var reader = new StreamReader(ConfigurationFile))
                {
                    var xs = new XmlSerializer(typeof(ConfigurationModel));
                    try
                    {
                        return (ConfigurationModel)xs.Deserialize(reader);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
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
            get => LoadConfiguration().LanguageCode;
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
            get => LoadConfiguration().Links;
            set
            {
                if (value == null) return;
                var configuration = LoadConfiguration();
                configuration.Links = value;
                SaveConfiguration(configuration);
            }
        }

        public static Dictionary<RssCategory, bool> VisibleCategories
        {
            get
            {
                return LoadConfiguration().VisibleCategories
                    .ToDictionary(i => i.Id, i => i.Value);
            }
            set
            {
                if (value == null) return;
                var configuration = LoadConfiguration();
                configuration.VisibleCategories = value
                    .Select(kv => new DictionaryItem() { Id = kv.Key, Value = kv.Value }).ToList();
                SaveConfiguration(configuration);
            }
        }

        private static ConfigurationModel Default =>
            new ConfigurationModel
            {
                LanguageCode = "de-DE",
                Links = new RssLinkCollection
                {
                    new RssLink
                    {
                        Title = "Welt",
                        Link = "https://www.welt.de/feeds/topnews.rss"
                    }
                },
                VisibleCategories = new List<DictionaryItem>
                {
                    new DictionaryItem{ Id = RssCategory.General, Value = true },
                    new DictionaryItem{ Id = RssCategory.Sport, Value = true },
                    new DictionaryItem{ Id = RssCategory.Technology, Value = true },
                    new DictionaryItem{ Id = RssCategory.Health, Value = true },
                    new DictionaryItem{ Id = RssCategory.Economy, Value = true },
                    new DictionaryItem{ Id = RssCategory.Career, Value = true },
                    new DictionaryItem{ Id = RssCategory.International, Value = true },
                    new DictionaryItem{ Id = RssCategory.Politics, Value = true },
                    new DictionaryItem{ Id = RssCategory.Cultural, Value = true },
                }
            };
    }
}
