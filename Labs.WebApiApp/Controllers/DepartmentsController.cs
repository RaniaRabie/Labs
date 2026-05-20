using Labs.WebApiApp.Context;
using Labs.WebApiApp.DTOs.Department;
using Labs.WebApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace Labs.WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }

        // Get All Departments
        [HttpGet]
        public ActionResult<List<DepartmentReadDto>> GetAllDepartments()
        {
            var departments = _context.Departments
                .Include(d => d.Branches)
                .Include(d=> d.Students)
                .ToList();
            if (departments is null || departments.Count == 0)
            {
                return NotFound("No Departments found.");
            }



            var result = departments.Select(d => new DepartmentReadDto
            {
                Id = d.Id,
                Number = d.Number,
                Name = d.Name,
                Location = d.Location,
                MgrName = d.MgrName,
                Branches = d.Branches.Select(b => b.Name).ToList(),
                StudentNames = d.Students.Select(s => s.Name).ToList(),
                StatusCount = d.Students.Count > 1 ? "Overload" : "Normal"
            });

            return Ok(result);

        }

        /* -----------------------------------------------------------------------  */

        // Get Department By Id
        [HttpGet("{id:int}")]
        public ActionResult<DepartmentReadDto> GetDepartmentById([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid department Id.");
            }
            var department = _context.Departments
                .Include(d => d.Branches)
                .Include(d => d.Students)
                .FirstOrDefault(d => d.Id == id);
            if (department is null)
            {
                return NotFound("Department not found.");
            }

            

            var result = new DepartmentReadDto
            {
                Id = department.Id,
                Name = department.Name,
                Number = department.Number,
                MgrName = department.MgrName,
                Location = department.Location,
                Branches = department.Branches.Select(b => b.Name).ToList(),
                StudentNames = department.Students.Select(s => s.Name).ToList(),
                StatusCount = department.Students.Count > 1 ? "Overload" : "Normal" ,
            };

            return Ok(result);
        }

        /* -----------------------------------------------------------------------  */

        // Get department
        [HttpGet("{name:alpha}")]
        public ActionResult<DepartmentReadDto> GetById([FromRoute] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Invalid Department Name.");
            }
            var department = _context.Departments.FirstOrDefault(d => d.Name == name);
            if (department is null)
            {
                return NotFound("Department with Name " + name + " not found.");
            }

            var departmentDto = new DepartmentReadDto();

            departmentDto.Id = department.Id;
            departmentDto.Name = department.Name;
            departmentDto.Number = department.Number;
            departmentDto.MgrName = department.MgrName;
            departmentDto.Location = department.Location;


            return Ok(departmentDto);

        }

        /* -----------------------------------------------------------------------  */

        // Add New Department
        [HttpPost]
        public ActionResult AddDepartment(DepartmentCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var department = new Department
            {
                Number = dto.Number,
                Name = dto.Name,
                Location = dto.Location,
                MgrName = dto.MgrName,
                
            };

            var branches = _context.Branches
            .Where(b => dto.BranchIds.Contains(b.Id))
            .ToList();

            department.Branches = branches;

            _context.Departments.Add(department);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, dto);
        }

        /* -----------------------------------------------------------------------  */

        // Update Department
        [HttpPut("{id:int}")]
        public ActionResult UpdateDepartment(int id, DepartmentCreateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Department ID mismatch.");
            }
            var existingDepartment = _context.Departments.Find(id);
            if (existingDepartment is null)
            {
                return NotFound("department with ID " + id + " not found.");
            }

            existingDepartment.Number = dto.Number;
            existingDepartment.Name = dto.Name;
            existingDepartment.Location = dto.Location;
            existingDepartment.MgrName = dto.MgrName;
            
            _context.SaveChanges();
            return NoContent();

        }

        /* -----------------------------------------------------------------------  */

        // Delete Department
        [HttpDelete("{id:int}")]
        public ActionResult DeleteDepartment(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid department ID.");
            }
            var department = _context.Departments.Find(id);
            if (department is null)
            {
                return NotFound("department with ID " + id + " not found.");
            }

            _context.Departments.Remove(department);
            _context.SaveChanges();
            return Ok(new { Message = "Department deleted successfully.", Data = department }); 
        }
    }
}
