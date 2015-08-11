using System.Collections.Generic;
using System.Threading.Tasks;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.Services.Stations
{
    public interface ICicloStationsManager
    {
        IEnumerable<CicloStation> CicloStations { get; set; }

        Task GetCicloStationsAsync();

        Task FirstLoadCicloStationsAsync();
    }
}