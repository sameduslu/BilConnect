using BilConnect.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Models
{
    public class PostReport : IEntityBase
    {
        public int Id { get; set; }

        public string Description { get; set; }

        // Navigation property for User
        public string ReporterId { get; set; }
        [ForeignKey("ReporterId")]

        // Navigation property for User
        public virtual ApplicationUser? Reporter { get; set; }

    }
}
