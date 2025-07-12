using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BloodDonationSystem.UI
{
    public partial class ViewBloodRequest : Window
    {
        private readonly BloodDonationDbContext _context = new();

        public ViewBloodRequest()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            var requests = _context.BloodRequests
                .Include(r => r.RequestBloodGroups)
                    .ThenInclude(rbg => rbg.BloodGroup)
                .ToList();

            foreach (var r in requests)
            {
                var border = new Border
                {
                    Margin = new Thickness(0, 10, 0, 10),
                    Padding = new Thickness(20),
                    Background = Brushes.White,
                    BorderBrush = Brushes.Red,
                    BorderThickness = new Thickness(2),
                    CornerRadius = new CornerRadius(10)
                };

                var stack = new StackPanel();

                stack.Children.Add(new TextBlock
                {
                    Text = $"📝 {r.Title}",
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.DarkRed
                });

                stack.Children.Add(new TextBlock
                {
                    Text = $"📅 {r.StartTime:dd/MM/yyyy HH:mm} → {r.EndTime:dd/MM/yyyy HH:mm}",
                    Margin = new Thickness(0, 4, 0, 0),
                    Foreground = Brushes.DimGray
                });

                stack.Children.Add(new TextBlock
                {
                    Text = $"👥 Max People: {r.MaxPeople}",
                    Margin = new Thickness(0, 4, 0, 0),
                    Foreground = Brushes.Black
                });

                stack.Children.Add(new TextBlock
                {
                    Text = $"🔥 Priority: {MapPriority(r.PriorityId)}",
                    Margin = new Thickness(0, 4, 0, 0),
                    Foreground = MapPriorityColor(r.PriorityId)
                });

                stack.Children.Add(new TextBlock
                {
                    Text = $"🩸 Blood Groups: {string.Join(", ", r.RequestBloodGroups.Select(bg => MapBloodGroup(bg.BloodGroupId)))}",
                    Margin = new Thickness(0, 4, 0, 10),
                    Foreground = Brushes.DarkSlateBlue
                });

                var btn = new Button
                {
                    Content = "Apply to Donate",
                    Background = Brushes.Red,
                    Foreground = Brushes.White,
                    Padding = new Thickness(10, 5, 10, 5),
                    Cursor = System.Windows.Input.Cursors.Hand,
                    Tag = r.Id,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                btn.Click += Apply_Click;

                stack.Children.Add(btn);
                border.Child = stack;
                RequestList.Children.Add(border);
            }
        }

        private string MapPriority(int id) => id switch
        {
            1 => "Low",
            2 => "Medium",
            3 => "High",
            _ => "Unknown"
        };

        private Brush MapPriorityColor(int id) => id switch
        {
            1 => Brushes.Green,
            2 => Brushes.Orange,
            3 => Brushes.Red,
            _ => Brushes.Gray
        };

        private string MapBloodGroup(int id) => id switch
        {
            1 => "O+",
            2 => "O-",
            3 => "A+",
            4 => "A-",
            5 => "B+",
            6 => "B-",
            7 => "AB+",
            8 => "AB-",
            _ => "Unknown"
        };

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var requestId = button?.Tag;
            MessageBox.Show($"Bạn đã chọn tham gia yêu cầu có ID:\n{requestId}", "Apply", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
