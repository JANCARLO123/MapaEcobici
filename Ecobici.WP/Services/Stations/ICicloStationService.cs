using System.Collections.Generic;
using System.Threading.Tasks;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.Services.Stations
{
    public interface ICicloStationService
    {
        Task<IEnumerable<CicloStation>> GetCicloStationsAsync();
    }
}