﻿using System;
using System.Linq;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using NewsReader.Model;
using NewsReader.Model.Enum;

namespace NewsReader.Converter
{
    class FeedHealthListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = (RSSFeedCollection)value;
            return list.Where(x => x.Category.Any(y => y == RSSCategory.Health));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}