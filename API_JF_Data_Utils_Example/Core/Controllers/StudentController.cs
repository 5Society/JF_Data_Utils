using API_JF_Data_Utils_Example.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_JF_Data_Utils_Example.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {

        [HttpGet(Name = "GetList")]
        public IActionResult Get()
        {
            return BadRequest("Not implemented");
        }
    }
}