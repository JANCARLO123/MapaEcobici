using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Ecobici.WP.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        public Geolocator Geolocator { get; set; }

        public LocalizationService()
        {

            Geolocator = new Geolocator
            {
                DesiredAccuracyInMeters = 1,
                MovementThreshold = 10,
                DesiredAccuracy = PositionAccuracy.High
            };
        }

        public async Task<Geoposition> GetGeopositionAsync()
        {
            Geoposition result = null;


            try
            {
                result = await Geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(1),
                    timeout: TimeSpan.FromSeconds(10));
            }
            catch (Exception ex)
            {
                if ((uint) ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                }
            }
            return result;
        }
    }
}