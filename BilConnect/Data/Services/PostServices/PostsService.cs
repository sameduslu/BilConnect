using BilConnect.Data;
using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models;
using BilConnect.Models.PostModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BilConnect.Data.Services.PostServices
{
    public class PostsService : EntityBaseRepository<Post>, IPostsService
    {
        private readonly AppDbContext _context;

        public PostsService(AppDbContext context) : base(context)
        {
            _context = context;
        }



        public async Task AddNewPostAsync(NewPostVM data)
        {
            var newPost = new Post()
            {
                Title = data.Title,
                Description = data.Description,
                ImageURL = data.ImageURL,
                PostDate = DateTime.Now,
                PostStatus = Enums.PostStatus.Available,
                UserId = data.UserId // Set UserId first.FindFirstValue(ClaimTypes.NameIdentifier); // Set UserId first,
            };

            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            var postDetails = _context.Posts
                  .Include(u => u.User)
                  .FirstOrDefaultAsync(n => n.Id == id);

            return await postDetails;

        }

        public async Task UpdatePostAsync(NewPostVM data)
        {
            var dbPost = await _context.Posts.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbPost != null)
            {
                dbPost.Title = data.Title;
                dbPost.Description = data.Description;
                dbPost.ImageURL = data.ImageURL;
                dbPost.PostDate = data.PostDate;
                dbPost.PostStatus = data.PostStatus;
                dbPost.UserId = data.UserId;
                await _context.SaveChangesAsync();

            }
            await _context.SaveChangesAsync();

        }
    }
}
