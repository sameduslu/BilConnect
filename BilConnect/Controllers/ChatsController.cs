using BilConnect.Data.Services;
using BilConnect.Data.Static;
using BilConnect.Data.ViewModels;
using BilConnect.Models;
using Bilconnect_First_Version.data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace BilConnect.Controllers
{
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.User)]

    public class ChatsController : Controller
    {
        private readonly IChatsService _service;
        public ChatsController(IChatsService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create(int postId, string postOwner)
        {
            System.Diagnostics.Debug.WriteLine(postId);
            System.Diagnostics.Debug.WriteLine(postOwner);
            var data = await _service.GetAllAsync(n => n.User);
            bool isDuplicate = false;
            foreach (Chat c in data)
            {
                if (c.RelatedPostId == postId)
                {
                    isDuplicate = true;
                    break;
                }
            }
            if (!isDuplicate)
            {
                ChatVM chat = new ChatVM
                {
                    RelatedPostId = postId,
                    ReceiverId = postOwner,
                };
                chat.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _service.AddNewChatAsync(chat);
                data = await _service.GetAllAsync(n => n.User);
            }
            return View(data);
        }
        public async Task<IActionResult> Index(int postId, string postOwner)
        {
            if (!string.IsNullOrEmpty(postOwner))
                return await Create(postId, postOwner);
            var data = await _service.GetAllAsync(n => n.User);
            return View(data);
        }
        public async Task<IActionResult> Room(int id)
        {
            var data = await _service.GetByIdAsync(id);
            System.Diagnostics.Debug.WriteLine(data.Id);
            return View(data);
        }
    }
}
