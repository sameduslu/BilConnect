using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostReportViewModels;
using BilConnect.Models.ReportModels;

namespace BilConnect.Data.Services.ReportServices
{
    public interface IPostReportsService : IEntityBaseRepository<PostReport>
    {
        Task<PostReport> GetPostReportByIdAsync(int id);
        Task AddNewPostReportAsync(NewPostReportVM data);
        //Task UpdatePostReportAsync(NewPostReportVM data);

        Task<IEnumerable<PostReport>> GetPostReportsByPostIdAsync(int postId);

        Task UpdatePostReportsAsync(IEnumerable<PostReport> postReports);

        Task UpdatePostReportStatusAsync(PostReport report, Enums.PostReportStatus status);
        Task UpdatePostReportAsync(NewPostReportVM post);
    }
}
