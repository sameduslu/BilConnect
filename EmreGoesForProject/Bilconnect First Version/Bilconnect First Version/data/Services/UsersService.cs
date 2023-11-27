using Bilconnect_First_Version.Models;
using Microsoft.EntityFrameworkCore;

namespace Bilconnect_First_Version.data.Services
{
    public class UsersService : IUsersService
    {
        private readonly AppDbContext _context;

        public UsersService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(n => n.Id == id);
            _context.Users.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _context.Users.ToListAsync();
            return result;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<User> UpdateAsync(string id, User newUser)
        {
            newUser.Id = id;
            _context.Update(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }
    }
}
