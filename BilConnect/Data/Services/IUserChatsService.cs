using BilConnect.Data.Base;
using BilConnect.Data.ViewModels;
using BilConnect.Models;

namespace BilConnect.Data.Services
{
    public interface IUserChatsService : IEntityBaseRepository<UserChat>
    {

        Task<UserChat> GetUserChatByIdAsync(int id);
        Task AddNewUserChatAsync(UserChatVM data);
        Task UpdateUserChatAsync(UserChatVM data);

    }
}
