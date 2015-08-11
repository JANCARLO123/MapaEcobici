using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Ecobici.WP.Enumerators;

namespace Ecobici.WP.View.Converters
{
    public class ChronometerTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChronometerType chronometerType = (ChronometerType)value;
            BitmapIcon result = null;
            switch (chronometerType)
            {
                case ChronometerType.Inicial:
                    result = new BitmapIcon()
                    {
                        UriSource = new Uri("ms-appx:///Assets/Icons/Alarm.png", UriKind.RelativeOrAbsolute)
                    };
                    break;
                case ChronometerType.Stop:

                    result = new BitmapIcon()
                    {
                        UriSource = new Uri("ms-appx:///Assets/Icons/Stop.png", UriKind.RelativeOrAbsolute)
                    };
                    break;
                case ChronometerType.Refresh:
                    result = new BitmapIcon()
                    {
                        UriSource = new Uri("ms-appx:///Assets/Icons/Refresh.png", UriKind.RelativeOrAbsolute)
                    };
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
