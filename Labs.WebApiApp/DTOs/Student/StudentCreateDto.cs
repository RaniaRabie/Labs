using Labs.WebApiApp.Validators;
using System.ComponentModel.DataAnnotations;

namespace Labs.WebApiApp.DTOs.Student
{
    public class StudentCreateDto
    {
        public int? Id { get; set; }

        [MinLength(3, ErrorMessage = "Name Must be at least 3 characters")]
        [MaxLength(20, ErrorMessage = "Name Shoudn't be more than 20 characters")]
        public string Name { get; set; }

        public string? SSN { get; set; }

        [UniqueEmail]
        public string Email { get; set; }

        [RegularExpression(@"^.*\.(jpg|JPG|png|PNG|jpeg|JPEG)$",
        ErrorMessage = "Invalid image file format. Only JPG, JPEG, and PNG are allowed.")]

        public string ImageUrl { get; set; }

        [DateInPast]
        public DateTime? DoB {  get; set; }

        [Range(18, 22)]
        public int Age { get; set; }

        public string Address { get; set; }

        public string Level { get; set; }

        public int DepartmentId { get; set; }
    }
}
