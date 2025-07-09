using System;

namespace BloodDonationSystem.Models.Entities
{
    public class BloodBag
    {
        public Guid Id { get; set; }
        public Guid DonationId { get; set; }
        public int ComponentId { get; set; }
        public bool IsUsed { get; set; }
        public int Amount { get; set; }
        public DateTime ExpiredTime { get; set; }

        // Navigation properties
        public virtual Donation? Donation { get; set; }
        public virtual BloodComponent? Component { get; set; }
    }
}
