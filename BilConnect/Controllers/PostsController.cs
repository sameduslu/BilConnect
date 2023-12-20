using BilConnect.Data.Enums;
using BilConnect.Data.Services.PostServices;
using BilConnect.Data.Static;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace BilConnect.Controllers.PostsControllers
{
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.User)]

    public class PostsController : Controller
    {
        private readonly IPostsService _service;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PostsController(IPostsService service, IWebHostEnvironment hostingEnvironment)
        {
            _service = service;
            _hostingEnvironment = hostingEnvironment;
        }
        /*
         *         * This method is provided by the IPostsService 
         *                 * interface and retrieves all the posts from the database.
         *                         * */
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(
                post => !post.User.IsSuspended && post.PostStatus == PostStatus.Available, 
                n => n.User
            );
            return View(data);
        }
        /*
         * _service takes an id argument and retrieves the post details 
         *  from the database based on the provided id. The method returns a task 
         *  that represents the asynchronous operation of retrieving the post 
         *  details.
         * */
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

        /* It checks if photoUpload is not null and if it has content. If it does, it generates a unique name for the image and
         * constructs a path to save the image in the "/wwwroot/images/" directory.It updates the ImageURL property of the post
         * to point to the newly uploaded image. If photoUpload is null or empty, it adds an error to the ModelState indicating
         * that a photo needs to be uploaded. It checks if additionalImagesUpload is not null and if it has content. If it
         * does, it iterates over each file in additionalImagesUpload, generates a unique name for each image. It removes certain
         * properties from ModelState based on the PostType of the post to bypass their validation. */
        [HttpPost]
        public async Task<IActionResult> Create(NewPostVM post, IFormFile photoUpload, List<IFormFile> additionalImagesUpload)
        {
            post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Handle the image upload
            if (photoUpload != null && photoUpload.Length > 0)
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(photoUpload.FileName); // Generate a unique name
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", imageName); // Save to /wwwroot/images/

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await photoUpload.CopyToAsync(stream);
                }

                post.ImageURL = Url.Content("~/images/" + imageName); // Update the ImageURL property
            }
            else
            {
                ModelState.AddModelError("photoUpload", "Please upload a photo.");
            }


            if (post.AdditionalImages == null)
            {
                post.AdditionalImages = new List<string>();
            }

            // Handle additional images
            if (additionalImagesUpload != null && additionalImagesUpload.Count > 0)
            {
                foreach (var file in additionalImagesUpload)
                {
                    if (file.Length > 0)
                    {
                        var imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", imageName);

                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        post.AdditionalImages.Add(Url.Content("~/images/" + imageName));
                    }
                }
            }

            // Ensure AdditionalImagesJson is not null or empty
            if (string.IsNullOrEmpty(post.AdditionalImagesJson))
            {
                post.AdditionalImagesJson = JsonSerializer.Serialize(post.AdditionalImages);
            }

            // Bypass Price validation for non-selling posts
            if (post.PostType != PostType.SellingPost && post.PostType != PostType.RentingPost && post.PostType != PostType.EventTicketPost && post.PostType != PostType.TravellingPost)
            {
                ModelState.Remove("Price");
            }

            if (post.PostType != PostType.SellingPost)
            {
                ModelState.Remove("PriceS");
            }

            if (post.PostType != PostType.RentingPost)
            {
                ModelState.Remove("ReturnDate");
                ModelState.Remove("PriceB");
            }

            if (post.PostType != PostType.BorrowingPost)
            {
                ModelState.Remove("ReturnDateB");
            }

            if (post.PostType != PostType.EventTicketPost)
            {
                ModelState.Remove("EventTime");
                ModelState.Remove("EventPlace");
                ModelState.Remove("PriceE");
            }

            if (post.PostType != PostType.FoundItemPost)
            {
                ModelState.Remove("Place");
            }

            if (post.PostType != PostType.PetAdoptionPost)
            {
                ModelState.Remove("IsFullyVaccinated");
                ModelState.Remove("AgeInMonths");
            }

            if (post.PostType != PostType.TravellingPost)
            {
                ModelState.Remove("Origin");
                ModelState.Remove("Destination");
                ModelState.Remove("TravelTime");
                ModelState.Remove("Quota");
                ModelState.Remove("PriceT");
            }

            if (!ModelState.IsValid)
            {   
                
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
                
                return View(post);
            }

            // Before saving the post object
            if (string.IsNullOrEmpty(post.AdditionalImagesJson))
            {
                post.AdditionalImagesJson = "[]"; // Set default value as empty JSON array
            }

            await _service.AddNewPostAsync(post);
            return RedirectToAction(nameof(Index));
        }
        /*
         *         * The Edit(int id) method takes an id argument and retrieves the post details from the database based on the provided id.
         *                 * The result is assigned to the postDetails variable. An if statement is used to check if the postDetails variable is null.
         *                         * If it is null, the View("NotFound") method is called, which returns a view named "NotFound". This is used to display an
         *                                 * error page when the requested post details are not found. If the postDetails variable is not null, for the given PostType,
         *                                  respective VM is created.
         *                                                 * */
        public async Task<IActionResult> Edit(int id)
        {
            var postDetails = await _service.GetPostByIdAsync(id);
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
            }
            else if (postDetails is RentingPost rentingPost)
            {
                response = new NewPostVM
                {
                    Id = rentingPost.Id,
                    Title = rentingPost.Title,
                    Description = rentingPost.Description,
                    ImageURL = rentingPost.ImageURL,
                    PostDate = rentingPost.PostDate,
                    PostStatus = rentingPost.PostStatus,
                    UserId = rentingPost.UserId,
                    PostType = PostType.RentingPost,
                    PriceB = rentingPost.Price,
                    ReturnDate = rentingPost.ReturnDate,
                };
            }
            else if (postDetails is BorrowingPost borrowingPost)
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
                    ReturnDateB = borrowingPost.ReturnDate,
                };
            }
            else if (postDetails is EventTicketPost eventTicketPost)
            {
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
            else if (postDetails is FoundItemPost foundItemPost)
            {
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
                    Place = foundItemPost.Place
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

                };
            }
            else if (postDetails is PetAdoptionPost petAdoptionPost)
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
            else if (postDetails is TravellingPost travellingPost)
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

        /*This Edit method in the PostsController class is an asynchronous method that is used to edit a post. 
         * It takes four parameters: an integer id representing the post's ID, a NewPostVM object post representing
         * the new post details, an IFormFile object photoUpload representing the main photo to be uploaded, and a
         * list of IFormFile objects additionalImagesUpload representing additional images to be uploaded. If the 
         * id does not match the Id property of the post object, it returns a "NotFound" view. If the model state 
         * is not valid, it returns the post view. if all conditions are met, it updates the post using the service
         * and redirects to the "SelfPosts" action of the "Account" controller.
         */
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewPostVM post, IFormFile? photoUpload, List<IFormFile>? additionalImagesUpload)
        {
            var postDetails = await _service.GetByIdAsync(id);
            post.AdditionalImages = postDetails.AdditionalImages;
            post.ImageURL = postDetails.ImageURL;
            if (postDetails is SellingPost)
            {
                post.PostType = PostType.SellingPost;
            }
            else if (postDetails is DonationPost)
            {
                post.PostType = PostType.DonationPost;
            }
            else if (postDetails is RentingPost)
            {
                post.PostType = PostType.RentingPost;
            }
            else if (postDetails is BorrowingPost)
            {
                post.PostType = PostType.BorrowingPost;
            }
            else if (postDetails is EventTicketPost)
            {
                post.PostType = PostType.EventTicketPost;
            }
            else if (postDetails is FoundItemPost)
            {
                post.PostType = PostType.FoundItemPost;
            }
            else if (postDetails is LostItemPost)
            { 
                post.PostType = PostType.LostItemPost;
            }
            else if (postDetails is PetAdoptionPost)
            {
                post.PostType = PostType.PetAdoptionPost;
            }
            else if (postDetails is TravellingPost)
            {
                post.PostType = PostType.TravellingPost;
            }
            if (photoUpload != null && photoUpload.Length > 0)
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(photoUpload.FileName); // Generate a unique name
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", imageName); // Save to /wwwroot/images/

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await photoUpload.CopyToAsync(stream);
                }
                post.AdditionalImages.Add(post.ImageURL);
                post.ImageURL = Url.Content("~/images/" + imageName); // Update the ImageURL property
            }      
            

            // Handle additional images
            if (additionalImagesUpload != null && additionalImagesUpload.Count > 0)
            {
                foreach (var file in additionalImagesUpload)
                {
                    if (file.Length > 0)
                    {
                        var imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", imageName);

                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        post.AdditionalImages.Add(Url.Content("~/images/" + imageName));
                    }
                }
            }
            if (post.PostType != PostType.SellingPost && post.PostType != PostType.RentingPost && post.PostType != PostType.EventTicketPost && post.PostType != PostType.TravellingPost)
            {
                ModelState.Remove("Price");
            }

            if (post.PostType != PostType.SellingPost)
            {
                ModelState.Remove("PriceS");
            }

            if (post.PostType != PostType.RentingPost)
            {
                ModelState.Remove("ReturnDate");
                ModelState.Remove("PriceB");
            }

            if (post.PostType != PostType.BorrowingPost)
            {
                ModelState.Remove("ReturnDateB");
            }

            if (post.PostType != PostType.EventTicketPost)
            {
                ModelState.Remove("EventTime");
                ModelState.Remove("EventPlace");
                ModelState.Remove("PriceE");
            }

            if (post.PostType != PostType.FoundItemPost)
            {
                ModelState.Remove("Place");
            }

            if (post.PostType != PostType.PetAdoptionPost)
            {
                ModelState.Remove("IsFullyVaccinated");
                ModelState.Remove("AgeInMonths");
            }

            if (post.PostType != PostType.TravellingPost)
            {
                ModelState.Remove("Origin");
                ModelState.Remove("Destination");
                ModelState.Remove("TravelTime");
                ModelState.Remove("Quota");
                ModelState.Remove("PriceT");
            }

            if (id != post.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                return View(post);
            }
            await _service.UpdatePostAsync(post);
            return RedirectToAction("SelfPosts", "Account");
        }
        // This method takes an id argument and retrieves the post details from the database based on the provided id. Then deletes it.
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var postDetails = await _service.GetByIdAsync(id);
            if (postDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction("SelfPosts", "Account");
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
        /* The method first retrieves the post details from the service using the provided id.Then, it redirects to the "Index" 
         * action of the "Chats" controller, passing in the Id and UserId properties of the retrieved post details as route 
         * values (postId and postOwner respectively). This is used to initiate a chat between the buyer and the owner
         * of the post. */
        public async Task<IActionResult> BuyItem(int id)
        {
            var postDetails = await _service.GetPostByIdAsync(id);
            return RedirectToAction("Index", "Chats", new { postId = postDetails.Id, postOwner = postDetails.UserId });
        }
        /* Marking post as inactivated by changing post status */
        [HttpPost]
        public async Task<IActionResult> Inactivate(int id)
        {
            var postDetails = await _service.GetByIdAsync(id);
            if (postDetails == null) return View("NotFound");

            var updateViewModel = PostViewModelFactory.CreateViewModel(postDetails);
            updateViewModel.PostStatus = PostStatus.Inactivated; // Set the suspended status

            await _service.UpdatePostAsync(updateViewModel);

            return RedirectToAction("SelfPosts", "Account"); // Or redirect to a suitable page
        }
        /* Marking post as activated by changing post status */
        [HttpPost]
        public async Task<IActionResult> Activate(int id)
        {
            var postDetails = await _service.GetByIdAsync(id);
            if (postDetails == null) return View("NotFound");

            var updateViewModel = PostViewModelFactory.CreateViewModel(postDetails);
            updateViewModel.PostStatus = PostStatus.Available; // Set the suspended status

            await _service.UpdatePostAsync(updateViewModel);

            return RedirectToAction("SelfPosts", "Account"); // Or redirect to a suitable page
        }




    }

}