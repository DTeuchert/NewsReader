using System;
using System.Globalization;
using System.Windows.Data;

namespace NewsReader.Converter
{
    internal class IsSelectedLanguageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cultureName = parameter is null ? "en-US" : parameter.ToString();
            return Util.TranslationSource.Instance.CurrentCulture.Equals(new CultureInfo(cultureName));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
