using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class RegisterController : Controller
    {
        static UserDAO userDAO = new UserDAO();
        static ClubAccountDAO clubAccountDAO = new ClubAccountDAO();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessUserRegister (User user)
        {
            userDAO.Insert(user);
            return View("UserRegisterSuccess", user);
        }
        public IActionResult RegisterClubAccount()
        {
            return View();
        }

        public IActionResult ProcessClubAccountRegister (ClubAccount clubAccount)
        {
            clubAccountDAO.Insert(clubAccount);
            return View("ClubAccountRegisterSuccess", clubAccount);
        }
    }
}
