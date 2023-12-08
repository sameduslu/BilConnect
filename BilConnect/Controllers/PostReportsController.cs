using BilConnect.data.Services;
using BilConnect.Data.Services;
using BilConnect.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var data = await _service.GetAllAsync(n => n.Reporter, u => u.ReportedPost);
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
                ReportedPostId = id
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
            return RedirectToAction("Details", "Posts", new { id = post.ReportedPostId });
        }



        //Get: Posts/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var postReportDetails = await _service.GetPostReportByIdAsync(id);
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

        public async Task<IActionResult> UpdatePostReportsStatus(int postId)
        {
            var relatedPostReports = await _service.GetPostReportsByPostIdAsync(postId);
            foreach (var report in relatedPostReports)
            {
                report.Status = Data.Enums.PostReportStatus.Approved;
            }
            await _service.UpdatePostReportsAsync(relatedPostReports);

            return RedirectToAction("Index", "PostReports");
        }

        //Get: Posts/Delete/1
        public async Task<IActionResult> Reject(int id)
        {
            var postReportDetails = await _service.GetByIdAsync(id);
            if (postReportDetails == null) return View("NotFound");
            return View(postReportDetails);
        }

        //Get: Posts/Delete/1
        public async Task<IActionResult> RejectConfirmed(int id)
        {
            var postReportDetails = await _service.GetPostReportByIdAsync(id);
            if (postReportDetails == null) return View("NotFound");

            await _service.UpdatePostReportStatusAsync(postReportDetails, Data.Enums.PostReportStatus.Rejected);

            return View(nameof(Index));
        }

        //GET: Post/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var postReportDetails = await _service.GetByIdAsync(id);
            if (postReportDetails == null)
            {
                return View("NotFound");
            }

            var response = new NewPostReportVM()
            {
                Id = postReportDetails.Id,
                Description = postReportDetails.Description,
                Status = postReportDetails.Status,
                ReporterId = postReportDetails.ReporterId,
                ReportedPostId = postReportDetails.ReportedPostId

            };

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewPostReportVM post)
        {

            if (id != post.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                return View(post);
            }

            await _service.UpdatePostReportAsync(post);

            return RedirectToAction("SelfReports", "Account");
        }
    }
}
