using BilConnect.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace BilConnect.Models
{
    public class ClubEvent : IEntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
