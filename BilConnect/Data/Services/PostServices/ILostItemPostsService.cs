using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;

namespace BilConnect.Data.Services.PostServices
{
    public interface ILostItemPostsService : IEntityBaseRepository<LostItemPost>
    {
        Task AddNewPostAsync(NewLostItemPostVM data);
        Task UpdatePostAsync(NewLostItemPostVM data);
    }
}
