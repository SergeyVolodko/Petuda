using System;
using System.Windows.Data;

namespace Petuda.Views.Converters
{
    [ValueConversion(typeof(object), typeof(string))]
    public class MultiANDConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException();

            var result = values[0] is bool && (bool)values[0];

            for (int i = 1; i < values.Length; i++)
            {
                result &= values[i] is bool && (bool)values[i];
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("ConcatenateFieldsMultiValueConverter cannot convert back (bug)!");
        }

    }
}
