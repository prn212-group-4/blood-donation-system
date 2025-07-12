using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using System;
using System.Windows;

namespace BloodDonationSystem.UI
{
    public partial class StaffHealthCheck : Window
    {
        private readonly BloodDonationDbContext _context = new();
        private readonly Guid _appointmentId;

        public StaffHealthCheck(Guid appointmentId)
        {
            InitializeComponent();
            _appointmentId = appointmentId;
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (!float.TryParse(TempBox.Text, out float temp) ||
                !float.TryParse(WeightBox.Text, out float weight) ||
                !int.TryParse(UpperBPBox.Text, out int upper) ||
                !int.TryParse(LowerBPBox.Text, out int lower) ||
                !int.TryParse(HeartRateBox.Text, out int heartRate))
            {
                MessageBox.Show("❗ Vui lòng nhập đúng định dạng số!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var appointment = _context.Appointments.Find(_appointmentId);
            if (appointment == null)
            {
                MessageBox.Show("❌ Không tìm thấy lịch hẹn!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var health = new Health
            {
                Id = Guid.NewGuid(),
                AppointmentId = _appointmentId,
                Temperature = temp,
                Weight = weight,
                UpperBloodPressure = upper,
                LowerBloodPressure = lower,
                HeartRate = heartRate,
                IsGoodHealth = IsGoodHealthCheck.IsChecked ?? false,
                Note = NoteBox.Text.Trim(),
                CreatedAt = DateTime.Now
            };

            _context.Healths.Add(health);
            appointment.StatusId = 3; // Donated
            _context.SaveChanges();

            MessageBox.Show("✅ Đã lưu kết quả kiểm tra sức khỏe và cập nhật trạng thái.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Logic sau
        }
    }
}
