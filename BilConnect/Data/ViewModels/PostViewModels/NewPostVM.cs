using System;
using System.ComponentModel.DataAnnotations;
using BilConnect.Data.Enums;

namespace BilConnect.Data.ViewModels.PostViewModels
{
    public class NewPostVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public string? ImageURL { get; set; }

        public string? AdditionalImagesJson { get; set; }
        public List<string>? AdditionalImages { get; set; }
        public DateTime PostDate { get; set; }

        [Required(ErrorMessage = "Post Status is required.")]
        public PostStatus PostStatus { get; set; }

        //Relationships

        //User
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Post Type is required.")]
        public PostType PostType { get; set; }

        // Donation Post
        // No field for Donation Post

        //Selling Post
        [Display(Name = "Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        [RegularExpression(@"^\d+(\.\d{0,2})?$", ErrorMessage = "Price can have at most 2 decimal places.")]
        public double? PriceS { get; set; }

        //Borrowing Post
        public string? ReturnDate { get; set; } // Consider if you need [Required] based on logic

        [Display(Name = "Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        [RegularExpression(@"^\d+(\.\d{0,2})?$", ErrorMessage = "Price can have at most 2 decimal places.")]
        public double? PriceB { get; set; }

        //EventTicketPost
        public string? EventTime { get; set; } // Consider if you need [Required] based on logic

        public string? EventPlace { get; set; } // Consider if you need [Required] based on logic

        [Display(Name = "Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        [RegularExpression(@"^\d+(\.\d{0,2})?$", ErrorMessage = "Price can have at most 2 decimal places.")]
        public double? PriceE { get; set; }

        //LostItemPost
        public string? Place { get; set; } // Consider if you need [Required] based on logic

        //Pet adoption post
        public string? IsFullyVaccinated { get; set; } // Consider if you need [Required] based on logic
        public int? AgeInMonths { get; set; } // Consider if you need [Required] based on logic

        //Travelling Post
        public string? Origin { get; set; } // Consider if you need [Required] based on logic
        public string? Destination { get; set; } // Consider if you need [Required] based on logic
        public string? TravelTime { get; set; } // Consider if you need [Required] based on logic


        [Range(0, double.MaxValue, ErrorMessage = "Quota must be a non-negative number.")]
        public int? Quota { get; set; } // Consider if you need [Required] based on logic

        [Display(Name = "Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        [RegularExpression(@"^\d+(\.\d{0,2})?$", ErrorMessage = "Price can have at most 2 decimal places.")]
        public double? PriceT { get; set; }
    }
}