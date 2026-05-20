using Labs.WebApiApp.Context;
using Labs.WebApiApp.DTOs.Department;
using Labs.WebApiApp.Models;
using System.ComponentModel.DataAnnotations;

namespace Labs.WebApiApp.Validators
{
    public class UniqueDepartmentNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext))!;

            var name = value?.ToString();
            var entity = (DepartmentCreateDto)validationContext.ObjectInstance;
            var exists = context.Departments.Any(d => d.Name == name && d.Id != entity.Id);

            if (exists)
            {
                return new ValidationResult("Department Name must be unique");
            }

            return ValidationResult.Success;
        }
    }
}
