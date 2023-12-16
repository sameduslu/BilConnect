using Azure.Messaging;
using BilConnect.Data.Base;
using BilConnect.Data.Services.PostServices;
using BilConnect.Data.ViewModels;
using BilConnect.Models;
using BilConnect.Models.PostModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BilConnect.Data.Services
{
    public class ClubEventsService : EntityBaseRepository<ClubEvent>, IClubEventsService
    {
        private readonly AppDbContext _context;
        public ClubEventsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewClubEventAsync(NewClubEventVM clubEventVM)
        {
            var clubEvent = new ClubEvent();
            clubEvent.Id = clubEventVM.Id;
            clubEvent.ownerClub = clubEventVM.ownerClub;
            clubEvent.ownerClubId = clubEventVM.ownerClubId;
            clubEvent.Place = clubEventVM.Place;
            clubEvent.Description = clubEventVM.Description;
            clubEvent.startTime = clubEventVM.startTime;
            clubEvent.endTime = clubEventVM.endTime;
            clubEvent.GE250_251Points = clubEventVM.GE250_251Points;
            clubEvent.GE250_251Status = clubEventVM.GE250_251Status;
            clubEvent.Name = clubEventVM.Name;
            clubEvent.quota = clubEventVM.quota;
            clubEvent.ImageURL = clubEventVM.ImageURL;

            await _context.ClubEvents.AddAsync(clubEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<ClubEvent> GetClubEventByIdAsync(int id)
        {
            var clubEvent = await _context.ClubEvents.Include(u  => u.ownerClub).FirstOrDefaultAsync(n => n.Id == id);
            return clubEvent;
        }

        public async Task UpdateClubEventAsync(NewClubEventVM clubEventVM)
        {
            var clubEvent = await _context.ClubEvents.FirstOrDefaultAsync(n => n.Id == clubEventVM.Id);
            if (clubEvent == null)
            {
                // Handle the case where the club event is not found
                return;
            }
            clubEvent.Id = clubEventVM.Id;
            clubEvent.ownerClub = clubEventVM.ownerClub;
            clubEvent.ownerClubId = clubEventVM.ownerClubId;
            clubEvent.Place = clubEventVM.Place;
            clubEvent.Description = clubEventVM.Description;
            clubEvent.startTime = clubEventVM.startTime;
            clubEvent.endTime = clubEventVM.endTime;
            clubEvent.GE250_251Points = clubEventVM.GE250_251Points;
            clubEvent.GE250_251Status = clubEventVM.GE250_251Status;
            clubEvent.Name = clubEventVM.Name;
            clubEvent.quota = clubEventVM.quota;
            clubEvent.ImageURL = clubEventVM.ImageURL;

            await _context.SaveChangesAsync();
        }
    }
}
