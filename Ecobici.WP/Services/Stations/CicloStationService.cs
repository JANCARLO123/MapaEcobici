using System.Collections.Generic;
using System.Threading.Tasks;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.Services.Stations
{
    public class CicloStationService : ICicloStationService
    {
        #region Fields

        private readonly ICicloStationsManager _cicloStationsManager;

        #endregion Fields

        public CicloStationService(ICicloStationsManager cicloStationsManager)
        {
            _cicloStationsManager = cicloStationsManager;
        }

        public async Task<IEnumerable<CicloStation>> GetCicloStationsAsync()
        {
            var result = _cicloStationsManager.CicloStations;
            if (result == null)
            {
                await _cicloStationsManager.GetCicloStationsAsync();
                result = _cicloStationsManager.CicloStations;
            }

            return result;
        }
    }
}