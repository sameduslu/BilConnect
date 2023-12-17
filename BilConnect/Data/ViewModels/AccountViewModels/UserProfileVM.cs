using BilConnect.Models;
using BilConnect.Models.PostModels;



namespace BilConnect.Data.ViewModels.AccountViewModels
{
    public class UserProfileVM
    {
        public string Id { get; set; }
        public ApplicationUser User { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
