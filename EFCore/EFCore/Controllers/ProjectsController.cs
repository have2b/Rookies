using EFCore.DTOs;
using EFCore.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _repository;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(IProjectRepository repository, ILogger<ProjectsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var projects = _repository.GetProjects();
                if (projects.Count == 0)
                {
                    return BadRequest("No data");
                }
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = _repository.GetProject(id);
                if (result is null)
                {
                    return BadRequest($"There is no project that has id: {id}");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProjectDTO model)
        {
            try
            {
                var result = _repository.AddProject(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in adding data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] ProjectDTO model)
        {
            try
            {
                var result = _repository.UpdateProject(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in updating data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var result = _repository.DeleteProject(id);
                if (result)
                {
                    return NoContent();
                }
                return BadRequest($"There is no project has id: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in deleting data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
