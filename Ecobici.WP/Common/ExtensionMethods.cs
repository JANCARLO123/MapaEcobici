using System;
using Windows.Devices.Geolocation;

namespace Ecobici.WP.Common
{
    public static class ExtensionMethods
    {
        public static double CalculateDistance(this Geopoint currentGeopoint, Geopoint geopoint)
        {
            double dDistance = Double.MinValue;
            double dLat1InRad = currentGeopoint.Position.Latitude * (Math.PI / 180.0);
            double dLong1InRad = currentGeopoint.Position.Longitude * (Math.PI / 180.0);
            double dLat2InRad = geopoint.Position.Latitude * (Math.PI / 180.0);
            double dLong2InRad = geopoint.Position.Longitude * (Math.PI / 180.0);
            double dLongitude = dLong2InRad - dLong1InRad;
            double dLatitude = dLat2InRad - dLat1InRad;

            // Intermediate result a.
            double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                       Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                       Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Intermediate result c (great circle distance in Radians).
            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

            // Distance.
            const Double kEarthRadiusKms = 6376.5;
            dDistance = kEarthRadiusKms * c;

            return dDistance;
        }
    }
}