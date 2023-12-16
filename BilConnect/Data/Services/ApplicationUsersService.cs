using BilConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace BilConnect.Data.Services
{
    public class ApplicationUsersService : IApplicationUsersService
    {
        private readonly AppDbContext _context;

        public ApplicationUsersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _context.Users
                         .Include(p => p.PostReports)
                             .ThenInclude(pr => pr.ReportedPost) 
                         .Include(u => u.Posts)
                         .Include(u => u.ClubEvents)
                         .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ApplicationUser> UpdateAsync(string id, ApplicationUser newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser));
            }

            var userToUpdate = await _context.Users.FindAsync(id);
            if (userToUpdate != null)
            {
                // Update the user properties here as needed
                userToUpdate.UserName = newUser.UserName;
                userToUpdate.Email = newUser.Email;

                await _context.SaveChangesAsync();
                return userToUpdate;
            }

            return null; // User with the specified id was not found
        }
    }
}
