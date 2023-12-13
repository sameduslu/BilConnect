using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;
using System.Linq.Expressions;

namespace BilConnect.Data.Services.PostServices
{
    public class DonationPostsService : PostsService, IDonationPostsService
    {
        public DonationPostsService(AppDbContext context) : base(context)
        {
            
        }
        public Task AddAsync(DonationPost entity)
        {
            throw new NotImplementedException();
        }

        public Task AddNewPostAsync(NewDonationPostVM data)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DonationPost>> GetAllAsync(params Expression<Func<DonationPost, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, DonationPost newEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostAsync(NewDonationPostVM data)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<DonationPost>> IEntityBaseRepository<DonationPost>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<DonationPost> IEntityBaseRepository<DonationPost>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
