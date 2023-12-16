using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;

namespace BilConnect.Data.Services.PostServices
{
    public interface IPostFactory
    {
        Post CreatePost(NewPostVM viewModel);
    }
}
