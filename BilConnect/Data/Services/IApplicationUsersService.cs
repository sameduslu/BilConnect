
using BilConnect.Models;

namespace BilConnect.Data.Services
{
    public interface IApplicationUsersService
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string id);

        Task AddAsync(ApplicationUser user);

        Task<ApplicationUser> UpdateAsync(string id, ApplicationUser newUser);

        Task DeleteAsync(string id);
    }
}
