using System.Xml.Serialization;
using NewsReader.Models;

namespace NewsReader.Util
{
    public class DictionaryItem
    {
        [XmlAttribute]
        public RssCategory Id;

        [XmlAttribute]
        public bool Value;
    }
}
