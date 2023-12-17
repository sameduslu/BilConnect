using BilConnect.Data.Base;
using BilConnect.Data.ViewModels;
using BilConnect.Models;

namespace BilConnect.Data.Services
{
    public interface IClubEventsService : IEntityBaseRepository<ClubEvent>
    {
        Task<ClubEvent> GetClubEventByIdAsync(int id);
        Task AddNewClubEventAsync(NewClubEventVM data);
        Task UpdateClubEventAsync(NewClubEventVM data);
    }
}
