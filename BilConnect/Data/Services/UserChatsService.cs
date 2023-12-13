using BilConnect.Data;
using BilConnect.Data.Base;
using BilConnect.Data.ViewModels;
using BilConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BilConnect.Data.Services
{
    public class UserChatsService : EntityBaseRepository<UserChat>, IUserChatsService
    {
        private readonly AppDbContext _context;

        public UserChatsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewUserChatAsync(UserChatVM data)
        {
            throw new NotImplementedException();
        }

        public async Task<UserChat> GetUserChatByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateUserChatAsync(UserChatVM data)
        {
            throw new NotImplementedException();

        }
    }
}
