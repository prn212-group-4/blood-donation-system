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
            // 📌 Map tất cả entity với tên bảng tương ứng
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Answer>().ToTable("Answers");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            modelBuilder.Entity<AppointmentStatus>().ToTable("AppointmentStatus");
            modelBuilder.Entity<BloodBag>().ToTable("BloodBags");
            modelBuilder.Entity<BloodComponent>().ToTable("BloodComponent");
            modelBuilder.Entity<BloodGroup>().ToTable("BloodGroup");
            modelBuilder.Entity<BloodRequest>().ToTable("BloodRequests");
            modelBuilder.Entity<Donation>().ToTable("Donations");
            modelBuilder.Entity<DonationType>().ToTable("DonationType");
            modelBuilder.Entity<Health>().ToTable("Healths");
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<RequestBloodGroup>().ToTable("RequestBloodGroups");
            modelBuilder.Entity<RequestPriority>().ToTable("RequestPriority");

            // ✅ Giữ nguyên các cấu hình quan hệ
            modelBuilder.Entity<Answer>()
                .HasKey(a => new { a.QuestionId, a.AppointmentId });

            modelBuilder.Entity<RequestBloodGroup>()
                .HasKey(rbg => new { rbg.RequestId, rbg.BloodGroupId });

            modelBuilder.Entity<RequestBloodGroup>()
                .HasOne(rbg => rbg.BloodRequest)
                .WithMany(br => br.RequestBloodGroups)
                .HasForeignKey(rbg => rbg.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RequestBloodGroup>()
                .HasOne(rbg => rbg.BloodGroup)
                .WithMany(bg => bg.RequestBloodGroups)
                .HasForeignKey(rbg => rbg.BloodGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasIndex(a => new { a.RequestId, a.MemberId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Answer>()
        //        .HasKey(a => new { a.QuestionId, a.AppointmentId });

        //    modelBuilder.Entity<RequestBloodGroup>()
        //        .HasKey(rbg => new { rbg.RequestId, rbg.BloodGroupId });

        //    modelBuilder.Entity<RequestBloodGroup>()
        //        .HasOne(rbg => rbg.BloodRequest)
        //        .WithMany(br => br.RequestBloodGroups)
        //        .HasForeignKey(rbg => rbg.RequestId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<RequestBloodGroup>()
        //        .HasOne(rbg => rbg.BloodGroup)
        //        .WithMany(bg => bg.RequestBloodGroups)
        //        .HasForeignKey(rbg => rbg.BloodGroupId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<Appointment>()
        //        .HasIndex(a => new { a.RequestId, a.MemberId })
        //        .IsUnique();

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
