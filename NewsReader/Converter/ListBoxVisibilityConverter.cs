using NewsReader.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NewsReader.Converter
{
    internal class ListBoxVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is null || ((RssFeedCollection)value).Count <= 0)
                ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}