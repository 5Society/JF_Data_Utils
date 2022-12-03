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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet()]
        public ActionResult<Teacher> Get(int page, int pageSize)
        {
            var results = _teacherService.GetAllTeachers(page, pageSize);
            return Ok(results);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = _teacherService.GetTeacherById(id);

            if (entity is null)
                return NotFound($"Entity with Id = {id} not found.");

            return Ok(entity);
        }

        [HttpPost]
        public ActionResult<Teacher> Post([FromBody] Teacher entity)
        {

            if (entity is null)
                return BadRequest(ModelState);

            bool result = _teacherService.AddTeacher(entity);

            if (!result)
                return BadRequest("Your changes have not been saved.");

            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Teacher entity)
        {
            if (entity is null)
                return BadRequest(ModelState);

            if (id != entity.Id)
                return BadRequest("Identifier is not valid or Identifiers don't match.");

            bool result = _teacherService.UpdateTeacher(id, entity);

            if (!result)
                return BadRequest("Your changes have not been saved.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool result = _teacherService.DeleteTeacher(id);

            if (!result) return NotFound($"Entity with Id = {id} not found");

            return NoContent();
        }
    }
}