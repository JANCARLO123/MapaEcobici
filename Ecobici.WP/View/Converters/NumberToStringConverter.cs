using System;
using Windows.UI.Xaml.Data;

namespace Ecobici.WP.View.Converters
{
    public class NumberToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int seconds = (int)value;
            return string.Format("{0:00}", seconds);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}