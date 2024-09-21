using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Web.Validator
{
    public class GreaterThanZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Amount is required.");
            }

            // Assuming the value is a decimal. Adjust if using other numeric types.
            if (decimal.TryParse(value.ToString(), out decimal amount))
            {
                if (amount > 0)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Amount must be greater than zero.");
                }
            }

            return new ValidationResult("Invalid amount format.");
        }
    }
}
