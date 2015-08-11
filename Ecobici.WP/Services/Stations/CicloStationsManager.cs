using System;
using Ecobici.WP.Common;
using Ecobici.WP.Services.HttpCommunicator;
using Ecobici.WP.Services.TokenManager;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.Services.Stations
{
    public class CicloStationsManager : ICicloStationsManager
    {
        #region Fields

        private readonly string _url = @"https://pubsbapi.smartbike.com/api/v1/stations.json?access_token=";

        private readonly IHttpCommunicator _httpCommunicator;
        private readonly ITokenManager _tokenManager;

        private readonly string fileName = "";

        #endregion Fields

        #region Properties

        public IEnumerable<CicloStation> CicloStations { get; set; }

        #endregion Properties

        #region Ctor

        public CicloStationsManager(IHttpCommunicator httpCommunicator, ITokenManager tokenManager)
        {
            _httpCommunicator = httpCommunicator;
            _tokenManager = tokenManager;
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            fileName = loader.GetString("CicloStationFile");
        }

        #endregion Ctor

        #region Methods

        public async Task GetCicloStationsAsync()
        {
            JsonController<IEnumerable<CicloStation>> jsonController = new JsonController<IEnumerable<CicloStation>>(fileName);
            CicloStations = await jsonController.ReadElementAsync();
        }

        public async Task FirstLoadCicloStationsAsync()
        {
            var token = await _tokenManager.GetTokenAsync();
            string cicloStationsUrl = String.Format("{0}{1}", _url, token.access_token);
            string jsonResult = await _httpCommunicator.GetRequestAsync(cicloStationsUrl);
            var cicloStationResult = JsonConvert.DeserializeObject<CicloStationResult>(jsonResult);

            if (cicloStationResult != null)
            {
                JsonController<IEnumerable<CicloStation>> jsonController = new JsonController<IEnumerable<CicloStation>>(fileName);
                CicloStations = cicloStationResult.stations;
                await jsonController.SaveElementAsync(cicloStationResult.stations);
            }
        }

        #endregion Methods
    }
}