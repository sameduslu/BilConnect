using BilConnect.Models.PostModels;
using BilConnect.Models.ReportModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BilConnect.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Users name")]
        public string FullName { get; set; }

        public List<Post>? Posts { get; set; }

        public List<PostReport>? PostReports { get; set; }

        public List<Chat>? Chats { get; set; }
    }
}
