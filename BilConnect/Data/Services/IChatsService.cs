using BilConnect.Data.Base;
using BilConnect.Data.ViewModels;
using BilConnect.Models;

namespace BilConnect.Data.Services
{
    public interface IChatsService : IEntityBaseRepository<Chat>
    {
        Task<Chat> GetChatByIdAsync(int id);
        Task<List<Chat>> GetAllChatsAsync();
        Task AddNewChatAsync(ChatVM data);
        Task UpdateChatAsync(ChatVM data);

    }
}
