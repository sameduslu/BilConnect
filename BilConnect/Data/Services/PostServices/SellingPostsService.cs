using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;
using System.Linq.Expressions;

namespace BilConnect.Data.Services.PostServices
{
    public class SellingPostsService : PostsService, ISellingPostsService
    {

        public SellingPostsService(AppDbContext context) : base(context)
        {

        }

        public Task AddAsync(SellingPost entity)
        {
            throw new NotImplementedException();
        }

        public Task AddNewPostAsync(NewSellingPostVM data)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SellingPost>> GetAllAsync(params Expression<Func<SellingPost, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, SellingPost newEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostAsync(NewSellingPostVM data)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<SellingPost>> IEntityBaseRepository<SellingPost>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<SellingPost> IEntityBaseRepository<SellingPost>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
