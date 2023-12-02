using BilConnect.Data.Base;
using BilConnect.Data.ViewModels;
using BilConnect.Models;

namespace BilConnect.Data.Services
{
    public interface IReportsService<T> : IEntityBaseRepository<Report<T>> where T : class, IEntityBase, new()
    {
        Task<ReportBase> GetReportBaseByIdAsync(int id);
        
    }
}
