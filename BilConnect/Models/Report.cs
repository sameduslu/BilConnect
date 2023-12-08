using BilConnect.Data.Base;
using BilConnect.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Models
{
    public abstract class Report : IEntityBase
    {
        public int Id { get; set;}

        public string Description { get; set;}

        public PostReportStatus Status { get; set; }

        public string ReporterId { get; set; }
        [ForeignKey("ReporterId")]


        // Navigation property for User
        public virtual ApplicationUser? Reporter { get; set; }



    }
}
