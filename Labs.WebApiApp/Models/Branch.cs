namespace Labs.WebApiApp.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Department> Departments { get; set; } = new HashSet<Department>();
    }
}
