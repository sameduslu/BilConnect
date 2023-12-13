using BilConnect.Data;
using BilConnect.Data.Base;
using BilConnect.Data.ViewModels;
using BilConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BilConnect.Data.Services
{
    public class MessagesService : EntityBaseRepository<Message>, IMessagesService
    {
        private readonly AppDbContext _context;

        public MessagesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMessageAsync(MessageVM data)
        {
            var newMessage = new Message()
            {
                Content = data.Content,
                Timestamp = DateTime.UtcNow,
                ChatId = data.ChatId,
                SenderUserId = data.SenderUserId,
            };
            await _context.Messages.AddAsync(newMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            var messageDetails = _context.Messages
                  .Include(m => m.Id)
                  .Include(m => m.Chat)
                  .Include(m => m.Sender)
                  .FirstOrDefaultAsync(n => n.Id == id);
            return await messageDetails;
        }
    }
}
