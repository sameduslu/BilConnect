using BilConnect.Data.Enums;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;

namespace BilConnect.Data.Services.PostServices
{
    public static class PostViewModelFactory
    {
        public static NewPostVM CreateViewModel(Post post)
        {
            var viewModel = new NewPostVM
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                ImageURL = post.ImageURL,
                PostDate = post.PostDate,
                PostStatus = post.PostStatus,
                UserId = post.UserId,
                PostType = DeterminePostType(post)
            };

            if (post is SellingPost sellingPost)
            {
                viewModel.PriceS = sellingPost.Price;
            }

            // No additional fields for DonationPost, but you can add logic here if needed in the future

            return viewModel;
        }

        private static PostType DeterminePostType(Post post)
        {
            if (post is SellingPost)
            {
                return PostType.SellingPost;
            }
            else if (post is DonationPost)
            {
                return PostType.DonationPost;
            }
            // Add more conditions for other post types
            return PostType.SellingPost; // Default or throw an exception if appropriate
        }
    }

}
