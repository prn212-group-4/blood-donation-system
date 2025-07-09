using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BloodDonationSystem.Repositories
{
    public class AccountRepository
    {
        private readonly BloodDonationDbContext _context;

        public AccountRepository()
        {
            _context = new BloodDonationDbContext();
        }

        public List<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }
    }
}
