using System.Windows;
using BloodDonationSystem.Models.Entities;

namespace BloodDonationSystem.UI
{
    public partial class StaffWindow : Window
    {
        private readonly Account _currentStaff;

        public StaffWindow(Account account)
        {
            InitializeComponent();
            WelcomeText.Text = $"👋 Welcome back, {account.Name}!";
            CurrentTimeText.Text = $"Today: {DateTime.Now.ToString("dddd, dd MMM yyyy - HH:mm")}";
        }

        private void CreateBloodRequest_Click(object sender, RoutedEventArgs e)
        {
            var requestWindow = new CreateBloodRequest(_currentStaff);
            requestWindow.ShowDialog();
        }

        private void ManageQuestion_Click(object sender, RoutedEventArgs e)
        {
            var questionWindow = new QuestionManagement();
            questionWindow.Show();
        }
        private void ManageAppointment_Click(object sender, RoutedEventArgs e)
        {
            var window = new StaffAppointmentManagement();
            window.ShowDialog();
        }

        private void ExtractBlood_Click(object sender, RoutedEventArgs e)
        {
            var extractWindow = new StaffExtract();
            extractWindow.ShowDialog();
        }

        private void ViewBloodStorage_Click(object sender, RoutedEventArgs e)
        {
            var window = new StaffBloodStorage();
            window.ShowDialog();
        }

    }
}
