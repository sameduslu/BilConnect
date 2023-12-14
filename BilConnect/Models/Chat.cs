using BilConnect.Data.Base;
using BilConnect.Models.PostModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Models
{
    public class Chat : IEntityBase, IComparable<Chat>
    {
        [Key]
        public int Id { get; set; }
        public int RelatedPostId { get; set; }
        [ForeignKey("RelatedPostId")]

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public string ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]

        public DateTime SenderLastSeen { get; set; }
        public DateTime ReceiverLastSeen { get; set; }
        public virtual Post? RelatedPost { get; set; }
        public virtual List<Message>? Messages { get; set;}
        public virtual ApplicationUser? User { get; set; }
        public virtual ApplicationUser? Receiver { get; set; }

        // Compares chats based on the last message.
        public int CompareTo(Chat other)
        {
            if (other == null)
            {
                return 1; // If the other object is null, this instance is greater.
            }
            else if (other.Messages == null || other.Messages.Count == 0)
            {
                return 1;
            }
            else if (Messages == null || Messages.Count == 0)
            {
                return -1;
            }
            // Compare based on the RelatedPostId.
            return other.Messages[other.Messages.Count-1].Timestamp.CompareTo(Messages[Messages.Count-1].Timestamp);
        }
    }
}
