using BilConnect.Data.Base;
using BilConnect.Data.ViewModels.PostViewModels;
using BilConnect.Models.PostModels;

namespace BilConnect.Data.Services.PostServices
{
    public interface IPetAdoptionPostsService : IEntityBaseRepository<PetAdoptionPost>
    {
        Task AddNewPostAsync(NewPetAdoptionPostVM data);
        Task UpdatePostAsync(NewPetAdoptionPostVM data);
    }
}
