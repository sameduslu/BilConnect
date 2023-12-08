using BilConnect.Data.Enums;
using BilConnect.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Data.ViewModels
{
    public class NewPostReportVM 
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public PostReportStatus Status { get; set; }
        public string? ReporterId { get; set; }

        public int ReportedPostId { get; set; }
    }
}
