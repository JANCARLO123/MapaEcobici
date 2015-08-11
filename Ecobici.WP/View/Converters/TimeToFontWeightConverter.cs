using System;
using Windows.UI.Text;
using Windows.UI.Xaml.Data;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.View.Converters
{
    public class TimeToFontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = FontWeights.Normal;
            Chronometer chronometer = value as Chronometer;
            if (chronometer != null && chronometer.Minutes <= 5)
                result = FontWeights.Bold;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}