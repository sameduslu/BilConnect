using BilConnect.Data.Services.PostServices;
using BilConnect.Data.Static;
using BilConnect.Data.ViewModels;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models;
using Bilconnect_First_Version.data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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

        //Get Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var postDetails = await _service.GetPostByIdAsync(id);

            if (postDetails == null)
            {
                return View("NotFound");
            }
            return View(postDetails);
        }

        public async Task<IActionResult> DetailsAfterReport(int id)
        {
            var postDetails = await _service.GetPostByIdAsync(id);

            if (postDetails == null)
            {
                return View("NotFound");
            }
            return View(postDetails);
        }

        // GET: Post/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewPostVM post)
        {
            post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return View(post);
            }

            await _service.AddNewPostAsync(post);
            return RedirectToAction(nameof(Index));
        }


        //GET: Post/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var postDetails = await _service.GetByIdAsync(id);
            if (postDetails == null)
            {
                return View("NotFound");
            }

            var response = new NewPostVM()
            {
                Id = postDetails.Id,
                Title = postDetails.Title,
                Description = postDetails.Description,
                ImageURL = postDetails.ImageURL,
                PostDate = postDetails.PostDate,
                PostStatus = postDetails.PostStatus,
                UserId = postDetails.UserId
            };

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


        //Get: Posts/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //Get: Posts/Suspend/1
        public async Task<IActionResult> Suspend(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        //Get: Posts/Suspend/1
        public async Task<IActionResult> SuspendConfirmed(int id)
        {
            var postDetails = await _service.GetByIdAsync(id);
            if (postDetails == null) return View("NotFound");

            var response = new NewPostVM()
            {
                Id = postDetails.Id,
                Title = postDetails.Title,
                Description = postDetails.Description,
                ImageURL = postDetails.ImageURL,
                PostDate = postDetails.PostDate,
                PostStatus = Data.Enums.PostStatus.Suspended,
                UserId = postDetails.UserId
            };

            await _service.UpdatePostAsync(response);


            return RedirectToAction("UpdatePostReportsStatus", "PostReports", new { postId = id });
        }


    }
}
