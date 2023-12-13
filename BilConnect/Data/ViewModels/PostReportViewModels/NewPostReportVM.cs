using BilConnect.Data.Enums;
using BilConnect.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Data.ViewModels.PostReportViewModels
{
    public class NewPostReportVM : NewReportVM
    {
        

        public int ReportedPostId { get; set; }
    }
}
