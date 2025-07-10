using BloodDonationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using BloodDonationSystem.DAL.Data;

namespace BloodDonationSystem.DAL.Repositories
{
    public class BloodRequestRepository : IBloodRequestRepository
    {
        private readonly BloodDonationDbContext _context;

        public BloodRequestRepository()
        {
        }

        public BloodRequestRepository(BloodDonationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(BloodRequest request, List<int> bloodGroupIds)
        {
            _context.BloodRequests.Add(request);

            foreach (var groupId in bloodGroupIds)
            {
                _context.RequestBloodGroups.Add(new RequestBloodGroup
                {
                    RequestId = request.Id,
                    BloodGroupId = groupId
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
