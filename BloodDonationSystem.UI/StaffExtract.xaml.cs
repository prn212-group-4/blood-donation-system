using BloodDonationSystem.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BloodDonationSystem.UI
{
    public partial class StaffExtract : Window
    {
        private readonly BloodDonationDbContext _context = new();

        public StaffExtract()
        {
            InitializeComponent();
            LoadDonations();
        }

        private void LoadDonations()
        {
            var donations = _context.Donations
                .Include(d => d.Type)
                .Include(d => d.Appointment)
                    .ThenInclude(a => a.Member)
                .Select(d => new
                {
                    DonationId = d.Id,
                    MemberName = d.Appointment.Member.Name,
                    DonationTypeName = d.Type.Name,
                    Amount = d.Amount,
                    CreatedAtFormatted = d.CreatedAt.ToString("dd/MM/yyyy HH:mm")
                })
                .ToList();

            DonationList.ItemsSource = donations;
        }

        private void ViewDetail_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is Guid donationId)
            {
                var detailWindow = new StaffExtractDetail(donationId);
                detailWindow.ShowDialog();
                LoadDonations();
            }
        }
    }
}
