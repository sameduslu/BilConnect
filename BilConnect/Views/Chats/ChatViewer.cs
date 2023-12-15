using BilConnect.Data.Base;
using BilConnect.Models.PostModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Models
{
    // Viewer class for Chat
    public class ChatViewer
    {
        public int Id { get; set; }
        public Post? RelatedPost { get; set; }
        public List<Message>? Messages { get; set; }
        public ApplicationUser? User { get; set; }
        public ApplicationUser? Receiver { get; set; }


    }
}
