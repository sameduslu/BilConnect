using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;

namespace BilConnect.Data.Services.PostServices
{
    public interface IDonationPostsService : IEntityBaseRepository<DonationPost>
    {
        Task AddNewPostAsync(NewDonationPostVM data);
        Task UpdatePostAsync(NewDonationPostVM data);
    }
}
