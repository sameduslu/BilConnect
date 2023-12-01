using BilConnect.Data;
using BilConnect.Data.ViewModels;
using BilConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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


        public async Task AddNewPostAsync(NewPostVM data)
        {
            var newPost = new Post()
            {
                Title = data.Title,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                PostDate = DateTime.Now,
                UserId =  data.UserId // Set UserId first.FindFirstValue(ClaimTypes.NameIdentifier); // Set UserId first,
        };

            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(NewPostVM data)
        {
            var dbPost = await _context.Posts.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbPost != null)
            {
                dbPost.Title = data.Title;
                dbPost.Description = data.Description;
                dbPost.Price = data.Price;
                dbPost.ImageURL = data.ImageURL;
                dbPost.PostDate = data.PostDate;
                dbPost.UserId = data.UserId;
                await _context.SaveChangesAsync();

            }
            await _context.SaveChangesAsync();

        }
    }
}
