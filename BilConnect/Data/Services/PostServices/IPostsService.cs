using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;

namespace BilConnect.Data.Services.PostServices
{
    public interface IPostsService : IEntityBaseRepository<Post>
    {

        Task<Post> GetPostByIdAsync(int id);
        Task AddNewPostAsync(NewPostVM data);
        Task UpdatePostAsync(NewPostVM data);

    }
}
