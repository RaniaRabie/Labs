using Labs.WebApiApp.DTOs.Student;
using Labs.WebApiApp.Models;
using System.ComponentModel.DataAnnotations;

namespace Labs.WebApiApp.Validators
{
    public class DateInPastAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dob = value as DateTime?;

            if (dob == null)
                return ValidationResult.Success;

            // 1. Date must be in past
            if (dob > DateTime.Now)
            {
                return new ValidationResult(ErrorMessage ?? "Date must be in the past.");
            }

            
            var student = (StudentCreateDto)validationContext.ObjectInstance;

            // Calc Age
            var today = DateTime.Today;
            int calculatedAge = today.Year - dob.Value.Year;

            
            if (dob.Value.Date > today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            
            if (student.Age != calculatedAge)
            {
                return new ValidationResult("Age does not match Date of Birth.");
            }

            return ValidationResult.Success;
        }
    }
}
