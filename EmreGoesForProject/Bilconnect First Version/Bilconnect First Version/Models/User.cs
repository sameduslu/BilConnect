using System.ComponentModel.DataAnnotations;

namespace Bilconnect_First_Version.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "User Photo")]
        public string ImageURL { get; set; }

        [Display(Name = "Name")]
        public string Name {  get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; } 

        public List<Post> Posts { get; set; }



    }
}
