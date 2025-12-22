using BloodDonor.Mvc.Models.Entitites;
using System.Linq.Expressions;

namespace BloodDonor.Mvc.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Query();
    }
}
