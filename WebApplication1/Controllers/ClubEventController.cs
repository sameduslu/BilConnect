using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ClubEventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
