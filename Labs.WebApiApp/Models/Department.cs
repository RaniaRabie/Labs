
namespace Labs.WebApiApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public string MgrName { get; set; }


        /* -------------------------------------------------------------------------------  */

        // Navigation property => One to Many relation between Department and Employee
        public ICollection<Student> Students { get; set; } = new HashSet<Student>();

        /* -------------------------------------------------------------------------------  */
        public ICollection<Branch> Branches { get; set; } = new HashSet<Branch>();
    }
}
