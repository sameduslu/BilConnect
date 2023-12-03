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

namespace BilConnect.Controllers
{

    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationUsersService _service;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                    IApplicationUsersService service)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
            _service = service;
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
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Posts");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }

        public IActionResult Register() => View(new RegisterVM());

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
                UserName = registerVM.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");
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

    }
}
