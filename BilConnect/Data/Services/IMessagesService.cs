using BilConnect.Data.Base;
using BilConnect.Data.ViewModels;
using BilConnect.Models;

namespace BilConnect.Data.Services
{
    public interface IMessagesService : IEntityBaseRepository<Message>
    {

        Task<Message> GetMessageByIdAsync(int id);
        Task AddNewMessageAsync(MessageVM data);

    }
}
