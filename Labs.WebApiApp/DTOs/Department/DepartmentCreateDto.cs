using Labs.WebApiApp.Validators;

namespace Labs.WebApiApp.DTOs.Department
{
    public class DepartmentCreateDto
    {
        public int? Id { get; set; }
        public int Number { get; set; }

        [UniqueDepartmentName]
        public string Name { get; set; }
        public string Location { get; set; }

        public string MgrName { get; set; }
        public List<int> BranchIds { get; set; } = new();
    }
}
