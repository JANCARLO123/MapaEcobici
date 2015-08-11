namespace Ecobici.WP.Model.Entities
{
    public class CicloStation
    {
        public int id { get; set; }

        public string name { get; set; }

        public string address { get; set; }

        public string addressNumber { get; set; }

        public string zipCode { get; set; }

        public string districtCode { get; set; }

        public string districtName { get; set; }

        public int[] nearbyStations { get; set; }

        public StationLocation location { get; set; }

        public string stationType { get; set; }
    }
}