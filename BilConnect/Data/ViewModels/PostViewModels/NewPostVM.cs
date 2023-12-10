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

    }
}
