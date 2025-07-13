using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Windows.Media.Animation;

namespace BloodDonationSystem.UI
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.Resources["FadeInStoryboard"];
            sb.Begin(MainPanel);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                new CustomMessageWindow("Login Failed", "Please enter both email and password.", AlertType.Error).ShowDialog();
                return;
            }

            using (var context = new BloodDonationDbContext())
            {
                var account = context.Accounts
                    .FirstOrDefault(a => a.Email == email && a.Password == password && a.IsActive);

                if (account != null)
                {
                    this.Hide();
                    new CustomMessageWindow("Welcome", $"Hi {account.Name}!", AlertType.Success).ShowDialog();

                    Window nextWindow;
                    switch (account.Role.ToLower())
                    {
                        case "staff":
                            nextWindow = new StaffWindow(account);
                            break;
                        case "member":
                            nextWindow = new MemberWindow(account);
                            break;
                        default:
                            new CustomMessageWindow("Login Failed", "Invalid email or password.", AlertType.Error).ShowDialog();
                            return;
                    }

                    nextWindow.Show();
                }
                else
                {
                    new CustomMessageWindow("Login Failed", "Invalid email or password.", AlertType.Error).ShowDialog();
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

        private void RegisterLink_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
