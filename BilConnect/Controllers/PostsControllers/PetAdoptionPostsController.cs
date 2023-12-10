using BilConnect.Data.Services.PostServices;
using BilConnect.Data.ViewModels.PostViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BilConnect.Controllers.PostsControllers
{
    public class PetAdoptionPostsController : Controller
    {
        private readonly IPetAdoptionPostsService _service;

        public PetAdoptionPostsController(IPetAdoptionPostsService service)
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
        public async Task<IActionResult> Create(NewPetAdoptionPostVM post)
        {
            throw new NotImplementedException();
        }


        //GET: Post/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewPetAdoptionPostVM post)
        {
            throw new NotImplementedException();

        }
    }
}
