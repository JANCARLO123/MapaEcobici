using System.Threading.Tasks;

namespace Ecobici.WP.Services.HttpCommunicator
{
    public interface IHttpCommunicator
    {
        Task<string> GetRequestAsync(string url);
    }
}