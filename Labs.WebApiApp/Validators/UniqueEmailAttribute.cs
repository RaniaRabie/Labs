using Labs.WebApiApp.Context;
using Labs.WebApiApp.DTOs.Student;
using Labs.WebApiApp.Models;
using System.ComponentModel.DataAnnotations;

namespace Labs.WebApiApp.Validators
{
    public class UniqueEmailAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext))!;

            var email = value?.ToString();
            var entity = (StudentCreateDto)validationContext.ObjectInstance;
            var exists = context.Students.Any(s => s.Email == email && s.Id != entity.Id);

            if (exists)
            {
                return new ValidationResult("Email must be unique");
            }

            return ValidationResult.Success;
        }
    }
}
