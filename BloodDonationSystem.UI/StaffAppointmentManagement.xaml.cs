using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BloodDonationSystem.UI
{
    public partial class StaffAppointmentManagement : Window
    {
        private readonly BloodDonationDbContext _context = new();

        public StaffAppointmentManagement()
        {
            InitializeComponent();
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            var appointments = _context.Appointments
                .Include(a => a.Member)
                .Include(a => a.Request)
                .Where(a => a.StatusId >= 1 && a.StatusId <= 3)
                .ToList();

            foreach (var a in appointments)
            {
                var border = new Border
                {
                    Background = Brushes.White,
                    BorderThickness = new Thickness(2),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(15),
                    Margin = new Thickness(0, 10, 0, 10),
                    BorderBrush = GetStatusColor(a.StatusId)
                };

                var stack = new StackPanel();

                stack.Children.Add(new TextBlock
                {
                    Text = $"🧍 Member: {a.Member?.Name ?? "Unknown"}",
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.Black
                });

                stack.Children.Add(new TextBlock
                {
                    Text = $"📅 Request: {a.Request?.Title ?? "Unknown"}",
                    FontSize = 14,
                    Foreground = Brushes.Gray
                });

                stack.Children.Add(new TextBlock
                {
                    Text = $"🔁 Status: {MapStatus(a.StatusId)}",
                    FontSize = 14,
                    Foreground = GetStatusColor(a.StatusId),
                    Margin = new Thickness(0, 4, 0, 0)
                });

                stack.Children.Add(new TextBlock
                {
                    Text = $"⏱ {a.Request?.StartTime:dd/MM/yyyy HH:mm} → {a.Request?.EndTime:dd/MM/yyyy HH:mm}",
                    FontSize = 14,
                    Foreground = Brushes.DimGray,
                    Margin = new Thickness(0, 2, 0, 8)
                });

                var btn = new Button
                {
                    Content = "👁️‍🗨️ View Detail",
                    Width = 120,
                    Background = GetStatusColor(a.StatusId),
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.Bold,
                    Tag = a.Id
                };
                btn.Click += ViewDetail_Click;

                stack.Children.Add(btn);
                border.Child = stack;
                AppointmentList.Children.Add(border);
            }
        }

        private string MapStatus(int statusId) => statusId switch
        {
            1 => "On Process",
            2 => "Checked In",
            3 => "Donated",
            _ => "Unknown"
        };

        private Brush GetStatusColor(int statusId) => statusId switch
        {
            1 => Brushes.Orange,
            2 => Brushes.SteelBlue,
            3 => Brushes.ForestGreen,
            _ => Brushes.Gray
        };

        private void ViewDetail_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Guid appointmentId)
            {
                var appointment = _context.Appointments.FirstOrDefault(a => a.Id == appointmentId);
                if (appointment == null)
                {
                    MessageBox.Show("❌ Không tìm thấy lịch hẹn!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Window windowToOpen = null!;

                switch (appointment.StatusId)
                {
                    case 1: // On Process → Review
                        windowToOpen = new StaffReview(appointmentId);
                        break;
                    case 2: // Checked In → Health Check
                        windowToOpen = new StaffHealthCheck(appointmentId);
                        break;
                    case 3: // Donated → (Sau sẽ làm)
                       windowToOpen = new StaffDonated(appointmentId);
                        break;
                    default:
                        MessageBox.Show("⚠️ Trạng thái lịch hẹn không hợp lệ.", "Cảnh báo");
                        return;
                }

                windowToOpen.ShowDialog();

                AppointmentList.Children.Clear(); // Refresh UI sau khi xử lý
                LoadAppointments();
            }
        }


    }
}
