using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;

namespace BilConnect.Data.Services.PostServices
{
    public interface ISellingPostsService : IEntityBaseRepository<SellingPost>
    {
        Task AddNewPostAsync(NewSellingPostVM data);
        Task UpdatePostAsync(NewSellingPostVM data);
    }
}
