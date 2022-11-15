using API_JF_Data_Utils_Example.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_JF_Data_Utils_Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogueController : ControllerBase
    {

        [HttpGet(Name = "GetList")]
        public IActionResult Get()
        {
            return BadRequest("Not implemented");
        }
    }
}