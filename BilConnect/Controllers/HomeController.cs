using BilConnect.Data.Static;
using BilConnect.Data.ViewModels;
using BilConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BilConnect.Controllers
{
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.User + "," + UserRoles.ClubAccount)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
        public IActionResult About()
        {
            var teamMembers = new List<DeveloperInfoViewModel>
            {
                new DeveloperInfoViewModel { Name = "Emre Akgül", Email = "emre.akgul@ug.bilkent.edu.tr", Cellphone = "541 714 24 85", LinkedInProfile = "https://www.linkedin.com/in/AkgulEmre/" },
                // Add more team members here
            };

            return View(teamMembers);
        }


    }
}