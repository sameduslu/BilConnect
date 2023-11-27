using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bilconnect_First_Version.data.enums;

namespace Bilconnect_First_Version.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string ImageURL {  get; set; }

        public DateTime PostDate { get; set; }


        //Relationshpis

        //User
        public int UserId { get; set; }
        [ForeignKey("UserId")]

        // Navigation property for User
        public virtual User User { get; set; }

    }
}
