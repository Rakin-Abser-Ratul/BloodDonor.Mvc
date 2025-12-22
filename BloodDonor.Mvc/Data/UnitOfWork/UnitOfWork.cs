using BloodDonor.Mvc.Repositories.Interfaces;

namespace BloodDonor.Mvc.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly BloodDonorDbContext _context;
        public IBloodDonorRepository BloodDonorRepository { get; }

        public IDonationRepository DonationRepository { get; }
        public UnitOfWork(IBloodDonorRepository bloodDonorRepository, IDonationRepository donationRepository, BloodDonorDbContext context)
        {
            BloodDonorRepository = bloodDonorRepository;
            DonationRepository = donationRepository;
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        

        public async Task<int> SaveAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
