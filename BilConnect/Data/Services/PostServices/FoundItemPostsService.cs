using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;
using System.Linq.Expressions;

namespace BilConnect.Data.Services.PostServices
{
    public class FoundItemPostsService : PostsService, IFoundItemPostsService
    {

        public FoundItemPostsService(AppDbContext context) : base(context)
        {

        }

        public Task AddAsync(FoundItemPost entity)
        {
            throw new NotImplementedException();
        }

        public Task AddNewPostAsync(NewFoundItemPostVM data)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FoundItemPost>> GetAllAsync(params Expression<Func<FoundItemPost, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, FoundItemPost newEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostAsync(NewFoundItemPostVM data)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<FoundItemPost>> IEntityBaseRepository<FoundItemPost>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<FoundItemPost> IEntityBaseRepository<FoundItemPost>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
