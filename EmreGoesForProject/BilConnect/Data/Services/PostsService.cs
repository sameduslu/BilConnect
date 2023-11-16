using BilConnect.Data;
using BilConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace BilConnect.data.Services
{
    public class PostsService : IPostsService
    {
        private readonly AppDbContext _context;

        public PostsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Posts.FirstOrDefaultAsync(n => n.Id == id);
            _context.Posts.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var result = await _context.Posts.Include(p => p.User).ToListAsync();
            return result;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            var result = await _context.Posts.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<Post> UpdateAsync(int id, Post newPost)
        {
            newPost.Id = id;
            _context.Update(newPost);
            await _context.SaveChangesAsync();
            return newPost;
        }
    }
}
