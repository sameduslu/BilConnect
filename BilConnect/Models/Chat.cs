using BilConnect.Data.Base;
using BilConnect.Models.PostModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Models
{
    public class Chat : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        //Relationships
        public int RelatedPostId { get; set; }
        [ForeignKey("RelatedPostId")]

        public string UserId { get; set; }

        public string ReceiverId { get; set; }
        // Navigation properties
        //public virtual List<UserChat>? UserChats { get; set; }
        public virtual Post? RelatedPost { get; set; }
        public virtual List<Message>? Messages { get; set;}

        // delete below
        public virtual ApplicationUser? User { get; set; }


    }
}
