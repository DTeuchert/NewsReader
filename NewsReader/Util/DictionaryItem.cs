using NewsReader.Model.Enum;
using System.Xml.Serialization;

namespace NewsReader.Util
{
    public class DictionaryItem
    {
        [XmlAttribute]
        public RSSCategory id;

        [XmlAttribute]
        public bool value;
    }
}