using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BloodDonationSystem.UI
{
    public partial class StaffExtractDetail : Window
    {
        private readonly BloodDonationDbContext _context = new();
        private readonly Guid _donationId;
        private Donation _donation;
        private List<BloodBag> _pendingBags = new();

        public StaffExtractDetail(Guid donationId)
        {
            InitializeComponent();
            _donationId = donationId;
            LoadData();
        }

        private void LoadData()
        {
            _donation = _context.Donations
                                .Where(d => d.Id == _donationId)
                                .Select(d => new Donation
                                {
                                    Id = d.Id,
                                    Amount = d.Amount,
                                    CreatedAt = d.CreatedAt,
                                    Type = d.Type!,
                                    Appointment = d.Appointment!
                                })
                                .FirstOrDefault()!;

            DonationInfoText.Text = $"{_donation.Type?.Name} | {_donation.Amount}ml | Created: {_donation.CreatedAt:g}";

            // Load Component list
            var components = _context.BloodComponents.ToList();
            ComponentComboBox.ItemsSource = components;
            ComponentComboBox.DisplayMemberPath = "Name";
            ComponentComboBox.SelectedIndex = 0;
        }

        private void AddComponent_Click(object sender, RoutedEventArgs e)
        {
            if (ComponentComboBox.SelectedItem is not BloodComponent selectedComponent)
            {
                MessageBox.Show("❗ Please select a component.");
                return;
            }

            if (!int.TryParse(ComponentAmountBox.Text, out int amount) || amount <= 0)
            {
                MessageBox.Show("❗ Invalid amount.");
                return;
            }

            var bloodBag = new BloodBag
            {
                Id = Guid.NewGuid(),
                DonationId = _donationId,
                ComponentId = selectedComponent.Id,
                Amount = amount,
                ExpiredTime = DateTime.Now.AddYears(1), // hoặc logic theo loại
                IsUsed = false
            };

            _pendingBags.Add(bloodBag);

            // UI render
            var text = $"{selectedComponent.Name} - {amount}ml - Expires: {bloodBag.ExpiredTime:d}";
            var label = new TextBlock { Text = text, Margin = new Thickness(5) };
            AddedComponentsPanel.Children.Add(label);

            ComponentAmountBox.Text = "";
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            if (_pendingBags.Count == 0)
            {
                MessageBox.Show("❗ No components added!");
                return;
            }

            _context.BloodBags.AddRange(_pendingBags);
            _context.SaveChanges();

            MessageBox.Show("✅ Donation extracted and saved!");
            Close();
        }
    }
}
