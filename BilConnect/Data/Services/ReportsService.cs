using BilConnect.data.Services;
using BilConnect.Data.Base;
using BilConnect.Data.ViewModels;
using BilConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BilConnect.Data.Services
{
    public class ReportsService<T> : EntityBaseRepository<Report<T>>, IReportsService<T> where T : class, IEntityBase, new()
    {

        private readonly AppDbContext _context;

        public ReportsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ReportBase> GetReportBaseByIdAsync(int id)
        {
            // Check each DbSet and find the report with the given id
            var postReport = await _context.PostReports
                .FirstOrDefaultAsync(r => r.Id == id);
            if (postReport != null)
                return postReport;

            /*
            var commentReport = await _context.CommentReports
                .FirstOrDefaultAsync(r => r.Id == id);
            if (commentReport != null)
                return commentReport;
            */

            // If no report is found in any DbSet, return null or handle as appropriate
            return null;
        }

    }

}
