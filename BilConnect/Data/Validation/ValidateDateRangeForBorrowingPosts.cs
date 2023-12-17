using System.ComponentModel.DataAnnotations;

namespace BilConnect.Data.Validation
{
    public class ValidateDateRangeForBorrowingPosts : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;

            if (dt >= DateTime.Now.AddDays(7))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("You should create the borrowing post at least 1 week before return time.");
            }
        }
    }
}
