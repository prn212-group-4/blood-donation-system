using BloodDonationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystem.DAL.Data
{
    public class BloodDonationDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<BloodBag> BloodBags { get; set; }
        public DbSet<BloodComponent> BloodComponents { get; set; }
        public DbSet<BloodGroup> BloodGroups { get; set; }
        public DbSet<BloodRequest> BloodRequests { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<DonationType> DonationTypes { get; set; }
        public DbSet<Health> Healths { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<RequestBloodGroup> RequestBloodGroups { get; set; }
        public DbSet<RequestPriority> RequestPriorities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=BloodDonationDB;User Id=sa;Password=12345;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                .HasKey(a => new { a.QuestionId, a.AppointmentId });

            modelBuilder.Entity<RequestBloodGroup>()
                .HasKey(rbg => new { rbg.RequestId, rbg.BloodGroupId });

            modelBuilder.Entity<Appointment>()
                .HasIndex(a => new { a.RequestId, a.MemberId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
