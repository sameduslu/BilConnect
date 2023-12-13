using BilConnect.Data;
using BilConnect.Data.Base;
using BilConnect.Data.ViewModels;
using BilConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BilConnect.Data.Services
{
    public class ChatsService : EntityBaseRepository<Chat>, IChatsService
    {
        private readonly AppDbContext _context;

        public ChatsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewChatAsync(ChatVM data)
        {
            var newChat = new Chat()
            {
                RelatedPostId = data.RelatedPostId
            };
            await _context.Chats.AddAsync(newChat);
            await _context.SaveChangesAsync();
            var newUserChat1 = new UserChat()
            {
                UserId = data.UserId,
                ChatId = newChat.Id,
            };
            var newUserChat2 = new UserChat()
            {
                UserId = data.ReceiverId,
                ChatId = newChat.Id,
            };
            await _context.UserChats.AddAsync(newUserChat1);
            await _context.UserChats.AddAsync(newUserChat2);
            await _context.SaveChangesAsync();
        }

        public async Task<Chat> GetChatByIdAsync(int id)
        {
            var chatDetails = _context.Chats
                  .Include(u => u.UserChats)
                  .Include(c => c.Messages)
                  .Include(c => c.RelatedPost)
                  .FirstOrDefaultAsync(n => n.Id == id);
            return await chatDetails;
        }
        public List<Chat> GetChatsForUser(string userId)
        {
            var userChats = _context.UserChats
                .Where(uc => uc.UserId == userId)
                .Include(uc => uc.Chat)
                .ToList();

            var chatsForUser = userChats.Select(uc => uc.Chat).ToList();
            return chatsForUser;
        }
    }
}
