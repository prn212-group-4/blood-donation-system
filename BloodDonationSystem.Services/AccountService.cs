using BloodDonationSystem.Models.Entities;
using BloodDonationSystem.Repositories;
using System.Collections.Generic;

namespace BloodDonationSystem.Services
{
    public class AccountService
    {
        private readonly AccountRepository _repository = new();

        public List<Account> GetAllAccounts()
        {
            return _repository.GetAllAccounts();
        }
    }
}
