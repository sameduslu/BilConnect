using Microsoft.AspNetCore.Mvc;

namespace BilConnect.Controllers.ReportsController
{
    public class ClubEventReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
