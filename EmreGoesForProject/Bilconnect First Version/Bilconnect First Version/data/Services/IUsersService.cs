using Bilconnect_First_Version.Models;

namespace Bilconnect_First_Version.data.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);

        Task AddAsync(User post);

        Task<User> UpdateAsync(string id, User newUser);

        Task DeleteAsync(string id);
    }
}
