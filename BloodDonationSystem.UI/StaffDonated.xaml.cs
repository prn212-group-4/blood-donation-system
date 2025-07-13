using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BloodDonationSystem.UI
{
    public partial class StaffDonated : Window
    {
        private readonly BloodDonationDbContext _context = new();
        private readonly Guid _appointmentId;
        private int _selectedTypeId = -1;
        private Border _selectedBorder = null;
        private readonly List<Donation> _pendingDonations = new();


        public StaffDonated(Guid appointmentId)
        {
            InitializeComponent();
            _appointmentId = appointmentId;
            SetupDonationTypeSelection();
        }

        private void SetupDonationTypeSelection()
        {
            // Tìm tất cả Border trong UniformGrid
            var donationTypeBorders = FindVisualChildren<Border>(this)
                .Where(b => b.BorderBrush is SolidColorBrush)
                .ToList();

            foreach (var border in donationTypeBorders)
            {
                border.MouseLeftButtonDown += (s, e) =>
                {
                    // Reset lại tất cả màu
                    foreach (var b in donationTypeBorders)
                    {
                        b.Background = Brushes.White;
                    }

                    border.Background = new SolidColorBrush(Color.FromRgb(245, 245, 245)); // Nhẹ hơn 1 chút
                    string typeName = GetDonationTypeFromBorder(border);
                    var type = _context.DonationTypes.FirstOrDefault(t => t.Name!.ToLower() == typeName.ToLower());
                    if (type != null)
                    {
                        _selectedTypeId = type.Id;
                    }
                };
            }
        }

        private string GetDonationTypeFromBorder(Border border)
        {
            var stack = border.Child as StackPanel;
            if (stack?.Children[0] is TextBlock tb)
            {
                // VD: "❤️ Whole Blood" → "whole_blood"
                return tb.Text.Split(' ', 2).Last().Trim().ToLower().Replace(" ", "_");
            }
            return "";
        }
        private void AddDonationItem_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTypeId == -1)
            {
                MessageBox.Show("❗ Vui lòng chọn loại hiến máu!", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(VolumeBox.Text, out int volume) || volume <= 0)
            {
                MessageBox.Show("❗ Vui lòng nhập số ml hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var type = _context.DonationTypes.FirstOrDefault(t => t.Id == _selectedTypeId);
            if (type == null)
            {
                MessageBox.Show("❌ Không tìm thấy loại hiến máu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Add vào danh sách tạm
            var donation = new Donation
            {
                Id = Guid.NewGuid(),
                AppointmentId = _appointmentId,
                TypeId = type.Id,
                Type = type,
                Amount = volume,
                CreatedAt = DateTime.Now
            };

            _pendingDonations.Add(donation);

            // Hiển thị lên UI
            DonationListBox.Items.Add($"{type.Name} - {volume}ml");

            // Reset lại
            VolumeBox.Text = "";
            _selectedTypeId = -1;
            if (_selectedBorder != null)
            {
                _selectedBorder.Background = Brushes.White;
                _selectedBorder = null;
            }
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (_pendingDonations.Count == 0)
            {
                MessageBox.Show("❗ Chưa có thành phần máu nào được thêm!", "Thiếu dữ liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var appointment = _context.Appointments.Find(_appointmentId);
            if (appointment == null)
            {
                MessageBox.Show("❌ Không tìm thấy cuộc hẹn!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var donation in _pendingDonations)
            {
                _context.Donations.Add(donation);
            }

            appointment.StatusId = 4; // Gán trạng thái phù hợp, ví dụ "Đã hiến"
            _context.SaveChanges();

            MessageBox.Show("✅ Đã ghi nhận tất cả loại hiến máu!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void DonationType_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is Border border)
            {
                // Reset màu border trước đó
                if (_selectedBorder != null)
                {
                    _selectedBorder.Background = Brushes.White;
                }

                // Highlight cái vừa chọn
                border.Background = new SolidColorBrush(Color.FromRgb(232, 245, 233)); // xanh nhạt
                _selectedBorder = border;

                string? typeName = border.Tag?.ToString()?.ToLower();
                if (typeName != null)
                {
                    var type = _context.DonationTypes.FirstOrDefault(t => t.Name!.ToLower() == typeName);
                    if (type != null)
                    {
                        _selectedTypeId = type.Id;
                    }
                }
            }
        }
        // Helper để duyệt UI
        private static System.Collections.Generic.IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child is T t)
                    {
                        yield return t;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
