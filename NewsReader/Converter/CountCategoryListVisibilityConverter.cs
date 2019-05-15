using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using NewsReader.Models;

namespace NewsReader.Converter
{
    internal class CountCategoryListVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is null)
            {
                return (value != null && ((RssFeedCollection)value).Count <= 0) ? Visibility.Visible : Visibility.Hidden;
            }

            var filterCategory = (RssCategory)parameter;
            return ((RssFeedCollection)value == null
                    || ((RssFeedCollection)value).Count(x => x.Category.Any(y => y == filterCategory)) <= 0)
                ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
