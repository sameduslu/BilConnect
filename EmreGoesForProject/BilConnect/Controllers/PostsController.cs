using BilConnect.data.Services;
using Bilconnect_First_Version.data;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
