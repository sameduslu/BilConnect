using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        SecurityController securityController = new SecurityController();

        public IActionResult ProcessLogin (User user)
        {
            if (securityController.isValid(user))
            {
                return View("LoginSuccess", user);
            }
            return View("LoginFailed", user);
        }
    }
}
