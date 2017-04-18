using System.IO;
using System.Xml.Serialization;
using NewsReader.Model;

namespace NewsReader.Util
{
    class SerializeHandler
    {
        private const string LIST_KEY = @"Resources\Links.xml";

        public static RSSLinkCollection Load()
        {
            if (File.Exists(LIST_KEY))
            {
                using (var reader = new StreamReader(LIST_KEY))
                {
                    var xs = new XmlSerializer(typeof(RSSLinkCollection));
                    return (RSSLinkCollection)xs.Deserialize(reader);
                }
            }
            return new RSSLinkCollection();
        }

        public static void Save(RSSLinkCollection list)
        {
            using (var writer = new StreamWriter(LIST_KEY))
            {
                var xs = new XmlSerializer(typeof(RSSLinkCollection));
                xs.Serialize(writer, list);
            }
        }
    }
}
