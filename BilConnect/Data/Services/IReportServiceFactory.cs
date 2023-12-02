using BilConnect.Data.Base;

namespace BilConnect.Data.Services
{
    public interface IReportServiceFactory
    {
        IReportsService<T> GetReportService<T>() where T : class, IEntityBase, new();
    }
}
