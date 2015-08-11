using Ecobici.WP.Common;
using Ecobici.WP.Services.Authentication;
using System;
using System.Threading.Tasks;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.Services.TokenManager
{
    public class TokenManager : ITokenManager
    {
        private readonly IAuthenticator _authenticator;

        #region Field

        private readonly string fileName = "";

        #endregion Field

        #region Ctor

        public TokenManager(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            fileName = loader.GetString("TokenFile");
        }

        #endregion Ctor

        public async Task<EcoBiciToken> GetTokenAsync()
        {
            EcoBiciToken result = default(EcoBiciToken);
            JsonController<EcobiciTokenControllerEntity> xmlController = new JsonController<EcobiciTokenControllerEntity>(fileName);

            var ecobiciTokenControllerResult = await xmlController.ReadElementAsync();

            //TODO: el token solo esta disponible 1 hora
            if (ecobiciTokenControllerResult != null && (DateTime.Now - ecobiciTokenControllerResult.SaveDate).Hours < 1) return ecobiciTokenControllerResult.EcoBiciToken;
            result = await _authenticator.GetAccessTokenAsync();
            EcobiciTokenControllerEntity ecobiciTokenControllerEntity = new EcobiciTokenControllerEntity()
            {
                EcoBiciToken = result,
                SaveDate = DateTime.Now
            };

            await xmlController.SaveElementAsync(ecobiciTokenControllerEntity);
            return result;
        }
    }
}