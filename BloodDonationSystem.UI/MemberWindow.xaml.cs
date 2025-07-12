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
            this.Title = $"Welcome, {_currentMember.Name ?? "Member"}";
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

    }
}
