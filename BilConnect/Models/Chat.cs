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

        //Relationshpis
        public string UserId { get; set; }
        [ForeignKey("UserId")] 
        public string ReceiverId { get; set; }
        //[ForeignKey("ReceiverId")]
        public int RelatedPostId { get; set; }
        // Navigation properties
        public virtual ApplicationUser? User { get; set; }
       // public virtual ApplicationUser? Receiver { get; set; }


    }
}
