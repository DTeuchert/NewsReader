using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace NewsReader.Converter
{
    class BackgroundHasSeenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value 
                ? (SolidColorBrush)Application.Current.Resources["LstBox_Background_HasSeen"] 
                : (SolidColorBrush)Application.Current.Resources["LstBox_Background_HasNotSeen"];
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}