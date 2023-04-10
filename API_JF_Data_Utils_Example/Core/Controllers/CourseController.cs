using API_JF_Data_Utils_Example.Core.Interfaces;
using API_JF_Data_Utils_Example.Core.Models;
using API_JF_Data_Utils_Example.Core.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_JF_Data_Utils_Example.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet()]
        public ActionResult<Course> Get(int page, int pageSize)
        {
            var results = _courseService.GetAllCourses(page, pageSize);
            return Ok(results);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = _courseService.GetCourseById(id);

            if (entity is null)
                return NotFound($"Entity with Id = {id} not found.");

            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Course>> Post([FromBody] Course entity)
        {

            if (entity is null)
                return BadRequest(ModelState);

            bool result = await _courseService.AddCourse(entity);

            if (!result)
                return BadRequest("Your changes have not been saved.");

            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Course entity)
        {
            if (entity is null)
                return BadRequest(ModelState);

            if (id != entity.Id)
                return BadRequest("Identifier is not valid or Identifiers don't match.");

            bool result = await _courseService.UpdateCourse(id, entity);

            if (!result)
                return BadRequest("Your changes have not been saved.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _courseService.DeleteCourse(id);

            if (!result) return NotFound($"Entity with Id = {id} not found");

            return NoContent();
        }
    }
}