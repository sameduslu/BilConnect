using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;
using System.Linq.Expressions;

namespace BilConnect.Data.Services.PostServices
{
    public class PetAdoptionPostsService : PostsService, IPetAdoptionPostsService
    {

        public PetAdoptionPostsService(AppDbContext context) : base(context)
        {

        }
        public Task AddAsync(PetAdoptionPost entity)
        {
            throw new NotImplementedException();
        }

        public Task AddNewPostAsync(NewPetAdoptionPostVM data)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PetAdoptionPost>> GetAllAsync(params Expression<Func<PetAdoptionPost, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, PetAdoptionPost newEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostAsync(NewPetAdoptionPostVM data)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<PetAdoptionPost>> IEntityBaseRepository<PetAdoptionPost>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<PetAdoptionPost> IEntityBaseRepository<PetAdoptionPost>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
