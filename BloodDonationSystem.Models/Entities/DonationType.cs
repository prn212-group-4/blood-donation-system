using System.Collections.Generic;

namespace BloodDonationSystem.Models.Entities
{
    public class DonationType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<Donation>? Donations { get; set; }
    }
}
