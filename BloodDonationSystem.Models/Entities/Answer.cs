namespace BloodDonationSystem.Models.Entities
{
    public class Answer
    {
        public int QuestionId { get; set; }
        public Guid AppointmentId { get; set; }
        public string Content { get; set; } = null!;
        public virtual Question? Question { get; set; }
        public virtual Appointment? Appointment { get; set; }
    }
}
