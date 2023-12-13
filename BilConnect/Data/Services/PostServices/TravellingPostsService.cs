using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;
using System.Linq.Expressions;

namespace BilConnect.Data.Services.PostServices
{
    public class TravellingPostsService : PostsService, ITravellingPostsService
    {

        public TravellingPostsService(AppDbContext context) : base(context)
        {

        }

        public Task AddAsync(TravellingPost entity)
        {
            throw new NotImplementedException();
        }

        public Task AddNewPostAsync(NewTravellingPostVM data)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TravellingPost>> GetAllAsync(params Expression<Func<TravellingPost, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, TravellingPost newEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostAsync(NewTravellingPostVM data)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TravellingPost>> IEntityBaseRepository<TravellingPost>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<TravellingPost> IEntityBaseRepository<TravellingPost>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
