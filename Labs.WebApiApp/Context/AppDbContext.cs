using Labs.WebApiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Labs.WebApiApp.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var departments = new List<Department>
            {
                new Department { Id = 1, Number = 10 ,Name = "HR", Location = "Alex", MgrName = "Ali"},
                new Department { Id = 2, Number = 20, Name = "IT", Location = "Alex", MgrName = "Ali"},
                new Department { Id = 3, Number = 30, Name = "Finance", Location = "Giza", MgrName = "Mai"},
                new Department { Id = 4, Number = 40, Name = "Marketing", Location = "Giza", MgrName = "Mai" }
            };

            // Seed Data for Students
            var students = new List<Student>
            {
                new Student {Id = 1, Name = "Ali", SSN = "3030", Email = "ali@mail.com", ImageUrl = "img1.png", Address = "Alex",
                Age = 25, Level = "Junior", DoB = new DateTime(2001, 8, 27), DepartmentId = 1
                },

                new Student {Id = 2, Name = "Mai", SSN = "3050", Email = "mai@mail.com", ImageUrl = "img2.png", Address = "Giza",
                Age = 23, Level = "Junior", DoB = new DateTime(2003, 5, 4), DepartmentId = 3
                }
            };

            modelBuilder.Entity<Department>().HasData(departments);
            modelBuilder.Entity<Student>().HasData(students);

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }
    }
}
