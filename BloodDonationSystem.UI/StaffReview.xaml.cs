using BloodDonationSystem.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BloodDonationSystem.UI
{
    public partial class StaffReview : Window
    {
        private readonly BloodDonationDbContext _context = new();
        private readonly Guid _appointmentId;

        public StaffReview(Guid appointmentId)
        {
            InitializeComponent();
            _appointmentId = appointmentId;
            LoadReview();
        }

        private void LoadReview()
        {
            var appointment = _context.Appointments
                .Include(a => a.Answers)
                    .ThenInclude(ans => ans.Question)
                .FirstOrDefault(a => a.Id == _appointmentId);

            if (appointment == null)
            {
                MessageBox.Show("❌ Không tìm thấy lịch hẹn!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            foreach (var answer in appointment.Answers)
            {
                var panel = new StackPanel { Margin = new Thickness(0, 10, 0, 10) };

                panel.Children.Add(new TextBlock
                {
                    Text = $"❓ {answer.Question?.Content ?? "[Không rõ câu hỏi]"}",
                    FontSize = 16,
                    FontWeight = FontWeights.SemiBold,
                    TextWrapping = TextWrapping.Wrap
                });

                panel.Children.Add(new TextBlock
                {
                    Text = $"✍️ {answer.Content}",
                    FontSize = 14,
                    Foreground = System.Windows.Media.Brushes.DimGray,
                    Margin = new Thickness(0, 4, 0, 0),
                    TextWrapping = TextWrapping.Wrap
                });

                QuestionAnswerPanel.Children.Add(panel);
            }
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == _appointmentId);
            if (appointment != null)
            {
                appointment.StatusId = 2; // Check-in
                _context.SaveChanges();
                MessageBox.Show("✅ Đã duyệt đơn đăng ký.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == _appointmentId);
            if (appointment != null)
            {
                appointment.StatusId = -1; // Hoặc gắn cờ rejected
                appointment.Reason = "Từ chối bởi nhân viên";
                _context.SaveChanges();
                MessageBox.Show("❌ Đã từ chối đơn đăng ký.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                Close();
            }
        }
    }
}
