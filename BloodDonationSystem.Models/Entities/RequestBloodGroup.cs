using System;

namespace BloodDonationSystem.Models.Entities
{
    public class RequestBloodGroup
    {
        public Guid RequestId { get; set; }
        public int BloodGroupId { get; set; }

        public virtual BloodRequest? BloodRequest { get; set; }
        public virtual BloodGroup? BloodGroup { get; set; }
    }
}
