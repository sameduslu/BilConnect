using BilConnect.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using BilConnect.Data.Validation;

namespace BilConnect.Data.ViewModels
{
    public class NewClubEventVM
    {
        
        public int Id { get; set; }

        [DisplayName("Organizer Club")]
        public ApplicationUser? ownerClub { get; set; }

        public string? ownerClubId { get; set; }

        [StringValidator (MinLength = 1, MaxLength = 100)]
        [Required(ErrorMessage = "Place is required.")]
        public string Place { get; set; }

        [StringValidator(MinLength = 1, MaxLength = 300)]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }


        [DisplayName("Starting Time")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Starting time is required.")]
        [ValidateDateRangeForClubEvents]
        public DateTime? startTime { get; set; }


        [DisplayName("Duration In Minutes")]
        [Range(30, 10080, ErrorMessage = "Duration must be an integer between 30 - 10080.")]
        [Required(ErrorMessage = "Duration is required.")]
        public int Duration { get; set; }


        [DisplayName("GE250/251 Points")]
        [Range(5, 150, ErrorMessage = "GE250/251 Points must be an integer between 5 - 150.")]
        public int? GE250_251Points { get; set; }


        [DisplayName("Will GE250/251 Points Be Given")]
        public bool GE250_251Status { get; set; }


        [DisplayName("Activity Name")]
        [StringValidator(MinLength = 1, MaxLength = 75)]
        [Required(ErrorMessage = "Activity Name is required.")]
        public string Name { get; set; }


        [DisplayName("Quota")]
        [Required(ErrorMessage = "Quota is required.")]
        [Range(1, 1000, ErrorMessage = "Quota must be an integer between 1 - 1000.")]
        public int quota { get; set; }


        [DisplayName("Image")]
        public string? ImageURL { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}
