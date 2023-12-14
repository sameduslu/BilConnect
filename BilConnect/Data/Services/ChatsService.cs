using BilConnect.Data;
using BilConnect.Data.Base;
using BilConnect.Data.ViewModels;
using BilConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
                RelatedPostId = data.RelatedPostId,
                ReceiverId = data.ReceiverId,
                UserId = data.UserId,
                SenderLastSeen = data.SenderLastSeen,
                ReceiverLastSeen = data.ReceiverLastSeen
            };
            await _context.Chats.AddAsync(newChat);
            await _context.SaveChangesAsync();
        }

        public async Task<Chat> GetChatByIdAsync(int id)
        {
            var chatDetails = _context.Chats
                  .Include(u => u.User)
                  .Include(u => u.Receiver)
                  .Include(c => c.Messages)
                  .Include(c => c.RelatedPost)
                  .FirstOrDefaultAsync(n => n.Id == id);
            return await chatDetails;
        }

        public async Task<List<Chat>> GetAllChatsAsync()
        {
            var chatDetails = _context.Chats
                  .Include(u => u.User)
                  .Include(u => u.Receiver)
                  .Include(c => c.Messages)
                  .Include(c => c.RelatedPost).ToListAsync();
            return await chatDetails;
        }

        public async Task UpdateChatAsync(ChatVM data)
        {
            var dbChat = await _context.Chats.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dbChat != null)
            {
                dbChat.RelatedPostId = data.RelatedPostId;
                dbChat.ReceiverId = data.ReceiverId;
                dbChat.UserId = data.UserId;
                dbChat.SenderLastSeen = data.SenderLastSeen;
                dbChat.ReceiverLastSeen = data.ReceiverLastSeen;
            }
            await _context.SaveChangesAsync();

        }
    }
}
