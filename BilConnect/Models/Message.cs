using BilConnect.Data.Base;
using BilConnect.Models.PostModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Models
{
    public class Message : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Timestamp { get; set; }

        public string SenderUserId { get; set; }
        [ForeignKey("SenderId")]
        public int ChatId { get; set; }
        [ForeignKey("ChatId")]
        
        public virtual Chat? Chat { get; set; }
        public virtual ApplicationUser? Sender { get; set; }


    }
}
