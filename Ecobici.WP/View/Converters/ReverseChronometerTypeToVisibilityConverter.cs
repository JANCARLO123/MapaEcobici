using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Ecobici.WP.Enumerators;

namespace Ecobici.WP.View.Converters
{
    public class ReverseChronometerTypeToVisibilityConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChronometerType valueResult = (ChronometerType)value;
            Visibility result = Visibility.Collapsed;
            switch (valueResult)
            {
                case ChronometerType.Inicial:
                    result = Visibility.Visible;
                    break;
                case ChronometerType.Stop:
                    result = Visibility.Collapsed;
                    break;
                case ChronometerType.Refresh:
                    result = Visibility.Collapsed;
                    break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
