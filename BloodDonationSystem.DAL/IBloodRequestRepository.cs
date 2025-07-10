using BloodDonationSystem.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloodDonationSystem.DAL.Repositories
{
    public interface IBloodRequestRepository
    {
        Task CreateAsync(BloodRequest request, List<int> bloodGroupIds);
    }
}
