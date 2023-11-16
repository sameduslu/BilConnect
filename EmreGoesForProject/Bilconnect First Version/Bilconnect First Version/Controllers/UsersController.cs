using Bilconnect_First_Version.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bilconnect_First_Version.Controllers
{
    public class UsersController : Controller
    {

        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allCinemas = await _context.Users.ToListAsync();
            return View(allCinemas);
        }
    }
}
