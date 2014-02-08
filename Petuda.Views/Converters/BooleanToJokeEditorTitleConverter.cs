using System;
using System.Globalization;
using System.Windows.Data;
using Petuda.Views.Resources;

namespace Petuda.Views.Converters
{
    public sealed class BooleanToJokeEditorTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                return Strings.JokeEditor;
            }

            return Strings.JokeCreator;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}