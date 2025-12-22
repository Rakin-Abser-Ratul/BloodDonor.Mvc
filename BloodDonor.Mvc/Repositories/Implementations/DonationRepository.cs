using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Models.Entitites;
using BloodDonor.Mvc.Repositories.Interfaces;

namespace BloodDonor.Mvc.Repositories.Implementations
{
    public class DonationRepository : IDonationRepository
    {
        public void Add(Donation donation)
        {
            throw new NotImplementedException();
        }

        public void Delete(Donation donation)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Donation>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Donation?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Donation donation)
        {
            throw new NotImplementedException();
        }
    }
}
