using Labs.WebApiApp.Context;
using Labs.WebApiApp.DTOs.Student;
using Labs.WebApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Labs.WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        /* -----------------------------------------------------------------------  */

        // Get All Students
        [HttpGet]
        public ActionResult<List<StudentReadDto>> Get()
        {
            var students = _context.Students.Include(d => d.Department).ToList();

            if (students is null || students.Count == 0)
            {
                return NotFound("No students found.");
            }

            // craete list of StudentReadDto
            var studentsDto = new List<StudentReadDto>();
            foreach (var item in students)
            {
                var dto = new StudentReadDto();

                dto.Id = item.Id;
                dto.Name = item.Name;
                dto.SSN = item.SSN;
                dto.Email = item.Email;
                dto.Address = item.Address;
                dto.ImageUrl = item.ImageUrl;
                dto.Age = item.Age;
                dto.DoB = item.DoB;
                dto.Level = item.Level;
                dto.DepartmentName = item.Department.Name;

                studentsDto.Add(dto);
            }
            
            return Ok(studentsDto);
        }

        /* -----------------------------------------------------------------------  */

        // Get Student By Id
        [HttpGet("{id:int}")]
        public ActionResult<StudentReadDto> GetById([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID.");
            }
            var student = _context.Students.Include(d=> d.Department).FirstOrDefault(s=> s.Id == id);
            if (student is null)
            {
                return NotFound("Student not found.");
            }

            var studentDto = new StudentReadDto();

            studentDto.Id = student.Id;
            studentDto.Name = student.Name;
            studentDto.SSN = student.SSN;
            studentDto.Email = student.Email;
            studentDto.Address = student.Address;
            studentDto.ImageUrl = student.ImageUrl;
            studentDto.Age = student.Age;
            studentDto.DoB = student.DoB;
            studentDto.Level = student.Level;
            studentDto.DepartmentName = student.Department.Name;


            return Ok(studentDto);
        }

        /* -----------------------------------------------------------------------  */

        // Get Student By SSN
        [HttpGet("{ssn:alpha}")]
        public ActionResult<StudentReadDto> GetById([FromRoute] string ssn)
        {
            if (string.IsNullOrEmpty(ssn))
            {
                return BadRequest("Invalid student SSN.");
            }
            var student = _context.Students.FirstOrDefault(s => s.SSN == ssn);
            if (student is null)
            {
                return NotFound("Student with SSN " + ssn + " not found.");
            }
            var studentDto = new StudentReadDto();

            studentDto.Id = student.Id;
            studentDto.Name = student.Name;
            studentDto.SSN = student.SSN;
            studentDto.Email = student.Email;
            studentDto.Address = student.Address;
            studentDto.ImageUrl = student.ImageUrl;
            studentDto.Age = student.Age;
            studentDto.DoB = student.DoB;
            studentDto.Level = student.Level;
            studentDto.DepartmentName = student.Department.Name;


            return Ok(studentDto);
        }

        /* -----------------------------------------------------------------------  */

        // Add New Student
        [HttpPost]
        public ActionResult AddStudent(StudentCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var student = new Student
            {
                Name = dto.Name,
                SSN = dto.SSN,
                Email = dto.Email,
                Address = dto.Address,
                ImageUrl = dto.ImageUrl,
                Age = dto.Age,
                DoB = dto.DoB,
                Level = dto.Level,
                DepartmentId = dto.DepartmentId
            };

            _context.Students.Add(student);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, dto);
        }

        /* -----------------------------------------------------------------------  */

        // Update Student
        [HttpPut("{id:int}")]
        public ActionResult UpdateStudent(int id, StudentCreateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Student ID mismatch.");
            }
            var existingStudent = _context.Students.Find(id);
            if (existingStudent is null)
            {
                return NotFound("Student with ID " + id + " not found.");
            }
            existingStudent.Name = dto.Name;
            existingStudent.Age = dto.Age;
            existingStudent.Email = dto.Email;
            existingStudent.ImageUrl = dto.ImageUrl;
            existingStudent.Address = dto.Address;
            existingStudent.Level = dto.Level;
            existingStudent.DepartmentId = dto.DepartmentId;
            
            _context.SaveChanges();
            return NoContent();

        }

        /* -----------------------------------------------------------------------  */
 
        // Delete Student
        [HttpDelete("{id:int}")]
        public ActionResult DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID.");
            }
            var student = _context.Students.Find(id);
            if(student is null)
            {
                return NotFound("Student with ID " + id + " not found.");
            }

            _context.Students.Remove(student);
            _context.SaveChanges();
            return Ok(new { Message = "Student deleted successfully." , Data = student }); //
        }
    }
}
