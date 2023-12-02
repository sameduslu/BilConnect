using BilConnect.Data.Base;
using BilConnect.Data.ViewModels;
using BilConnect.Models;

namespace BilConnect.data.Services
{
    public interface IPostsService : IEntityBaseRepository<Post>
    {

        Task<Post> GetPostByIdAsync(int id);
        Task AddNewPostAsync(NewPostVM data);
        Task UpdatePostAsync(NewPostVM data);

    }
}
