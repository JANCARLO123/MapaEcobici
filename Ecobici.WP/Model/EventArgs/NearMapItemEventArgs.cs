using System.Collections.Generic;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.Model.EventArgs
{
    public class NearMapItemEventArgs
    {
        public IEnumerable<MapItem> Result { get; set; }

        public NearMapItemEventArgs(IEnumerable<MapItem> mapItems)
        {
            Result = mapItems;
        }
    }
}