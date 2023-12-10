using BilConnect.Data.Base;
using BilConnect.Data.Enums;
using BilConnect.Data.ViewModels.PostReportViewModels;
using BilConnect.Models.ReportModels;
using Microsoft.EntityFrameworkCore;

namespace BilConnect.Data.Services.ReportServices
{
    public class PostReportsService : EntityBaseRepository<PostReport>, IPostReportsService
    {
        private readonly AppDbContext _context;

        public PostReportsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewPostReportAsync(NewPostReportVM data)
        {
            var newPostReport = new PostReport()
            {
                Description = data.Description,
                Status = data.Status,
                ReporterId = data.ReporterId,
                ReportedPostId = data.ReportedPostId
            };

            await _context.PostReports.AddAsync(newPostReport);
            await _context.SaveChangesAsync();
        }

        public async Task<PostReport> GetPostReportByIdAsync(int id)
        {
            var postReportDetails = _context.PostReports
                  .Include(p => p.Reporter)
                  .Include(r => r.ReportedPost)
                  .FirstOrDefaultAsync(n => n.Id == id);

            return await postReportDetails;

        }


        public async Task<IEnumerable<PostReport>> GetPostReportsByPostIdAsync(int postId)
        {
            return await _context.PostReports
                                 .Where(pr => pr.ReportedPostId == postId)
                                 .ToListAsync();
        }

        public async Task UpdatePostReportsAsync(IEnumerable<PostReport> postReports)
        {
            foreach (var report in postReports)
            {
                report.Status = PostReportStatus.Approved;
                _context.Entry(report).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostReportStatusAsync(PostReport report, PostReportStatus status)
        {
            report.Status = PostReportStatus.Rejected;
            _context.Entry(report).State = EntityState.Modified;

            await _context.SaveChangesAsync();

        }

        public async Task UpdatePostReportAsync(NewPostReportVM data)
        {
            var dbPostReport = await _context.PostReports.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbPostReport != null)
            {

                dbPostReport.Description = data.Description;

                await _context.SaveChangesAsync();

            }
            await _context.SaveChangesAsync();
        }

    }
}
