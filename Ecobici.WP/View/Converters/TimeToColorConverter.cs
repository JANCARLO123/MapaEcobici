using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.View.Converters
{
    public class TimeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Brush result = new SolidColorBrush(Colors.LimeGreen);

            var valueResult = value as Chronometer;
            if (valueResult != null && (valueResult.Minutes < 5 && valueResult.Minutes > 2))
                result = new SolidColorBrush(Colors.Yellow);
            else if (valueResult != null && valueResult.Minutes <= 2)
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