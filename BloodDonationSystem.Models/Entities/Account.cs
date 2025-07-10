using System;
using System.Collections.Generic;

namespace BloodDonationSystem.Models.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public DateTime? Birthday { get; set; }
        public int? BloodGroupId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<BloodRequest>? BloodRequests { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
