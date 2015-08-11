using System;
using Ecobici.WP.Services.HttpCommunicator;
using Ecobici.WP.Services.TokenManager;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.Services.Availability
{
    public class AvailabilityService : IAvailabilityService
    {
        private string _url = "https://pubsbapi.smartbike.com/api/v1/stations/status.json?access_token=";
        private readonly IHttpCommunicator _httpCommunicator;
        private readonly ITokenManager _tokenManager;

        public IEnumerable<AvailabilityStatus> AvailabilityStatuses { get; set; }

        public AvailabilityService(IHttpCommunicator httpCommunicator, ITokenManager tokenManager)
        {
            _httpCommunicator = httpCommunicator;
            _tokenManager = tokenManager;
        }

        public async Task GetAvailabilityAsync()
        {
            var token = await _tokenManager.GetTokenAsync();
            string cicloStationsUrl = $"{_url}{token.access_token}";
            string jsonResult = await _httpCommunicator.GetRequestAsync(cicloStationsUrl);
            var availabilitiesResult = JsonConvert.DeserializeObject<AvailabilityStatusResult>(jsonResult);

            if (availabilitiesResult != null)
            {
                AvailabilityStatuses = availabilitiesResult.stationsStatus;
            }
        }
    }
}