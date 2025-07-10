using System.Windows;
using BloodDonationSystem.Models.Entities;

namespace BloodDonationSystem.UI
{
    public partial class StaffWindow : Window
    {
        private readonly Account _currentStaff;

        public StaffWindow(Account staff)
        {
            InitializeComponent();
            _currentStaff = staff;
        }

        private void CreateBloodRequest_Click(object sender, RoutedEventArgs e)
        {
            var requestWindow = new CreateBloodRequest(_currentStaff);
            requestWindow.ShowDialog();
        }
    }
}
