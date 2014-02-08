using System;
using System.Windows.Data;

namespace Petuda.Views.Converters
{
    [ValueConversion(typeof(object), typeof(string))]
    public class MultiMinusConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException();

            
            var reducer = parameter!=null ? (double)parameter : 0;

            var result = (double)values[0] - reducer;

            for (int i = 1; i < values.Length; i++)
            {
                result -= (double)values[i];
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("ConcatenateFieldsMultiValueConverter cannot convert back (bug)!");
        }

    }
}