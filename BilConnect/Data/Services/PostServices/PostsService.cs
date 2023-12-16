using BilConnect.Data;
using BilConnect.Data.Base;
using BilConnect.Data.Enums;
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
        private readonly IPostFactory _postFactory;

        public PostsService(AppDbContext context, IPostFactory postFactory) : base(context)
        {
            _context = context;
            _postFactory = postFactory;
        }



        public async Task AddNewPostAsync(NewPostVM data)
        {
            var newPost = _postFactory.CreatePost(data);

            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();
        }



        public async Task<Post> GetPostByIdAsync(int id)
        {
            // Fetch the post. EF Core will automatically determine the type (Post, SellingPost, etc.)
            var post = await _context.Posts
                                     .Include(u => u.User)
                                     .FirstOrDefaultAsync(n => n.Id == id);

            return post; // This will be the correct type (SellingPost, DonationPost, etc.)
        }


        public async Task UpdatePostAsync(NewPostVM data)
        {
            var dbPost = await _context.Posts.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbPost == null)
            {
                // Handle the case where the post is not found
                return;
            }

            // Update common properties
            dbPost.Title = data.Title;
            dbPost.Description = data.Description;
            dbPost.ImageURL = data.ImageURL;
            dbPost.PostDate = data.PostDate;
            dbPost.PostStatus = data.PostStatus;
            dbPost.UserId = data.UserId;

            // Handle specific properties based on PostType
            if (data.PostType == PostType.SellingPost && dbPost is SellingPost sellingPost)
            {
                sellingPost.Price = data.PriceS ?? 0; // Ensure Price is not null, or handle accordingly
            }
            else if (data.PostType == PostType.DonationPost && dbPost is DonationPost donationPost)
            {
                // Update DonationPost specific properties if any
            }

            await _context.SaveChangesAsync();
        }
    }
}
