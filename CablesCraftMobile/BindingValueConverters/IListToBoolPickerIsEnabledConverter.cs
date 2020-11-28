using System;
using System.Collections.Generic;
using System.Globalization;
using Cables.Materials;
using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class IListToBoolPickerIsEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is IList<Tape> iList)
                return iList.Count > 1;
            throw new ArgumentException("Источник привязки не является объектом, реализующим интерфейс IList<Tape> или равен null!");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Конвертер не предназначен для привязки от источника в сторону цели!");
        }
    }
}
