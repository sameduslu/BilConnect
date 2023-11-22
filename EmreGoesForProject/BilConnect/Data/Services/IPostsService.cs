using BilConnect.Data.ViewModels;
using BilConnect.Models;

namespace BilConnect.data.Services
{
    public interface IPostsService
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);

        Task AddAsync(Post post);

        Task<Post> UpdateAsync(int id, Post newPost);

        Task DeleteAsync(int id);

        Task AddNewPostAsync(NewPostVM data);
        Task UpdatePostAsync(NewPostVM data);

    }
}
