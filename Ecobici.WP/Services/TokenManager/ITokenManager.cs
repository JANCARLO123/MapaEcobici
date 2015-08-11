using System.Threading.Tasks;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.Services.TokenManager
{
    public interface ITokenManager
    {
        Task<EcoBiciToken> GetTokenAsync();
    }
}