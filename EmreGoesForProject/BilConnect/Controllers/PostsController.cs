using Microsoft.AspNetCore.Mvc;

namespace BilConnect.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
