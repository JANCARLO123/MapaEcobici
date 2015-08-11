using Windows.Devices.Geolocation;
using Ecobici.WP.Common;
using Ecobici.WP.Enumerators;

namespace Ecobici.WP.Model.Entities
{
    public class MapItem : NotifyPropertyBase
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private Geopoint _geopoint;

        public Geopoint Geopoint
        {
            get { return _geopoint; }
            set
            {
                _geopoint = value;
                OnPropertyChanged();
            }
        }

        private ItemType _itemType;

        public ItemType ItemType
        {
            get { return _itemType; }
            set
            {
                _itemType = value;
                OnPropertyChanged();
            }
        }

        private int _bikes;

        public int Bikes
        {
            get { return _bikes; }
            set
            {
                _bikes = value;
                OnPropertyChanged();
            }
        }

        private int _slots;

        public int Slots
        {
            get { return _slots; }
            set
            {
                _slots = value;
                OnPropertyChanged();
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }
}