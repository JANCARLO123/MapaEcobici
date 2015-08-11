using Ecobici.WP.Common;

namespace Ecobici.WP.Model.Entities
{
    public class Chronometer : NotifyPropertyBase
    {
        private int _minutes;

        public int Minutes
        {
            get { return _minutes; }
            set
            {
                _minutes = value;
                OnPropertyChanged();
            }
        }

        private int _seconds;

        public int Seconds
        {
            get { return _seconds; }
            set
            {
                _seconds = value < 0 ? 59 : value;
                OnPropertyChanged();
            }
        }
    }
}