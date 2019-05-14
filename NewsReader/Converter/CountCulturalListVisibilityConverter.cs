using System;
using System.Linq;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using NewsReader.Models;

namespace NewsReader.Converter
{
    class CountCulturalListVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((RssFeedCollection)value == null 
                || (value as RssFeedCollection).Count(x => x.Category.Any(y => y == RssCategory.Cultural)) <= 0)
                ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}