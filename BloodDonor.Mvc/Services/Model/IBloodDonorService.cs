using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Models.Entitites;
using BloodDonor.Mvc.Models.ViewModel;
using System.Linq.Expressions;

namespace BloodDonor.Mvc.Services.Model
{
    public interface IBloodDonorService
    {
        Task<IEnumerable<BloodDonorEntity>> GetAllAsync();
        Task<List<BloodDonorEntity>> GetFilteredBloodDonorAsync(FilterDonorModel filter);
        Task<BloodDonorEntity?> GetByIdAsync(int id);
        
        Task AddAsync(BloodDonorEntity blooddonor);
        Task UpdateAsync(BloodDonorEntity blooddonor);
        Task DeleteAsync(int id);
    }
}
