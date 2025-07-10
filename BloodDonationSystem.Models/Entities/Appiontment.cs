using System;
using System.Collections.Generic;

namespace BloodDonationSystem.Models.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public Guid MemberId { get; set; }
        public int StatusId { get; set; }
        public string? Reason { get; set; }

        // Navigation properties
        public virtual BloodRequest? Request { get; set; }
        public virtual Account? Member { get; set; }
        public virtual AppointmentStatus? Status { get; set; }
        public virtual ICollection<Answer>? Answers { get; set; }
        public virtual Health? Health { get; set; }
        public virtual ICollection<Donation>? Donations { get; set; }
    }
}
