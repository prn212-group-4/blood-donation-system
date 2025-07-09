using System;

namespace BloodDonationSystem.Models.Entities
{
    public class Health
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public float Temperature { get; set; }
        public float Weight { get; set; }
        public int UpperBloodPressure { get; set; }
        public int LowerBloodPressure { get; set; }
        public int HeartRate { get; set; }
        public bool IsGoodHealth { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Appointment? Appointment { get; set; }
    }
}
