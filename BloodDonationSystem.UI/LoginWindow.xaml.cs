using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace BloodDonationSystem.UI
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new BloodDonationDbContext())
            {
                var account = context.Accounts
                    .FirstOrDefault(a => a.Email == email && a.Password == password && a.IsActive);

                if (account != null)
                {
                    MessageBox.Show($"Welcome, {account.Name}!", "Login Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RegisterText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Registration form not implemented yet.");
        }
        private void EmailTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EmailTextBox.Text == "Email")
            {
                EmailTextBox.Text = "";
                EmailTextBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailTextBox.Text = "Email";
                EmailTextBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }
    }
}
