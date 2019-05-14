using System;
using System.Globalization;
using System.Windows.Data;

namespace NewsReader.Converter
{
    internal class DateTimeFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateOffset = (DateTimeOffset?)value ?? new DateTimeOffset();
            return dateOffset.LocalDateTime.ToString("ddd, dd. MMMM yyyy - H:mm",
                Util.TranslationSource.Instance.CurrentCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
