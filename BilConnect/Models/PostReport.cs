using BilConnect.Data.Base;
using BilConnect.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Models
{
    public class PostReport : IEntityBase
    {
        public int Id { get; set; }

        [Display(Name = "Complaint")]
        public string Description { get; set; }

        public PostReportStatus Status { get; set; }

        // Navigation property for User
        public string ReporterId { get; set; }
        [ForeignKey("ReporterId")]

        // Navigation property for User
        public virtual ApplicationUser? Reporter { get; set; }

        public int ReportedPostId { get; set; }
        [ForeignKey("ReportedPostId")]

        public virtual Post? ReportedPost { get; set; }

    }
}
