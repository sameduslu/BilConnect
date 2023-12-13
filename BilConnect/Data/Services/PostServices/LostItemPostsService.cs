using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;
using System.Linq.Expressions;

namespace BilConnect.Data.Services.PostServices
{
    public class LostItemPostsService : PostsService, ILostItemPostsService
    {
        public LostItemPostsService(AppDbContext context) : base(context)
        {

        }

        public Task AddAsync(LostItemPost entity)
        {
            throw new NotImplementedException();
        }

        public Task AddNewPostAsync(NewLostItemPostVM data)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LostItemPost>> GetAllAsync(params Expression<Func<LostItemPost, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, LostItemPost newEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostAsync(NewLostItemPostVM data)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<LostItemPost>> IEntityBaseRepository<LostItemPost>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<LostItemPost> IEntityBaseRepository<LostItemPost>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
