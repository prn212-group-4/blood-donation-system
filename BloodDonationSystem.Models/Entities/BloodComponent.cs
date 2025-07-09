using System.Collections.Generic;

namespace BloodDonationSystem.Models.Entities
{
    public class BloodComponent
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<BloodBag>? BloodBags { get; set; }
    }
}
