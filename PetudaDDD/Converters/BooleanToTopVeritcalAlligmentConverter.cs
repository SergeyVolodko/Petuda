using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Petuda.Views.Converters
{
    public sealed class BooleanToTopVeritcalAlligmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool && (bool)value) ? VerticalAlignment.Top : VerticalAlignment.Center;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is VerticalAlignment && (VerticalAlignment)value == VerticalAlignment.Top;
        }
    }
}