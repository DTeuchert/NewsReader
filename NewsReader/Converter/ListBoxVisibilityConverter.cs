using System;
using System.Windows;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Windows.Data;
using NewsReader.Model;

namespace NewsReader.Converter
{
    class ListBoxVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null || (value as RSSFeedCollection).Count <= 0)
                ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}