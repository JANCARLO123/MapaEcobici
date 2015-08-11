using System.Collections.Generic;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.Model.EventArgs
{
    public class MapItemEventArgs
    {
        public IEnumerable<MapItem> Result { get; set; }

        public MapItemEventArgs(IEnumerable<MapItem> mapItems)
        {
            Result = mapItems;
        }
    }
}