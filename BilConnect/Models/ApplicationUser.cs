using BilConnect.Models.PostModels;
using BilConnect.Models.ReportModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BilConnect.Models
{
    public class ApplicationUser : IdentityUser
    {

        public bool IsSuspended { get; set; }

        [Display(Name = "Users name")]
        public string FullName { get; set; }

        public string? ImageURL { get; set; }

        public List<Post>? Posts { get; set; }

        public List<PostReport>? PostReports { get; set; }

        public List<Chat>? SenderChats { get; set; }
        public List<Chat>? ReceiverChats { get; set; } 
        public List<Message>? Messages { get; set; }
    }
}
