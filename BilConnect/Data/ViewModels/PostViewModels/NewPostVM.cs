using BilConnect.Data.Enums;
using BilConnect.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilConnect.Data.ViewModels.PostViewModels
{
    public class NewPostVM
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public DateTime PostDate { get; set; }

        public PostStatus PostStatus { get; set; }

        //Relationshpis

        //User
        public string? UserId { get; set; }

        
        public PostType PostType { get; set; }


        // Donation Post
        // No field for Donation Post


        //Selling Post
        public double? PriceS { get; set; }

        //Borrowing Post
        public string? ReturnDate { get; set; }

        public double? PriceB { get; set; }

        //EventTicketPost
        public string? EventTime { get; set; }

        public string? EventPlace { get; set; }

        public double? PriceE { get; set; }


        //LostItemPost
        public string? Place { get; set; }


        //Pet adoption post
        public string? IsFullyVaccinated { get; set; }
        public int? AgeInMonths { get; set; }

        //Travelling Post

        public string? Origin { get; set; }

        public string? Destination { get; set; }

        public string? TravelTime { get; set; }

        public int? Quota { get; set; }

        public double? PriceT { get; set; }
    }
}
