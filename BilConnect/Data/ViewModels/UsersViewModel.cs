using BilConnect.Models;

namespace BilConnect.Data.ViewModels
{
    public class UsersViewModel
    {
        public List<ApplicationUser> AdminUsers { get; set; }
        public List<ApplicationUser> ClubAccountUsers { get; set; }
        public List<ApplicationUser> RegularUsers { get; set; }
    }
}
