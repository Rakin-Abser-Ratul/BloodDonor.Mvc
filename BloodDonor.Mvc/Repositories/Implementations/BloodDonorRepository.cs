using BloodDonor.Mvc.Data;
using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Models.Entitites;
using BloodDonor.Mvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BloodDonor.Mvc.Repositories.Implementations
{
    public class BloodDonorRepository : Repository<BloodDonorEntity>, IBloodDonorRepository
    {
        /* private readonly BloodDonorDbContext _context;
         public BloodDonorRepository(BloodDonorDbContext context)
         {
             _context = context;
         }


         public void Add(BloodDonorEntity blooddonor)
         {
            _context.BloodDonors.Add(blooddonor);
         }

         public void Delete(BloodDonorEntity blooddonor)
         {
            _context.BloodDonors.Remove(blooddonor);
         }
         public async Task<IEnumerable<BloodDonorEntity>> FindAllAsync(Expression<Func<BloodDonorEntity, bool>> predicate)
         {
             return await Task.FromResult(_context.BloodDonors.Where(predicate).AsEnumerable());
         }



         public async Task<IEnumerable<BloodDonorEntity>> GetAllAsync()
         {
             return await _context.BloodDonors.ToListAsync();
         }
         public async Task<BloodDonorEntity?> GetByIdAsync(int id)
         {
             return await _context.BloodDonors.FindAsync(id);

         }


         public void Update(BloodDonorEntity blooddonor)
         {
             _context.BloodDonors.Update(blooddonor);
         }*/
        public BloodDonorRepository(BloodDonorDbContext context) : base(context)
        {
        }
    }
}
