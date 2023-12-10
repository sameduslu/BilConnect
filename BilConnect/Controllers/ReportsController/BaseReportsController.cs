using Microsoft.AspNetCore.Mvc;

namespace BilConnect.Controllers.ReportsController
{
    public class BaseReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
