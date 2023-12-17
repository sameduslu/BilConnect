using BilConnect.Data.Services.ReportServices;
using BilConnect.Data.ViewModels.PostReportViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BilConnect.Controllers.ReportsController
{
    public class PostReportsController : Controller
    {
        private readonly IPostReportsService _service;
        /*
         *  public PostReportsController(IPostReportsService service) initializes the _service field 
         *  of the PostReportsController class with the provided service argument, allowing the class 
         *  to have access to an instance of the IPostReportsService interface
         *  */
        public PostReportsController(IPostReportsService service)
        {
            _service = service;
        }
        /*
         * This method is provided by the IPostReportsService 
         * interface and retrieves all the post reports from the database.
         * */
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(null, n => n.Reporter, u => u.ReportedPost);
            return View(data);
        }
        /*
         *   _service takes an id argument and retrieves the post report details 
         *   from the database based on the provided id. The method returns a task 
         *   that represents the asynchronous operation of retrieving the post 
         *   report details.
         * */
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

        /* Asynchronous method that returns an IActionResult object. A new instance of the NewPostReportVM 
         * class is created and assigned to the viewModel variable.The ReportedPostId property of the 
         * viewModel object is set to the value of the id parameter.Finally, the viewModel object is passed 
         * as an argument to the View() method, and the resulting IActionResult object is returned.*/
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


        /* 
         * method retrieves the post report details from the database based on the provided id. 
         * The result is assigned to the postReportDetails variable. An if statement is used to 
         * check if the postReportDetails variable is null. If it is null, the View("NotFound") 
         * method is called, which returns a view named "NotFound". This is used to display an error 
         * page when the requested post report details are not found. If the postReportDetails variable 
         * is not null, the View(postReportDetails) method is called, which returns a view with 
         * the postReportDetails object as the model. 
         * */
        //Get: Posts/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var postReportDetails = await _service.GetPostReportByIdAsync(id);
            if (postReportDetails == null) return View("NotFound");
            return View(postReportDetails);
        }

        /*
         *   DeleteConfirmed(int id) method, indicating that it should be 
         *   invoked when an HTTP POST request is made to the "Delete" 
         *   action of the controller. This method retrieves the post report 
         *   details based on the provided id, deletes the post report asynchronously, 
         *   and redirects the user to the "Index" action of the controller.
         * */
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postReportDetails = await _service.GetByIdAsync(id);
            if (postReportDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        /*   This method retrieves all the reports related to the post with the given 
         *   ID from the database. The result is stored in the relatedPostReports variable. 
         *   It then iterates over each report in relatedPostReports and sets its Status 
         *   property to Data.Enums.PostReportStatus.Approved. This means that all the reports 
         *   related to the post are marked as approved. Lastly and redirects the user to the 
         *   "Index" action of the controller.
         *   */
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

        /* This method retrieves the post report with the given ID from the database. 
         * The result is stored in the postReportDetails variable. If postReportDetails 
         * is not null, it means that a post report with the given ID was found in 
         * the database. In this case, it returns a view with postReportDetails as the model.
         * Else, it returns a view named "NotFound".
         * */
        //Get: Posts/Delete/1
        public async Task<IActionResult> Reject(int id)
        {
            var postReportDetails = await _service.GetByIdAsync(id);
            if (postReportDetails == null) return View("NotFound");
            return View(postReportDetails);
        }

        /* This method retrieves the post report with the given ID from the database. 
         * The result is stored in the postReportDetails variable.
         * UpdatePostReportStatusAsync method of the _service object with postReportDetails 
         * and Data.Enums.PostReportStatus.Rejected as arguments. This method updates the 
         * status of the post report in the database to "Rejected". It, then redirects 
         * the user to the Index action of the controller.
         * */
        //Get: Posts/Delete/1
        public async Task<IActionResult> RejectConfirmed(int id)
        {
            var postReportDetails = await _service.GetPostReportByIdAsync(id);
            if (postReportDetails == null) return View("NotFound");

            await _service.UpdatePostReportStatusAsync(postReportDetails, Data.Enums.PostReportStatus.Rejected);

            return View(nameof(Index));
        }

        /* If postReportDetails is not null, it means that a post report with the given ID 
         * was found in the database. In this case, it creates a new instance of NewPostReportVM 
         * and sets its properties to the values of the corresponding properties of 
         * postReportDetails. It then passes the viewModel object as an argument to the View() method
         * which returns a view with the viewModel object as the model. Else, it returns a 
         * view named "NotFound".
         * */
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
        /* If the model state is valid, it means that the post object satisfies the validation rules. 
         * In this case, it calls the UpdatePostReportAsync method of the _service object 
         * (which is an instance of IPostReportsService) with post as an argument. This method updates
         * the details of the post report in the database. Finally, it redirects the user to the 
         * SelfReports action of the Account controller.
         */
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
