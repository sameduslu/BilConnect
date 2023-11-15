using Bilconnect_First_Version.Models;

namespace Bilconnect_First_Version.data.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);

        Task AddAsync(User post);

        Task<User> UpdateAsync(int id, User newUser);

        Task DeleteAsync(int id);
    }
}
