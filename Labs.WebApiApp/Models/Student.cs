namespace Labs.WebApiApp.Models
{
    public class Student
    {
        // Id
        public int Id { get; set; }

        // Name
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

        public int DepartmentId { get; set; }
        public  Department? Department { get; set; }

    }
}
