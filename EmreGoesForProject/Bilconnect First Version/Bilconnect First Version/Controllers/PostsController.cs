using Bilconnect_First_Version.data;
using Bilconnect_First_Version.data.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bilconnect_First_Version.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostsService _service;

        public PostsController(IPostsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var postDetails = await _service.GetByIdAsync(id);

            if (postDetails == null)
            {
                return View("NotFound");
            }
            return View(postDetails);
        }


        // GET: Post/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Price,ImageURL")] Post post)
        {
            if (ModelState.IsValid)
            {
                // Set the UserId to the ID of the currently logged-in user
                post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                post.PostDate = DateTime.Now; // Set the post date to the current date

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to the index action after saving
            }
            return View(post);
        }
    }
}
