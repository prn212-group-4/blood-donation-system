using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using System;
using System.Linq;

namespace BloodDonationSystem.Services
{
    public class BloodRequestService
    {
        private readonly BloodDonationDbContext _context;

        public BloodRequestService(BloodDonationDbContext context)
        {
            _context = context;
        }

        public void CreateBloodRequest(BloodRequest request)
        {
            foreach (var bg in request.RequestBloodGroups)
            {
                bg.RequestId = request.Id;
            }

            _context.BloodRequests.Add(request);
            _context.SaveChanges();
        }

    }
}
