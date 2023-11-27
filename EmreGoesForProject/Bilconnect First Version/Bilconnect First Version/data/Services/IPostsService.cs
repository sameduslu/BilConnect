using Bilconnect_First_Version.Models;

namespace Bilconnect_First_Version.data.Services
{
    public interface IPostsService
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);

        Task AddAsync(Post post);

        Task<Post> UpdateAsync(int id, Post newPost);

        Task DeleteAsync(int id);
    }
}
