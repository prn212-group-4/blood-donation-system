using System.Collections.Generic;

namespace BloodDonationSystem.Models.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<Answer>? Answers { get; set; }
    }
}
