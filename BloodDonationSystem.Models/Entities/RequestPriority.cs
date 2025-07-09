using System.Collections.Generic;

namespace BloodDonationSystem.Models.Entities
{
    public class RequestPriority
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // Navigation property
        public virtual ICollection<BloodRequest>? BloodRequests { get; set; }
    }
}
