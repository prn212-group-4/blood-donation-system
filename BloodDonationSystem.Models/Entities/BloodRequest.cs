using System;
using System.Collections.Generic;

namespace BloodDonationSystem.Models.Entities
{
    public class BloodRequest
    {
        public Guid Id { get; set; }
        public Guid StaffId { get; set; }
        public int PriorityId { get; set; }
        public string Title { get; set; } = null!;
        public int MaxPeople { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual Account? Staff { get; set; }
        public virtual RequestPriority? Priority { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
        public virtual ICollection<RequestBloodGroup>? RequestBloodGroups { get; set; }
    }
}
