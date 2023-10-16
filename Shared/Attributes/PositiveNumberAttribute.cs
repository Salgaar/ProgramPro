using System.ComponentModel.DataAnnotations;

namespace ProgramPro.Shared.Attributes
{
    public class PositiveNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int intValue && intValue <= 0 || value is double doubleValue && doubleValue <= 0)
            {
                return new ValidationResult("Tallet skal være positivt og større end 0.");
            }
            return ValidationResult.Success;
        }
    }
}
