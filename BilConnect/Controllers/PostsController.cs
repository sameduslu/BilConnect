using BilConnect.Data.Enums;
using BilConnect.Data.Services.PostServices;
using BilConnect.Data.Static;
using BilConnect.Data.ViewModels;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models;
using BilConnect.Models.PostModels;
using Bilconnect_First_Version.data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Claims;

namespace BilConnect.Controllers.PostsControllers
{
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.User)]

    public class PostsController : Controller
    {
        private readonly IPostsService _service;

        public PostsController(IPostsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(n => n.User);
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var postDetails = await _service.GetPostByIdAsync(id);
            if (postDetails == null)
            {
                return View("NotFound");
            }
            return View(postDetails);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewPostVM post)
        {
            post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            // Bypass Price validation for non-selling posts
            if (post.PostType != PostType.SellingPost && post.PostType != PostType.BorrowingPost && post.PostType != PostType.EventTicketPost && post.PostType != PostType.TravellingPost)
            {
                ModelState.Remove("Price");
            }

            if(post.PostType != PostType.BorrowingPost)
            {
                ModelState.Remove("ReturnDate");
            }

            if(post.PostType != PostType.EventTicketPost)
            {
                ModelState.Remove("EventTime");
                ModelState.Remove("EventPlace");
            }

            if (post.PostType != PostType.LostItemPost)
            {
                ModelState.Remove("Place");
            }

            if (post.PostType != PostType.LostItemPost)
            {
                ModelState.Remove("IsFullyVaccinated");
                ModelState.Remove("AgeInMonths");
            }

            if (post.PostType != PostType.LostItemPost)
            {
                ModelState.Remove("Origin");
                ModelState.Remove("Destination");
                ModelState.Remove("TravelTime");
                ModelState.Remove("Quota");
            }


            if (!ModelState.IsValid)
            {
                return View(post);
            }
            await _service.AddNewPostAsync(post);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var postDetails = await _service.GetByIdAsync(id);
            if (postDetails == null)
            {
                return View("NotFound");
            }

            NewPostVM response;

            if (postDetails is SellingPost sellingPost)
            {
                response = new NewPostVM
                {
                    Id = sellingPost.Id,
                    Title = sellingPost.Title,
                    Description = sellingPost.Description,
                    ImageURL = sellingPost.ImageURL,
                    PostDate = sellingPost.PostDate,
                    PostStatus = sellingPost.PostStatus,
                    UserId = sellingPost.UserId,
                    PostType = PostType.SellingPost,
                    PriceS = sellingPost.Price  // Populate price for SellingPost
                };
            }
            else if (postDetails is DonationPost donationPost)
            {
                response = new NewPostVM
                {
                    Id = donationPost.Id,
                    Title = donationPost.Title,
                    Description = donationPost.Description,
                    ImageURL = donationPost.ImageURL,
                    PostDate = donationPost.PostDate,
                    PostStatus = donationPost.PostStatus,
                    UserId = donationPost.UserId,
                    PostType = PostType.DonationPost
                };
            }else if (postDetails is BorrowingPost borrowingPost)
            {
                response = new NewPostVM
                {
                    Id = borrowingPost.Id,
                    Title = borrowingPost.Title,
                    Description = borrowingPost.Description,
                    ImageURL = borrowingPost.ImageURL,
                    PostDate = borrowingPost.PostDate,
                    PostStatus = borrowingPost.PostStatus,
                    UserId = borrowingPost.UserId,
                    PostType = PostType.BorrowingPost,
                    PriceB = borrowingPost.Price,
                    ReturnDate = borrowingPost.ReturnDate,
                };
            }
            else if (postDetails is EventTicketPost eventTicketPost){
                response = new NewPostVM
                {
                    Id = eventTicketPost.Id,
                    Title = eventTicketPost.Title,
                    Description = eventTicketPost.Description,
                    ImageURL = eventTicketPost.ImageURL,
                    PostDate = eventTicketPost.PostDate,
                    PostStatus = eventTicketPost.PostStatus,
                    UserId = eventTicketPost.UserId,
                    PostType = PostType.EventTicketPost,
                    EventTime = eventTicketPost.EventTime,
                    EventPlace = eventTicketPost.EventPlace,
                    PriceE = eventTicketPost.Price,
                };
            }
            else if (postDetails is FoundItemPost foundItemPost){
                response = new NewPostVM
                {
                    Id = foundItemPost.Id,
                    Title = foundItemPost.Title,
                    Description = foundItemPost.Description,
                    ImageURL = foundItemPost.ImageURL,
                    PostDate = foundItemPost.PostDate,
                    PostStatus = foundItemPost.PostStatus,
                    UserId = foundItemPost.UserId,
                    PostType = PostType.FoundItemPost,
                };
            }
            else if (postDetails is LostItemPost lostItemPost)
            {
                response = new NewPostVM
                {
                    Id = lostItemPost.Id,
                    Title = lostItemPost.Title,
                    Description = lostItemPost.Description,
                    ImageURL = lostItemPost.ImageURL,
                    PostDate = lostItemPost.PostDate,
                    PostStatus = lostItemPost.PostStatus,
                    UserId = lostItemPost.UserId,
                    PostType = PostType.LostItemPost,
                    Place = lostItemPost.Place,
                   
                };
            }
            else if(postDetails is PetAdoptionPost petAdoptionPost)
            {
                response = new NewPostVM
                {
                    Id = petAdoptionPost.Id,
                    Title = petAdoptionPost.Title,
                    Description = petAdoptionPost.Description,
                    ImageURL = petAdoptionPost.ImageURL,
                    PostDate = petAdoptionPost.PostDate,
                    PostStatus = petAdoptionPost.PostStatus,
                    UserId = petAdoptionPost.UserId,
                    PostType = PostType.PetAdoptionPost,
                    IsFullyVaccinated = petAdoptionPost.IsFullyVaccinated,
                    AgeInMonths = petAdoptionPost.AgeInMonths,
                };
            }
            else if(postDetails is TravellingPost travellingPost)
            {
                response = new NewPostVM
                {
                    Id = travellingPost.Id,
                    Title = travellingPost.Title,
                    Description = travellingPost.Description,
                    ImageURL = travellingPost.ImageURL,
                    PostDate = travellingPost.PostDate,
                    PostStatus = travellingPost.PostStatus,
                    UserId = travellingPost.UserId,
                    PostType = PostType.TravellingPost,
                    Origin = travellingPost.Origin,
                    Destination = travellingPost.Destination,
                    TravelTime = travellingPost?.TravelTime,
                    Quota = travellingPost?.Quota,
                    PriceT = travellingPost.Price,

                };
            }
            else
            {
                response = new NewPostVM
                {
                    // Populate common fields if the post is neither SellingPost nor DonationPost
                };
            }

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewPostVM post)
        {
            if (id != post.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                return View(post);
            }
            await _service.UpdatePostAsync(post);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var postDetails = await _service.GetByIdAsync(id);
            if (postDetails == null) return View("NotFound");
            return View(postDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postDetails = await _service.GetByIdAsync(id);
            if (postDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Suspend(int id)
        {
            var postDetails = await _service.GetByIdAsync(id);
            if (postDetails == null) return View("NotFound");
            return View(postDetails);
        }

        [HttpPost, ActionName("Suspend")]
        public async Task<IActionResult> SuspendConfirmed(int id)
        {
            var postDetails = await _service.GetByIdAsync(id);
            if (postDetails == null) return View("NotFound");

            // Update the status to Suspended
            postDetails.PostStatus = PostStatus.Suspended;

            // Use the factory to create the appropriate ViewModel
            var updateViewModel = PostViewModelFactory.CreateViewModel(postDetails);
            updateViewModel.PostStatus = PostStatus.Suspended; // Set the suspended status



            await _service.UpdatePostAsync(updateViewModel);

            return RedirectToAction("UpdatePostReportsStatus", "PostReports", new { postId = id });

        }
    }

}
