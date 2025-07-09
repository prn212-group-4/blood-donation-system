using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using System.Collections.Generic;
using System.Windows;

namespace BloodDonationSystem.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            using (var context = new BloodDonationDbContext())
            {
                List<Account> accounts = context.Accounts.ToList();
                AccountsGrid.ItemsSource = accounts;
            }
        }
    }
}
