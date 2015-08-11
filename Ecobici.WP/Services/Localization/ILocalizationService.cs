using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Ecobici.WP.Services.Localization
{
    public interface ILocalizationService
    {

        Geolocator Geolocator { get; set; }

        Task<Geoposition> GetGeopositionAsync();

    }
}