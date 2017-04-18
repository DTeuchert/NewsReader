using System;
using System.Linq;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using NewsReader.Model;
using NewsReader.Model.Enum;

namespace NewsReader.Converter
{
    class FeedCulturalListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = (RSSFeedCollection)value;
            return list.Where(x => x.Category.Any(y => y == RSSCategory.Kultur));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}