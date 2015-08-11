namespace Ecobici.WP.Model.Entities
{
    public class AvailabilityStatus
    {
        public int id { get; set; }

        public string status { get; set; }

        public Availability availability { get; set; }
    }
}