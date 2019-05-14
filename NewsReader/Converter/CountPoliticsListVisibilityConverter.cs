using System;
using System.Linq;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using NewsReader.Model;
using NewsReader.Models;

namespace NewsReader.Converter
{
    class CountPoliticsListVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((RSSFeedCollection)value == null 
                || (value as RSSFeedCollection).Count(x => x.Category.Any(y => y == RssCategory.Politics)) <= 0)
                ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}