using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Models.Entitites;

namespace BloodDonor.Mvc.Repositories.Interfaces
{
    public interface IDonationRepository
    {
        Task<IEnumerable<Donation>> GetAllAsync();
        Task<Donation?> GetByIdAsync(int id);
       
        void Add(Donation donation);
        void Update(Donation donation);
        void Delete(Donation donation);
    }
}
