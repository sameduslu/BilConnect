using Bilconnect_First_Version.Models;

namespace Bilconnect_First_Version.data.Services
{
    public interface IActorsService
    {
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<Actor> GetByIdAsync(int id);

        Task AddAsync(Actor actor);

        Actor Update(int id, Actor newActor);

        void Delete(int id);
    }
}
