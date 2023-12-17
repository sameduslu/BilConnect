
using System.ComponentModel.DataAnnotations;

namespace BilConnect.Data.Validation
{
    internal class ValidateDateRangeForClubEvents : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return ValidationResult.Success;
            }
            
            DateTime dt = (DateTime)value;

            

            if (dt >= DateTime.Now.AddDays(1))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("You should create the club event at least 1 day before start time.");
            }
        }
    }
}