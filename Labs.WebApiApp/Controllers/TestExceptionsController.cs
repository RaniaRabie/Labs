using Labs.WebApiApp.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Labs.WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestExceptionsController : ControllerBase
    {
        [HttpGet]
        //[TypeFilter(typeof(MyExceptionFilter))]
        public IActionResult Get()
        {
            throw new Exception("Error!");
        }
    }
}
