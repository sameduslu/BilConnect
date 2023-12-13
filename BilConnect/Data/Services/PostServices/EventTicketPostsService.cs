using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;
using System.Linq.Expressions;

namespace BilConnect.Data.Services.PostServices
{
    public class EventTicketPostsService : PostsService, IEventTicketPostsService
    {
        public EventTicketPostsService(AppDbContext context) : base(context)
        {

        }
        public Task AddAsync(EventTicketPost entity)
        {
            throw new NotImplementedException();
        }

        public Task AddNewPostAsync(NewEventTicketPostVM data)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventTicketPost>> GetAllAsync(params Expression<Func<EventTicketPost, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, EventTicketPost newEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostAsync(NewEventTicketPostVM data)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<EventTicketPost>> IEntityBaseRepository<EventTicketPost>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<EventTicketPost> IEntityBaseRepository<EventTicketPost>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
