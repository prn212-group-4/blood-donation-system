using System.Collections.Generic;

namespace BloodDonationSystem.Models.Entities
{
    public class AppointmentStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
