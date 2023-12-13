using BilConnect.Models.PostModels;
using BilConnect.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Models
{
    public class UserChat : IEntityBase
    {
        public int Id {  get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public int ChatId { get; set; }
        [ForeignKey("ChatId")]
        public virtual Chat? Chat { get; set; }
        public virtual ApplicationUser? User { get; set; }

    }
}
