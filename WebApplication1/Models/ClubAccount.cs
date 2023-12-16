using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication1.Models
{
    public class ClubAccount : User
    {
        public ClubAccount() { }

        [Required]
        [StringLength(6, MinimumLength = 1)]
        [DisplayName("Club Abbrevation")]
        public string Abbrevation { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo {  get; set; }
        
    }
}
