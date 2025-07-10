using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BloodDonationSystem.UI
{
    /// <summary>
    /// Interaction logic for CreateBloodRequest.xaml
    /// </summary>
    public partial class CreateBloodRequest : Window
    {
        public CreateBloodRequest()
        {
            InitializeComponent();
        }
        private void SubmitRequest_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Blood Request Submitted!");
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var staffWindow = new StaffWindow();
            staffWindow.Show();
            this.Close();
        }
    }
}
