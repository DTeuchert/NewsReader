using System.Windows.Data;

namespace NewsReader.Util
{
    public class LocalizationExtension : Binding
    {
        public LocalizationExtension(string name)
        : base("[" + name + "]")
        {
            Mode = BindingMode.OneWay;
            Source = TranslationSource.Instance;
        }
    }
}
