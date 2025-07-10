using System;
using System.Collections.Generic;

namespace BloodDonationSystem.Models.Entities
{
    public class Donation
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public int TypeId { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Appointment? Appointment { get; set; }
        public virtual DonationType? Type { get; set; }
        public virtual ICollection<BloodBag>? BloodBags { get; set; }
    }
}
