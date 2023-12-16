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

        public ClubEventsController (IClubEventsService service, IWebHostEnvironment hostingEnvironment)
        {
            _service = service;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(n => n.ownerClub);
            //var data = new List<ClubEvent>();
            return View(data);
        }

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

            //if (photoUpload != null && photoUpload.Length > 0)
            //{
            //    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(photoUpload.FileName); // Generate a unique name
            //    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", imageName); // Save to /wwwroot/images/

            //    using (var stream = new FileStream(imagePath, FileMode.Create))
            //    {
            //        await photoUpload.CopyToAsync(stream);
            //    }

            //    clubEvent.ImageURL = Url.Content("~/images/" + imageName); // Update the ImageURL property
            //}

            //else
            //{
            //    ModelState.AddModelError("photoUpload", "Please upload a photo.");
            //}

            //if (clubEvent.GE250_251Status == false)
            //{
            //    ModelState.Remove("GE250_251Points");
            //}

            //if (!ModelState.IsValid)
            //{

            //    foreach (var modelStateKey in ModelState.Keys)
            //    {
            //        var modelStateVal = ModelState[modelStateKey];
            //        foreach (var error in modelStateVal.Errors)
            //        {
            //            // Log these details or return them in the response
            //            var errorMessage = error.ErrorMessage;
            //            // You can log this or return it as part of your response
            //        }
            //    }

            //    return View(clubEvent);
            //}

            await _service.AddNewClubEventAsync(clubEvent);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit (int id)
        {
            var clubEventDetails = await _service.GetByIdAsync(id);
            if (clubEventDetails == null)
            {
                return View ("Error");
            }
            NewClubEventVM clubEvent = new NewClubEventVM {
                Id = clubEventDetails.Id,
                quota = clubEventDetails.quota,
                startTime = clubEventDetails.startTime,
                endTime = clubEventDetails.endTime,
                ownerClub = clubEventDetails.ownerClub,
                Name = clubEventDetails.Name,
                Description = clubEventDetails.Description,
                GE250_251Points = clubEventDetails.GE250_251Points,
                GE250_251Status = clubEventDetails.GE250_251Status,
                ImageURL = clubEventDetails.ImageURL,
                ownerClubId = clubEventDetails.ownerClubId,
                Place = clubEventDetails.Place
            };
            return View(clubEvent);
        }

        public async Task<IActionResult> ProcessEdit (int id, NewClubEventVM clubEvent)
        {
            if (id != clubEvent.Id)
            {
                return View ("Error");
            }
            if (!ModelState.IsValid)
            {
                return View (clubEvent);
            }
            clubEvent.ownerClubId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.UpdateClubEventAsync (clubEvent);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete (int id)
        {
            var clubEventDetails = await _service.GetByIdAsync (id);
            if (clubEventDetails == null)
            {
                return View ("Error");
            }
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
