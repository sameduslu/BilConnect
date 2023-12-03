using BilConnect.data.Services;
using BilConnect.Data.Services;
using BilConnect.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BilConnect.Controllers
{
    public class PostReportsController : Controller
    {
        private readonly IPostReportsService _service;

        public PostReportsController(IPostReportsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(n => n.Reporter);
            return View(data);
        }

        //Get Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var postReportDetails = await _service.GetPostReportByIdAsync(id);

            if (postReportDetails == null)
            {
                return View("NotFound");
            }
            return View(postReportDetails);
        }

        // GET: PostReports/Create
        public async Task<IActionResult> Create(int id)
        {
            var viewModel = new NewPostReportVM
            {

            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(NewPostReportVM post)
        {
            post.ReporterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            await _service.AddNewPostReportAsync(post);
            return RedirectToAction(nameof(Index));
        }



        //Get: Posts/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var postReportDetails = await _service.GetByIdAsync(id);
            if (postReportDetails == null) return View("NotFound");
            return View(postReportDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postReportDetails = await _service.GetByIdAsync(id);
            if (postReportDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
