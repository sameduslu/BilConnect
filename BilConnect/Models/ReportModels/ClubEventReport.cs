using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Models.ReportModels
{
    public class ClubEventReport : Report
    {
        public int ReportedClubEventId { get; set; }
        [ForeignKey("ReportedClubEventId")]

        public virtual ClubEvent? ReportedClubEvent { get; set; }
    }
}
