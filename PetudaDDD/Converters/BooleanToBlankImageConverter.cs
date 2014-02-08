using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Petuda.Views.Converters
{
    public sealed class BooleanToBlankImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imagePath = "";

            if (value is bool && (bool)value)
            {
                imagePath ="/Images/blank.png";
            }
            else
            {
                imagePath = "/Images/blank_inverted.png";
            }

            return new BitmapImage(new Uri(imagePath, UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}