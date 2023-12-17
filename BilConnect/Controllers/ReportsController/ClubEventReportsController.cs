using Microsoft.AspNetCore.Mvc;

/*
 *  ClubEventReportsController that inherits from the Controller class.
 *  Controller class called ClubEventReportsController with an Index() method that returns a view.
 * 
 * */
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
