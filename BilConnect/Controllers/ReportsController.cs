using BilConnect.data.Services;
using BilConnect.Data.Services;
using BilConnect.Models;
using Microsoft.AspNetCore.Mvc;

namespace BilConnect.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportServiceFactory _reportServiceFactory;

        public ReportsController(IReportServiceFactory reportServiceFactory)
        {
            _reportServiceFactory = reportServiceFactory;
        }


        public IActionResult Index()
        {
            return View();
        }

        //Get Reports/Details/1
        public async Task<IActionResult> Details(string reportType, int id)
        {
            if (reportType == "Post")
            {
                var service = _reportServiceFactory.GetReportService<Post>();
                var reportDetails = await service.GetReportBaseByIdAsync(id);

                if (reportDetails == null)
                {
                    return View("NotFound");
                }
                return View(reportDetails);

            }

            return View("NotFound");
        }
    }
}
