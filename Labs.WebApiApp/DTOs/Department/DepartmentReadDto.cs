namespace Labs.WebApiApp.DTOs.Department
{
    public class DepartmentReadDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public List<string> Branches { get; set; } = new ();
        public string MgrName { get; set; }

        public string StatusCount { get; set; }

        public List<string> StudentNames { get; set; } = new();

    }
}
