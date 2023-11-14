using Microsoft.AspNetCore.Mvc;

namespace BilConnect.Controllers
{
    public class ClubEventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
