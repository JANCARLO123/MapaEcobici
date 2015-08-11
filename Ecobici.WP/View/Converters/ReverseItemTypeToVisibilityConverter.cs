using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Ecobici.WP.Enumerators;

namespace Ecobici.WP.View.Converters
{
    public class ReverseItemTypeToVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ItemType itemType = (ItemType)value;
            return itemType == ItemType.Position ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
