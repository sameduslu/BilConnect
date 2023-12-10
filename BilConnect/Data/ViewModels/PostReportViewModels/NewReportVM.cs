using BilConnect.Data.Enums;

namespace BilConnect.Data.ViewModels.PostReportViewModels
{
    public class NewReportVM
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public PostReportStatus Status { get; set; }
        public string? ReporterId { get; set; }
    }
}
