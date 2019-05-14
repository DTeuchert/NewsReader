using NewsReader.Models;
using System.Xml.Serialization;

namespace NewsReader.Util
{
    public class DictionaryItem
    {
        [XmlAttribute]
        public RssCategory id;

        [XmlAttribute]
        public bool value;
    }
}