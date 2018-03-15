using System;
using System.Globalization;
using System.Windows.Data;

namespace NewsReader.Converter
{
    class IsSelectedLanguageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var itemLanguage = new CultureInfo(parameter.ToString());
            return Util.TranslationSource.Instance.CurrentCulture.Equals(itemLanguage);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}