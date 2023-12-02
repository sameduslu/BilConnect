using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BilConnect.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Users name")]
        public string FullName { get; set; }

        public List<Post>? Posts { get; set; }
    }
}
