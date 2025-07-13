using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BloodDonationSystem.UI
{
    public partial class StaffBloodStorage : Window
    {
        private readonly BloodDonationDbContext _context = new();

        public StaffBloodStorage()
        {
            InitializeComponent();
            LoadBloodBags();
        }

        private void LoadBloodBags()
        {
            var bloodBags = _context.BloodBags
                .Include(bb => bb.Component)
                .Include(bb => bb.Donation)
                .ToList();

            BloodBagList.ItemsSource = bloodBags;
        }

        private void MarkAsUsed_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is BloodBag bloodBag)
            {
                var result = MessageBox.Show("Mark this blood bag as used?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var bag = _context.BloodBags.FirstOrDefault(x => x.Id == bloodBag.Id);
                    if (bag != null)
                    {
                        bag.IsUsed = true;
                        _context.SaveChanges();
                        LoadBloodBags();
                    }
                }
            }
        }
    }
}
