using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Api.Validation
{
    public class NotWhiteSpaceAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            if (value is string str)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    return new ValidationResult(
                        $"{validationContext.DisplayName} cannot be empty");
                }
            }

            return ValidationResult.Success;
        }
    }
}
