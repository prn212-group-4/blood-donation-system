using System.Windows;

namespace BloodDonationSystem.UI
{
    public partial class StaffWindow : Window
    {
        public StaffWindow()
        {
            InitializeComponent();
        }

        private void CreateBloodRequest_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked Create Blood Request!");
        }
    }
}
