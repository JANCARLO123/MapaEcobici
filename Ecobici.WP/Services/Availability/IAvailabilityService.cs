using System.Collections.Generic;
using System.Threading.Tasks;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.Services.Availability
{
    public interface IAvailabilityService
    {
        IEnumerable<AvailabilityStatus> AvailabilityStatuses { get; set; }

        Task GetAvailabilityAsync();
    }
}