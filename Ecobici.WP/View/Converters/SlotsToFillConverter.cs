using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Ecobici.WP.View.Converters
{
    public class SlotsToFillConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            SolidColorBrush result = null;
            var mapItem = (int)value;
            if (mapItem >= 10)
            {
                result = new SolidColorBrush(Colors.Green);
            }
            else if (mapItem < 10 && mapItem > 0)
            {
                result = new SolidColorBrush(Colors.Yellow);
            }
            else
            {
                result = new SolidColorBrush(Colors.Red);
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
