using System.ComponentModel.DataAnnotations;


namespace BilConnect.Data.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
