using System.Collections.Generic;

namespace BloodDonationSystem.Models.Entities
{
    public class BloodGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<Account>? Accounts { get; set; }
        public virtual ICollection<RequestBloodGroup>? RequestBloodGroups { get; set; }
    }
}
