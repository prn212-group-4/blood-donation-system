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
    public partial class MemberAppointment : Window
    {
        private readonly BloodDonationDbContext _context = new();
        private readonly Guid _memberId;

        public MemberAppointment(Guid memberId)
        {
            InitializeComponent();
            _memberId = memberId;
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            AppointmentList.Children.Clear();

            var appointments = _context.Appointments
                .Include(a => a.Status)
                .Include(a => a.Request)
                .Where(a => a.MemberId == _memberId)
                .ToList();

            foreach (var a in appointments)
            {
                var border = new Border
                {
                    Background = Brushes.White,
                    BorderBrush = GetStatusBrush(a.Status?.Id ?? 0),
                    BorderThickness = new Thickness(2),
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(0, 10, 0, 10),
                    Padding = new Thickness(15)
                };

                var stack = new StackPanel();

                stack.Children.Add(new TextBlock
                {
                    Text = $"🩸 {a.Request?.Title ?? "Unknown Request"}",
                    FontSize = 18,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = Brushes.Black
                });

                stack.Children.Add(new TextBlock
                {
                    Text = $"📅 {a.Request?.StartTime:dd/MM/yyyy HH:mm}",
                    Margin = new Thickness(0, 5, 0, 0),
                    FontSize = 14,
                    Foreground = Brushes.DimGray
                });

                stack.Children.Add(new TextBlock
                {
                    Text = $"📌 Status: {a.Status?.Name ?? "Unknown"}",
                    Margin = new Thickness(0, 5, 0, 0),
                    FontSize = 14,
                    Foreground = GetStatusBrush(a.Status?.Id ?? 0)
                });

                border.Child = stack;
                AppointmentList.Children.Add(border);
            }
        }

        private Brush GetStatusBrush(int statusId) => statusId switch
        {
            1 => Brushes.OrangeRed,     // Review
            2 => Brushes.Orange,        // Health Check
            3 => Brushes.Green,         // Donation
            4 => Brushes.SteelBlue,     // Done
            5 => Brushes.Gray,          // Canceled
            6 => Brushes.Purple,        // Missed
            _ => Brushes.Black
        };
    }
}
