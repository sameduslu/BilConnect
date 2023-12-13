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

    public class UserChatsController : Controller
    {
        private readonly IUserChatsService _service;
        public UserChatsController(IUserChatsService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create(int postId, string postOwner)
        {
            UserChatVM vm = new UserChatVM {
                ChatId = chatId,
                UserId = userId
            };
            await _service.AddNewUserChatAsync(vm);
            return Json(new { success = true, message = "Data saved successfully" });
        }
        public async Task<IActionResult> Index(int postId, string postOwner)
        {
            if (!string.IsNullOrEmpty(postOwner))
                return await Create(postId, postOwner);
            var chats = _service.GetAllAsync();
            return View(chats);
        }
    }
}