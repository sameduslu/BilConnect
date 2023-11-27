using BilConnect.data.Services;
using BilConnect.Models;
using Bilconnect_First_Version.data;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BilConnect.Controllers
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
            post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Set UserId first
            post.PostDate = DateTime.Now;

            ModelState.Remove("UserId"); // Remove the ModelState error for UserId
            if (ModelState.IsValid)
            {
                await _service.AddAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
    }
}
