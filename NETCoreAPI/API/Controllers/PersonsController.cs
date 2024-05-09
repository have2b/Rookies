using System.Net;
using API.Interfaces;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonsController> _logger;

        public PersonsController(IPersonService personService, ILogger<PersonsController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll(string? name, GenderType? gender, string? birthPlace)
        {
            try
            {
                var people = _personService.GetAll(name, gender, birthPlace);
                return Ok(people);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all persons");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] PersonDTO personDTO)
        {
            try
            {
                var person = _personService.Add(personDTO);
                if (person is null)
                {
                    return BadRequest(new { Message = "Can't add person" });
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding person");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] PersonDTO personDTO)
        {
            try
            {
                var person = _personService.Update(id, personDTO);
                if (person is null)
                {
                    return BadRequest(new { Message = $"Can't found person with ID: {id}" });
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating person");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var isDeleted = _personService.Delete(id);
                if (!isDeleted)
                {
                    return BadRequest(new { Message = $"Can't found person with ID: {id}" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting person");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
