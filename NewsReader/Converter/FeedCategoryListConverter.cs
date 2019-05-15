using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using NewsReader.Models;

namespace NewsReader.Converter
{
    internal class FeedCategoryListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = (RssFeedCollection)value;
            if (parameter is null || list is null)
            {
                return list;
            }

            var filterCategory = (RssCategory)parameter;
            return list.Where(x => x.Category.Any(y => y == filterCategory));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
