using Microsoft.AspNetCore.Mvc;

namespace BilConnect.Controllers
{
    public class ClubEventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
