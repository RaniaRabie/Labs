namespace Labs.WebApiApp.DTOs.Student
{
    public class StudentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // SSN
        public string SSN { get; set; }

        public string Email { get; set; }

        // ImageUrl
        public string ImageUrl { get; set; }

        // Age
        public int Age { get; set; }

        public DateTime? DoB { get; set; }

        // Address
        public string Address { get; set; }

        // Level
        public string Level { get; set; }

        public string DepartmentName { get; set; }
    }
}
