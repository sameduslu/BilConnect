using System.ComponentModel.DataAnnotations;

namespace Bilconnect_First_Version.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        public string Logo { get; set; }

        [Display(Name = "Name")]
        public string Name {  get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; } 

        public List<Movie> Movies { get; set; }

    }
}
