using Microsoft.AspNetCore.Mvc;
/*
 *  BaseReportsController that inherits from the Controller class.
 *  Controller class called BaseReportsController with an Index() method that returns a view.
 * 
 * */
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
