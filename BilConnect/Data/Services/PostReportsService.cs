using BilConnect.Data.Base;
using BilConnect.Data.ViewModels;
using BilConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace BilConnect.Data.Services
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
                ReporterId = data.ReporterId,
            };

            await _context.PostReports.AddAsync(newPostReport);
            await _context.SaveChangesAsync();
        }

        public async Task<PostReport> GetPostReportByIdAsync(int id)
        {
            var postReportDetails = _context.PostReports
                  .Include(p => p.Reporter)
                  .FirstOrDefaultAsync(n => n.Id == id);

            return await postReportDetails;

        }

    }
}
