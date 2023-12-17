using System.ComponentModel.DataAnnotations;

namespace BilConnect.Data.Validation
{
    public class ValidateDateRangeForEventTicketPosts : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;

            if (dt >= DateTime.Now.AddHours(3))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("You should create the event ticket post at least 3 hours before event time.");
            }
        }
    }
}
