using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;

namespace BilConnect.Data.Services.PostServices
{
    public interface ITravellingPostsService : IEntityBaseRepository<TravellingPost>
    {
        Task AddNewPostAsync(NewTravellingPostVM data);
        Task UpdatePostAsync(NewTravellingPostVM data);
    }
}
