using System;
using System.Globalization;
using System.Windows;
using BloodDonationSystem.Models.Entities;

namespace BloodDonationSystem.UI
{
    public partial class MemberWindow : Window
    {
        private readonly Account _currentMember;

        public MemberWindow(Account member)
        {
            InitializeComponent();
            _currentMember = member;

            // Tiêu đề cửa sổ
            this.Title = $"Welcome, {_currentMember.Name ?? "Member"}";

            // Câu chào cá nhân hóa
            WelcomeText.Text = $"👋 Welcome back, {_currentMember.Name}!";

            var today = DateTime.Now;
            var formattedDate = today.ToString("dddd, dd MMM yyyy - HH:mm", CultureInfo.InvariantCulture);
            TodayText.Text = $"Today: {formattedDate}";
        }

        private void ViewBloodRequestButton_Click(object sender, RoutedEventArgs e)
        {
            var viewWindow = new ViewBloodRequest(_currentMember);
            viewWindow.ShowDialog();
        }

        private void ViewAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new MemberAppointment(_currentMember.Id);
            window.ShowDialog();
        }
        private void DonationHistory_Click(object sender, RoutedEventArgs e)
        {
            new CustomMessageWindow("Coming Soon", "Donation history feature is under development.", AlertType.Info).ShowDialog();
        }


    }
}
