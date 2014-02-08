using System;
using System.Globalization;
using System.Windows.Data;
using Petuda.Views.Resources;

namespace Petuda.Views.Converters
{
    public sealed class BooleanToScriptEditorTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                return Strings.ScriptEditor;
            }

            return Strings.ScriptCreator;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}