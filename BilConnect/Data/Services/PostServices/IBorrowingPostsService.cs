using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;

namespace BilConnect.Data.Services.PostServices
{
    public interface IBorrowingPostsService : IEntityBaseRepository<BorrowingPost>
    {
        Task AddNewPostAsync(NewBorrowingPostVM data);
        Task UpdatePostAsync(NewBorrowingPostVM data);
    }
}
