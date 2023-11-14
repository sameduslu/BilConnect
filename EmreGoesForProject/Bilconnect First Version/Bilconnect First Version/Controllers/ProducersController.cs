using Bilconnect_First_Version.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bilconnect_First_Version.Controllers
{
    public class ProducersController : Controller
    {

        private readonly AppDbContext _context;

        public ProducersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allProducers = await _context.Producers.ToListAsync();
            return View(allProducers);
        }
    }
}
