using BilConnect.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BilConnect.Models
{
    public class ReportBase : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        //Reporter
        public string ReporterId { get; set; }
        [ForeignKey("Reporterd")]

        // Navigation property for User
        public virtual ApplicationUser? Reporter { get; set; }

        //Reported Object
        public int ReportedObjId { get; set; }
    }
}
