using System;
using System.Globalization;
using System.Windows.Data;

namespace Petuda.Views.Converters
{
    public sealed class BooleanToScriptMenuHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool && (bool)value) ? 70.0 : 190.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is double && (double)value == 70.0;
        }
    }
}