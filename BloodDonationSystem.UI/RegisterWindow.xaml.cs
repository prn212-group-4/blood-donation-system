using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using System.Windows;
using System.Windows.Controls;


namespace BloodDonationSystem.UI
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            string phone = PhoneTextBox.Text.Trim();
            string gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString().ToLower();
            DateTime? birthday = BirthdayPicker.SelectedDate;
            string address = AddressTextBox.Text.Trim();
            string bloodGroup = (BloodGroupComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            // Validate cơ bản
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(gender) || birthday == null ||
                string.IsNullOrEmpty(address) || string.IsNullOrEmpty(bloodGroup))
            {
                new CustomMessageWindow("Registration Failed", "Please fill in all fields.", AlertType.Warning).ShowDialog();
                return;
            }
            string normalizedGroup = bloodGroup switch
            {
                "O+" => "o_plus",
                "O-" => "o_minus",
                "A+" => "a_plus",
                "A-" => "a_minus",
                "B+" => "b_plus",
                "B-" => "b_minus",
                "AB+" => "ab_plus",
                "AB-" => "ab_minus",
                _ => null
            };

            using (var context = new BloodDonationDbContext())
            {
                if (context.Accounts.Any(a => a.Email == email))
                {
                    new CustomMessageWindow("Registration Failed", "Email already exists.", AlertType.Error).ShowDialog();
                    return;
                }

                var bloodGroupEntity = context.BloodGroups.FirstOrDefault(bg => bg.Name == normalizedGroup);
                if (bloodGroupEntity == null)
                {
                    new CustomMessageWindow("Registration Failed", "Invalid blood group selected.", AlertType.Error).ShowDialog();
                    return;
                }

                var account = new Account
                {
                    Id = Guid.NewGuid(),
                    Role = "member",
                    Email = email,
                    Password = password,
                    Phone = phone,
                    Name = name,
                    Gender = gender,
                    Address = address,
                    Birthday = birthday.Value,
                    BloodGroupId = bloodGroupEntity.Id,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                context.Accounts.Add(account);
                context.SaveChanges();
            }

            new CustomMessageWindow("Registration Successful", "You can now log in with your new account.", AlertType.Success).ShowDialog();

            var login = new LoginWindow();
            login.Show();
            this.Close();
        }

    }
}
