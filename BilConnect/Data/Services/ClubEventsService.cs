using Azure.Messaging;
using BilConnect.Data.Base;
using BilConnect.Data.Services.PostServices;
using BilConnect.Data.ViewModels;
using BilConnect.Models;
using BilConnect.Models.PostModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System.Security.Claims;

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
            ClubEvent clubEvent = null;
            clubEvent = new ClubEvent {
                Id = clubEventVM.Id,
                ownerClub = clubEventVM.ownerClub,
                ownerClubId = clubEventVM.ownerClubId,
                Place = clubEventVM.Place,
                Description = clubEventVM.Description,
                startTime = clubEventVM.startTime,
                Duration = clubEventVM.Duration,
                GE250_251Points = clubEventVM.GE250_251Points,
                GE250_251Status = clubEventVM.GE250_251Status,
                Name = clubEventVM.Name,
                quota = clubEventVM.quota,
                ImageURL = clubEventVM.ImageURL,
                CreationTime = DateTime.Now
            };            

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
            clubEvent.Duration = clubEventVM.Duration;
            clubEvent.GE250_251Points = clubEventVM.GE250_251Points;
            clubEvent.GE250_251Status = clubEventVM.GE250_251Status;
            clubEvent.Name = clubEventVM.Name;
            clubEvent.quota = clubEventVM.quota;
            clubEvent.ImageURL = clubEventVM.ImageURL;

            await _context.SaveChangesAsync();
        }
    }
}
