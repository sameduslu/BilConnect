using BilConnect.Data.Services;
using BilConnect.Data.Static;
using BilConnect.Data.ViewModels;
using BilConnect.Models;
using BilConnect.Models.PostModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System.Security.Claims;

namespace BilConnect.Controllers
{
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.User + "," + UserRoles.ClubAccount)]
    public class ClubEventsController : Controller
    {
        private readonly IClubEventsService _service;
        private readonly IWebHostEnvironment _hostingEnvironment;
        /*
         *         *  public ClubEventsController(IClubEventsService service) initializes the _service field 
         *                 *  of the ClubEventsController class with the provided service argument, allowing the class 
         *                         *  to have access to an instance of the IClubEventsService interface
         *                                 *  */
        public ClubEventsController (IClubEventsService service, IWebHostEnvironment hostingEnvironment)
        {
            _service = service;
            _hostingEnvironment = hostingEnvironment;
        }
        /*
         *         * This method is provided by the IClubEventsService 
         *                 * interface and retrieves all the club events from the database.
         *                         * */
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(null, n => n.ownerClub);
            return View(data);
        }
        /*
         *         *   _service takes an id argument and retrieves the club event details 
         *                 *   from the database based on the provided id. The method returns a task 
         *                         *   that represents the asynchronous operation of retrieving the club 
         *                                 *   event details.
         *                                         * */
        public async Task<IActionResult> Details (int id)
        {
            var clubEvent = await _service.GetClubEventByIdAsync(id);
            if (clubEvent == null)
            {
                return View ("Error");
            }
            return View(clubEvent);
        }
        
        public async Task<IActionResult> Create ()
        {
            return View();
        }
        
        public async Task<IActionResult> ProcessCreate (NewClubEventVM clubEvent, IFormFile photoUpload)
        {
            clubEvent.ownerClubId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Is photo uploaded or not
            if (photoUpload != null && photoUpload.Length > 0)
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(photoUpload.FileName); // Generate a unique name
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", imageName); // Save to /wwwroot/images/
                // Copy the image to the target path
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await photoUpload.CopyToAsync(stream);
                }
                //Update ImageURL
                clubEvent.ImageURL = Url.Content("~/images/" + imageName); // Update the ImageURL property
            }

            else
            {
                // If no photo is uploaded, add an error to ModelState and return to the view
                ModelState.AddModelError("photoUpload", "Please upload a photo.");
            }

            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                // If not, loop through the model state errors and log them or return them in the response
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        // Log these details or return them in the response
                        var errorMessage = error.ErrorMessage;
                        // You can log this or return it as part of your response
                    }
                }
                return View("Create", clubEvent);
            }
            // Add the new club event to the database
            await _service.AddNewClubEventAsync(clubEvent);
            // List all of the club events
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Edit (int id)
        {
            // Get the club event details from the database by ID
            var clubEventDetails = await _service.GetByIdAsync(id);
            if (clubEventDetails == null)
            {
                // If the club event does not exist, return an error
                return View ("Error");
            }
            // If the club event exists, return the club event details
            NewClubEventVM clubEvent = new NewClubEventVM {
                Id = clubEventDetails.Id,
                quota = clubEventDetails.quota,
                startTime = clubEventDetails.startTime,
                Duration = clubEventDetails.Duration,
                ownerClub = clubEventDetails.ownerClub,
                Name = clubEventDetails.Name,
                Description = clubEventDetails.Description,
                GE250_251Points = clubEventDetails.GE250_251Points,
                GE250_251Status = clubEventDetails.GE250_251Status,
                ImageURL = clubEventDetails.ImageURL,
                ownerClubId = clubEventDetails.ownerClubId,
                Place = clubEventDetails.Place,
                CreationTime = clubEventDetails.CreationTime
            };
            return View(clubEvent);
        }

        public async Task<IActionResult> ProcessEdit (int id, NewClubEventVM clubEvent, IFormFile? photoUpload)
        {
            // Check if the photo is uploaded
            if (photoUpload != null && photoUpload.Length > 0)
            {
                // If the photo is uploaded, save it to the /wwwroot/images/ folder
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(photoUpload.FileName); // Generate a unique name
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", imageName); // Save to /wwwroot/images/

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await photoUpload.CopyToAsync(stream);
                }

                clubEvent.ImageURL = Url.Content("~/images/" + imageName); // Update the ImageURL property
            }
            else
            {
                var clubEventDetails = await _service.GetByIdAsync(id);
                clubEvent.ImageURL = clubEventDetails.ImageURL;
            }
            if (id != clubEvent.Id)
            {
                return View ("Error");
            }
            //ModelState.Remove("photoUpload");
            if (!ModelState.IsValid)
            {
                return View ("Edit", clubEvent);
            }
            clubEvent.ownerClubId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.UpdateClubEventAsync (clubEvent);
            return RedirectToAction("SelfClubEvents", "Account");
        }

        public async Task<IActionResult> Delete (int id)
        {
            // Get the club event details from the database by ID
            var clubEventDetails = await _service.GetByIdAsync (id);
            if (clubEventDetails == null)
            {
                return View ("Error");
            }
            // If the club event exists, delete it by using ID
            await _service.DeleteAsync(id);
            return RedirectToAction("SelfClubEvents", "Account");
        }
    }
}
