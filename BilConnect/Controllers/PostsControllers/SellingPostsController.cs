using BilConnect.Data.Services.PostServices;
using BilConnect.Data.ViewModels.PostViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BilConnect.Controllers.PostsControllers
{
    public class SellingPostsController : Controller
    {
        private readonly ISellingPostsService _service;

        public SellingPostsController(ISellingPostsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            throw new NotImplementedException();
        }

        //Get Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            throw new NotImplementedException();
        }

        // GET: Post/Create
        public async Task<IActionResult> Create()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewSellingPostVM post)
        {
            throw new NotImplementedException();
        }


        //GET: Post/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewSellingPostVM post)
        {
            throw new NotImplementedException();

        }
    }
}
