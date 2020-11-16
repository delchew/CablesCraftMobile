using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Cables;
using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class TwistedElementTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TwistedElementType twistedElementType)
            {
                var name = twistedElementType.GetType()
                                             .GetMember(twistedElementType.ToString())[0]
                                             .GetCustomAttribute<DescriptionAttribute>()
                                             .Description;
                //return new TypeOfTwist
                //{
                //    Name = name,
                //    TwistedElementType = twistedElementType
                //};
                return name;
            }
            throw new InvalidCastException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}