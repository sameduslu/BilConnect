using BilConnect.Models.PostModels;
using BilConnect.Models.ReportModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
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
        [DisplayName("Post Reports")]
        public List<PostReport>? PostReports { get; set; }
        [DisplayName("Sender Chats")]
        public List<Chat>? SenderChats { get; set; }
        [DisplayName("Receiver Chats")]
        public List<Chat>? ReceiverChats { get; set; } 
        public List<Message>? Messages { get; set; }
        [DisplayName("Club Events")]
        public List<ClubEvent>? ClubEvents { get; set; }
        [StringLength(6, MinimumLength = 1)]
        public string? Abbrevation { get; set; }
    }
}
