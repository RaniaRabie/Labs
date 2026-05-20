using Labs.WebApiApp.Context;
using Labs.WebApiApp.DTOs.Branch;
using Labs.WebApiApp.DTOs.Department;
using Labs.WebApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labs.WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BranchesController(AppDbContext context)
        {
            _context = context;
        }

        // Get All Branches
        [HttpGet]
        public ActionResult<List<BranchReadDto>> GetAllBranches()
        {
            var branches = _context.Branches.ToList();
            if (branches is null || branches.Count == 0)
            {
                return NotFound("No Departments found.");
            }

            var result = branches.Select(d => new BranchReadDto
            {
                Id = d.Id,
                Name = d.Name,
                
            });

            return Ok(result);
        }

    }
}
