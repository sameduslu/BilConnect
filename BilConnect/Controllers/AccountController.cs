using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BilConnect.Data.Static;
using BilConnect.Data.Services;
using System.Security.Claims;
using BilConnect.Data.Services.PostServices;
using BilConnect.Data.Enums;
using Microsoft.EntityFrameworkCore;
using BilConnect.Data.ViewModels.AccountViewModels;
using BilConnect.Data.ViewModels;
using BilConnect.Models;

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

        /*
         * This method returns all the users from the database.
         */
        public async Task<IActionResult> Users()
        {
            var users = await _service.GetAllAsync();
            return View(users);
        }


        public IActionResult Login()
        {
            // Redirect the user if already logged in
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginVM());
        }
            
  

        [HttpPost]
        [ValidateAntiForgeryToken] 
        /* It attempts to find a user with the email address provided in the loginVM object. If no user is found, 
         * it sets an error message in TempData and returns the login view. If a user is found, it checks if the 
         * user's email is confirmed. If it's not, it sets an error message in TempData and returns the login view.
         * In any shape or form if sign in fails, it sets an error message in TempData and returns the login view.
         */
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

                
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                //Locked account
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Your account is locked. Please try again later.");
                    return View(loginVM);
                }
                

                // If password check fails
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVM);
            }

            // If user is not found
            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }

        public IActionResult Register() {
            // Redirect the user if already logged in
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new RegisterVM());

        }

        private async Task SendEmailConfirmationAsync(string email, string token)
        {
            var callbackUrl = Url.Action("ConfirmEmail", "Account",
                                         new { email, token }, protocol: HttpContext.Request.Scheme);
            await _emailService.SendEmailAsync(email, "Confirm Your Email",
                                               $"Please confirm your account by clicking the following link: <a href='{callbackUrl}'>Confirm Email</a>.");
        }
        /* It attempts to find a user with the email address provided in the registerVM object. 
         * If a user is found and their email is not confirmed, it returns a view named "EmailInUse". 
         * If the user's email is confirmed, it adds an error to the ModelState and returns the 
         * registration view with the current registerVM object. If no user is found, it creates a 
         * new ApplicationUser object with the details provided in the registerVM object and attempts
         * to create a new user with these details and the password provided in the registerVM object.
        * If the user creation is successful, it adds the new user to the "User" role, generates an 
        * email confirmation token for the new user, sends an email confirmation with this token, and 
        * redirects to a page named "CheckYourEmail"
        */
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {

            // Redirect the user if already logged in
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    // The email is already registered but not confirmed.
                    return View("EmailInUse");
                }
                else
                {
                    // The email is already registered and confirmed.
                    ModelState.AddModelError(string.Empty, "This email address is already in use.");
                    return View(registerVM);
                }
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


        /* 
         *         * The ConfirmEmail method takes the email and token parameters. It attempts to find a user with the email address 
         *                 * provided in the email parameter. If no user is found, it returns a view named "VerificationError". If a user is 
         *                         * found, it attempts to confirm the user's email with the token provided in the token parameter. If the email 
         *                                 * confirmation is successful, it returns a view named "VerificationSuccess". If the email confirmation fails, 
         *                                         * it returns a view named "VerificationError".
         *                                                 * */
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

        
         /* The Logout method calls the SignOutAsync method of the SignInManager class to sign out the user. 
         * It then redirects the user to the "Index" action of the "Home" controller.
         * */
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

         
         /* The SelfPosts method retrieves the current user's ID from the User object. It then calls the GetByIdAsync method 
          * of the application users service to retrieve the user's details and posts. If the user or their posts are not found, 
          * it returns a view named "NotFound". If the user and their posts are found, it returns a view with the userWithPosts 
          * object as the model.*/
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

        /* 
         * The SelfClubEvents method retrieves the current user's ID from the User object. It then calls the GetByIdAsync method of the 
         * application users service to retrieve the user's details and club events. If the user or their club events are not found, 
         *  it returns a view named "NotFound". If the user and their club events are found, it returns a view with the 
         *  userWithClubEvents object as the model.
         *  */
        public async Task<IActionResult> SelfClubEvents()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userWithClubEvents = await _service.GetByIdAsync(userId);

            if (userWithClubEvents == null)
            {
                return NotFound();
            }

            return View(userWithClubEvents);
        }
        /* 
         * The SelfReports method retrieves the current user's ID from the User object. It then calls the GetByIdAsync method of the 
         * application users service to retrieve the user's details and reports. If the user or their reports are not found, 
         * it returns a view named "NotFound". If the user and their reports are found, it returns a view with the 
         * userWithReports object as the model.
         * */
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

        /*
         * If a user is found, it sets the IsSuspended property of the user to true, indicating that the user is suspended.
         * It then attempts to update the user in the database with the UpdateAsync method of the _userManager object. If 
         * the update is successful, it redirects to the "Users" action. This is a view that lists all users.
        */
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
        /*
         * If a user is found, it sets the IsSuspended property of the user to false, indicating that the user is unsuspended.
         * It then attempts to update the user in the database with the UpdateAsync method of the _userManager object. If 
         * the update is successful, it redirects to the "Users" action. This could be a view that lists all users.
         * */
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

        /*
         *         * The Profile method takes the id parameter. If the id parameter is null, it retrieves the current user's ID from the User object. 
         *                 * If the id parameter is not null, it sets the userId variable to the id parameter. It then attempts to find a user with the ID 
         *                         * provided in the userId variable. If no user is found, it returns a view named "Error". If a user is found, it calls the 
         *                                 * GetAllAsync method of the posts service to retrieve all posts made by the user. It then creates a UserProfileVM object with 
         *                                         * the user and posts retrieved from the database. It then returns a view with the userProfileVM object as the model.
         *                                                 * */
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
        /*
         *         * The UpdateProfile method takes the id parameter. If the id parameter is null, it retrieves the current user's ID from the User object. 
         *                 * If the id parameter is not null, it sets the userId variable to the id parameter. It then attempts to find a user with the ID provided 
         *                         * in the userId variable. If no user is found, it returns a view named "Error". If a user is found, it creates a UserProfileVM object with 
         *                                 * the user retrieved from the database. It then returns a view with the userProfileVM object as the model.
         *                                         * */
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


        public IActionResult ForgotPassword() => View();
        /*
         *         * The ForgotPassword method takes the model parameter. If the ModelState is not valid, it returns the ForgotPassword view with the model as the model. 
         *                 * If the ModelState is valid, it attempts to find a user with the email address provided in the model. If no user is found, it returns the ForgotPasswordConfirmation view. 
         *                         * If a user is found, it generates a password reset token for the user and creates a callback URL with the token and email address. It then sends an email to the user with the callback URL. 
         *                                 * It then redirects to the ForgotPasswordConfirmation action of the Account controller.
         *                                         * */
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    // Generate the reset password token
                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Create reset password URL
                    var callbackUrl = Url.Action("ResetPassword", "Account",
                         new { token = resetToken, email = user.Email }, protocol: HttpContext.Request.Scheme);
                    // Send email
                    await _emailService.SendEmailAsync(user.Email, "Reset Password",
                        $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

                    // Redirect user to confirm password reset email sent
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }

                // If user not exist just dont care and pretend like you send the mail
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // Error
            return View(model);
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        /*
         * The ResetPassword method takes the token and email parameters. If the token or email parameters are null, it adds an error to the ModelState and returns the ResetPassword view with the model. 
         * If the token and email parameters are not null, it creates a ResetPasswordVM object with the token and email parameters. It then returns the ResetPassword view with the resetPasswordVM object as the model.
         * */
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View(new ResetPasswordVM { Token = token, Email = email });
        }
        
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        /*
         *         * The ResetPassword method takes the model parameter. If the ModelState is not valid, it returns the ResetPassword view with the model as the model. 
         *                 * If the ModelState is valid, it attempts to find a user with the email address provided in the model. If no user is found, it returns the ResetPasswordConfirmation view. 
         *                         * If a user is found, it attempts to reset the user's password with the token and password provided in the model. If the password reset is successful, it returns the ResetPasswordConfirmation view. 
         *                                 * If the password reset fails, it adds an error to the ModelState and returns the ResetPassword view with the model as the model.
         *                                         * */
        [HttpPost]
        [ValidateAntiForgeryToken]//Security purpose
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // No need to reveal user does not exist for security
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        /*
         *         * The ListUsers method retrieves all users from the database. It then creates three lists of users: adminUsers, clubAccountUsers, and regularUsers. 
         *                 * It then loops through all users and adds them to the appropriate list based on their role. It then creates a UsersViewModel object with the three lists. 
         *                         * It then returns the ListUsers view with the usersViewModel object as the model.
         *                                 * */
        public async Task<IActionResult> ListUsers()
        {

            if (!User.IsInRole("Admin"))
            {
                return View("NotAuthorized");
            }

            var allUsers = await _userManager.Users.ToListAsync();
            var adminUsers = new List<ApplicationUser>();
            var clubAccountUsers = new List<ApplicationUser>();
            var regularUsers = new List<ApplicationUser>();

            foreach (var user in allUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains(UserRoles.Admin))
                {
                    adminUsers.Add(user);
                }
                else if (roles.Contains(UserRoles.ClubAccount))
                {
                    clubAccountUsers.Add(user);
                }
                else
                {
                    regularUsers.Add(user);
                }
            }

            var viewModel = new UsersViewModel
            {
                AdminUsers = adminUsers,
                ClubAccountUsers = clubAccountUsers,
                RegularUsers = regularUsers
            };

            return View(viewModel);
        }
        /*
         * The ChangePassword method returns the ChangePassword view.
         *  */
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordVM();
            return View();
        }
        /*
         * The ChangePassword method takes the model parameter. If the ModelState is not valid, it returns the ChangePassword view with the model as the model. 
         *  If the ModelState is valid, it attempts to find the current user with the GetUserAsync method of the _userManager object. 
         *  If no user is found, it redirects to the Login action of the Account controller. If a user is found, it attempts to change the user's password with the ChangePasswordAsync method of the _userManager object. 
         *  If the password change is successful, it refreshes the user's sign-in cookie with the RefreshSignInAsync method of the _signInManager object. It then redirects to the Index action of the Home controller. 
         *  If the password change fails, it adds an error to the ModelState and returns the ChangePassword view with the model as the model.
         *  */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);
            TempData["SuccessMessage"] = "Your password has been changed successfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}
