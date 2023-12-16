using BilConnect.Models;
using System.ComponentModel.DataAnnotations;

namespace BilConnect.Data.ViewModels
{
    public class NewClubEventVM
    {
        
        public int Id { get; set; }
        public ApplicationUser? ownerClub { get; set; }
        public string? ownerClubId { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public int? GE250_251Points { get; set; }
        public bool GE250_251Status { get; set; }
        public string Name { get; set; }
        [Range(0, 1000, ErrorMessage = "Quota must be an integer between 0 - 1000.")]
        public int quota { get; set; }
        public string? ImageURL { get; set; }
    }
}
