using BilConnect.Data;
using BilConnect.Models;
using Bilconnect.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BilConnect.Data.Static;
using BilConnect.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using BilConnect.Data.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using BilConnect.Data.Services.PostServices;
using BilConnect.Data.Enums;
using Microsoft.Extensions.Hosting.Internal;

namespace BilConnect.Controllers
{

    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationUsersService _service;
        private readonly IEmailService _emailService;
        private readonly IPostsService _postsService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                    IApplicationUsersService service, IEmailService emailService, IPostsService postsService,
                                    IWebHostEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _service = service;
            _emailService = emailService;
            _postsService = postsService;
            _hostingEnvironment = hostingEnvironment;
        }


        public async Task<IActionResult> Users()
        {
            var users = await _service.GetAllAsync();
            return View(users);
        }


        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    // If email is not confirmed, inform the user
                    TempData["Error"] = "You must confirm your email before logging in.";
                    return View(loginVM);
                }

                if (user.IsSuspended)
                {
                    TempData["Error"] = "Your account has been suspended. Please contact support.";
                    return View(loginVM);
                }

                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Posts");
                    }
                }

                // If password check fails
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVM);
            }

            // If user is not found
            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }

        public IActionResult Register() => View(new RegisterVM());



        private async Task SendEmailConfirmationAsync(string email, string token)
        {
            var callbackUrl = Url.Action("ConfirmEmail", "Account",
                                         new { email, token }, protocol: HttpContext.Request.Scheme);
            await _emailService.SendEmailAsync(email, "Confirm Your Email",
                                               $"Please confirm your account by clicking the following link: <a href='{callbackUrl}'>Confirm Email</a>.");
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {   
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                EmailConfirmed = false, // User's email is not confirmed initially
                IsSuspended = false,
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

                // Generate email confirmation token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                // Send email with this token
                await SendEmailConfirmationAsync(newUser.Email, token);

                // Redirect to a page that instructs the user to check their email
                return RedirectToAction("CheckYourEmail");
            }
            else
            {
                foreach (var error in newUserResponse.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerVM);
            }
        }



        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return View("VerificationError");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View("VerificationSuccess");
            }

            return View("VerificationError");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }


        public async Task<IActionResult> SelfPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var userWithPosts = await _service.GetByIdAsync(userId); 

            if (userWithPosts == null)
            {
                // Handle the case where the user or their posts are not found
                return NotFound(); 
            }

            return View(userWithPosts);
        }

        public async Task<IActionResult> SelfReports()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID
            var userWithPosts = await _service.GetByIdAsync(userId); 

            if (userWithPosts == null)
            {
                // Handle the case where the user or their posts are not found
                return NotFound(); 
            }

            return View(userWithPosts); 
        }


        public IActionResult CheckYourEmail()
        {
            return View();
        }


        public async Task<IActionResult> SuspendUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.IsSuspended = true;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // Optionally add logic here to notify the user of suspension
                return RedirectToAction("Users");
            }

            // Handle errors here
            return View("Error"); // Replace with appropriate error handling
        }

        public async Task<IActionResult> UnsuspendUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.IsSuspended = false;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // Optionally add logic here to notify the user of unsuspension
                return RedirectToAction("Users");
            }

            // Handle errors here
            return View("Error"); // Replace with appropriate error handling
        }


        public async Task<IActionResult> Profile(string? id)
        {
            string userId;
            if(id == null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            else
            {
                userId = id;
            }

            if (string.IsNullOrEmpty(userId))
            {
                return View("Error"); // or any appropriate error view
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error"); // User not found, return an error view
            }

            // Fetch posts made by the user
            var userPosts = await _postsService.GetAllAsync(
                post => post.UserId == userId && post.PostStatus != PostStatus.Inactivated // Modify the filter as needed
            );

            var viewModel = new UserProfileVM
            {
                Id = userId,
                User = user,
                Posts = userPosts
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfilePicture(IFormFile photoUpload)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            if (photoUpload != null && photoUpload.Length > 0)
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(photoUpload.FileName);
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", imageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await photoUpload.CopyToAsync(stream);
                }

                user.ImageURL = Url.Content("~/images/" + imageName);
                var updateResult = await _userManager.UpdateAsync(user);

                if (updateResult.Succeeded)
                {
                    return Json(new { success = true, newImageUrl = user.ImageURL });
                }

                return Json(new { success = false, message = "Error updating user." });
            }

            return Json(new { success = false, message = "Please upload a photo." });
        }
    }
}
