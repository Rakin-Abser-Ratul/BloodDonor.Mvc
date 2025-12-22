using BloodDonor.Mvc.Data.UnitOfWork;
using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Services.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BloodDonor.Mvc.Services.Implementations
{
    public class BloodDonorService : IBloodDonorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BloodDonorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BloodDonorEntity>> GetAllAsync()
        {
            return await _unitOfWork.BloodDonorRepository.GetAllAsync();
        }

        public async Task<BloodDonorEntity?> GetByIdAsync(int id)
        {
            return await _unitOfWork.BloodDonorRepository.GetByIdAsync(id);
        }



        public async Task AddAsync(BloodDonorEntity blooddonor)
        {
            _unitOfWork.BloodDonorRepository.Add(blooddonor);
             await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(BloodDonorEntity blooddonor)
        {
            _unitOfWork.BloodDonorRepository.Update(blooddonor);
            await _unitOfWork.SaveAsync();
         }

        public async Task DeleteAsync(int id)
        {

            var donor = await _unitOfWork.BloodDonorRepository.GetByIdAsync(id);
            if (donor != null)
            {
                _unitOfWork.BloodDonorRepository.Delete(donor);
                await _unitOfWork.SaveAsync();
            }
        }

        public Task<IEnumerable<BloodDonorEntity>> FindAllAsync(Expression<Func<BloodDonorEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BloodDonorEntity>> GetFilteredBloodDonorAsync(FilterDonorModel filter)
        {
            var query = _unitOfWork.BloodDonorRepository.Query();
            if (!string.IsNullOrEmpty(filter.bloodGroup))
            {
                query = query.Where(d => d.BloodGroup.ToString() == filter.bloodGroup);
            }
            if (!string.IsNullOrEmpty(filter.address))
            {
                query = query.Where(d => d.Address != null && (" " + d.Address + " ").Contains(" " + filter.address + " "));
            }
           
            return await query.ToListAsync();
        }
        public static bool IsEligible(BloodDonorEntity donor)
        {
            if (donor.Weight < 45 || donor.Weight > 200)
            {
                return false;
            }
            if (donor.LastDonationDate.HasValue)
            {
                var daysSinceLastDonation = (DateTime.Today - donor.LastDonationDate.Value).TotalDays;
                if (daysSinceLastDonation < 90)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
