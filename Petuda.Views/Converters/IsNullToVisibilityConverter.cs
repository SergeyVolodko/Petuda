using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Petuda.Views.Converters
{
    public sealed class IsNullToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}