using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User
    {
        public int UserId { get; set; }

        [DisplayName ("Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [DisplayName ("Username")]
        public string name { get; set; }

        [Required]
        [DataType (DataType.Password)]
        [DisplayName("Password")]
        public string password { get; set; }
    }
}
