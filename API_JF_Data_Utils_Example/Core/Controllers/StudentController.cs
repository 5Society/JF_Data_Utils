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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet()]
        public ActionResult<Student> Get()
        {
            var results = _studentService.GetAllStudents();
            return Ok(results);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _studentService.GetStudentById(id);

            if (entity is null)
                return NotFound($"Entity with Id = {id} not found.");

            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Post([FromBody] Student entity)
        {

            if (entity is null)
                return BadRequest(ModelState);

            bool result = await _studentService.AddStudent(entity);

            if (!result)
                return BadRequest("Your changes have not been saved.");

            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Student entity)
        {
            if (entity is null)
                return BadRequest(ModelState);

            if (id != entity.Id)
                return BadRequest("Identifier is not valid or Identifiers don't match.");

            bool result = await _studentService.UpdateStudent(id, entity);

            if (!result)
                return BadRequest("Your changes have not been saved.");

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Student> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest(ModelState);

            var existEntity = await _studentService.GetByIdAsync(id);
            if (existEntity is null)
                return NotFound($"Entity with Id = {id} not found");

            patchDoc.ApplyTo(existEntity, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            var isValid = TryValidateModel(existEntity);
            if (!isValid)
                return BadRequest(ModelState);

            try
            {
                await _studentService.UnitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool result = await _studentService.DeleteStudent(id);

            if (!result) return NotFound($"Entity with Id = {id} not found");

            return NoContent();
        }
    }
}