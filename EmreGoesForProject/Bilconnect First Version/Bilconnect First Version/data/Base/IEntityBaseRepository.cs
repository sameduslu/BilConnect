using Bilconnect_First_Version.Models;

namespace Bilconnect_First_Version.data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(int id, T newEntity);

        Task DeleteAsync(int id);
    }
}
