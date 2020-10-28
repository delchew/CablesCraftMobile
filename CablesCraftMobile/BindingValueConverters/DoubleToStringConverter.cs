using System;
using System.Globalization;
using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
                return doubleValue.ToString();
            throw new InvalidCastException("Переданное значение не является типом double!");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                if (double.TryParse(stringValue, out double number))
                {
                    return number;
                }
            }
            throw new InvalidCastException("Переданное значение не является типом string!");
        }
    }
}
