using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Ecobici.WP.Enumerators;

namespace Ecobici.WP.View.Converters
{
    public class ChronometerTypeToLabelConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChronometerType chronometerType = (ChronometerType)value;
            string result = null;
            switch (chronometerType)
            {
                case ChronometerType.Inicial:
                    result = "Iniciar";
                    break;
                case ChronometerType.Stop:
                    result = "Detener";
                    break;
                case ChronometerType.Refresh:
                    result = "Reiniciar";
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
