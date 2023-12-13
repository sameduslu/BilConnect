using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;

namespace BilConnect.Data.Services.PostServices
{
    public interface IEventTicketPostsService : IEntityBaseRepository<EventTicketPost>
    {
        Task AddNewPostAsync(NewEventTicketPostVM data);
        Task UpdatePostAsync(NewEventTicketPostVM data);
    }
}
