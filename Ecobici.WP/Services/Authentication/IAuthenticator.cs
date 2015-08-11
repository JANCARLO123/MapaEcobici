using System.Threading.Tasks;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.Services.Authentication
{
    public interface IAuthenticator
    {
        Task<EcoBiciToken> GetAccessTokenAsync();
    }
}