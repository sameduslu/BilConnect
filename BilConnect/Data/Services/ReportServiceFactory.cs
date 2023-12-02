using BilConnect.Data.Base;
using BilConnect.Models;

namespace BilConnect.Data.Services
{
    public class ReportServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ReportServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IReportsService<T> GetReportService<T>() where T : class, IEntityBase, new()
        {
            if (typeof(T) == typeof(Post))
            {
                return (IReportsService<T>)_serviceProvider.GetService(typeof(IReportsService<Post>));
            }
 

            throw new ArgumentException("Invalid report type", nameof(T));
        }
    }
}
