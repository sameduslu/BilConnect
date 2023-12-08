using BilConnect.Data.Base;
using BilConnect.Data.ViewModels;
using BilConnect.Models;

namespace BilConnect.Data.Services
{
    public interface IPostReportsService : IEntityBaseRepository<PostReport>
    {
        Task<PostReport> GetPostReportByIdAsync(int id);
        Task AddNewPostReportAsync(NewPostReportVM data);
        //Task UpdatePostReportAsync(NewPostReportVM data);

        Task<IEnumerable<PostReport>> GetPostReportsByPostIdAsync(int postId);

        Task UpdatePostReportsAsync(IEnumerable<PostReport> postReports);
    }
}
