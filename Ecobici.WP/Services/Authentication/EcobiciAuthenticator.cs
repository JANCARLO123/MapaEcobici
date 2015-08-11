using System;
using Ecobici.WP.Services.HttpCommunicator;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.Services.Authentication
{
    public class EcobiciAuthenticator : IAuthenticator
    {
        #region Fields

        private readonly string _clientId = "";
        private readonly string _clientSecret = "";
        private readonly string _url = @"https://pubsbapi.smartbike.com/oauth/v2/token";

        private readonly IHttpCommunicator _httpComunicator;

        #endregion Fields

        public EcobiciAuthenticator(IHttpCommunicator httpComunicator)
        {
            _httpComunicator = httpComunicator;
        }

        public async Task<EcoBiciToken> GetAccessTokenAsync()
        {
            string result = String.Format("{0}?client_id={1}&client_secret={2}&grant_type=client_credentials", _url, _clientId, _clientSecret);
            string jsonResult = await _httpComunicator.GetRequestAsync(result);
            return JsonConvert.DeserializeObject<EcoBiciToken>(jsonResult);
        }
    }
}