using System;
using System.Globalization;
using System.Windows.Data;

namespace NewsReader.Converter
{
    class DateTimeFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTimeOffset)value).ToString("ddd, dd. MMMM yyyy - H:mm", new CultureInfo("de-DE"));;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}