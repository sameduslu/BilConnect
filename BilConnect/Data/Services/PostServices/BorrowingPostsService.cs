using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;
using System.Linq.Expressions;

namespace BilConnect.Data.Services.PostServices
{
    public class BorrowingPostsService : PostsService, IBorrowingPostsService
    {

        public BorrowingPostsService(AppDbContext context) : base(context)
        {
            
        }
        public Task AddAsync(BorrowingPost entity)
        {
            throw new NotImplementedException();
        }

        public Task AddNewPostAsync(NewBorrowingPostVM data)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BorrowingPost>> GetAllAsync(params Expression<Func<BorrowingPost, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, BorrowingPost newEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostAsync(NewBorrowingPostVM data)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<BorrowingPost>> IEntityBaseRepository<BorrowingPost>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<BorrowingPost> IEntityBaseRepository<BorrowingPost>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
