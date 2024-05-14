using EFCore.DTOs;
using EFCore.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EFCore.Controllers
{
    [Route("api/projects/{projectId}/[controller]")]
    [ApiController]
    public class ProjectEmployeesController : ControllerBase
    {
        private readonly IProjectEmployeeRepository _repository;
        private readonly ILogger<ProjectEmployeesController> _logger;

        public ProjectEmployeesController(
            IProjectEmployeeRepository repository,
            ILogger<ProjectEmployeesController> logger
        )
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll(Guid projectId)
        {
            try
            {
                var pes = _repository.GetProjectEmployeesForProject(projectId);
                if (pes is null)
                {
                    return BadRequest("Project not found");
                }
                return Ok(pes);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("employee")]
        public IActionResult GetByEmployeeId(Guid projectId, [FromQuery] Guid empId)
        {
            try
            {
                var pe = _repository.GetProjectEmployee(projectId, empId);
                if (pe is null)
                {
                    return BadRequest("ProjectEmployee not found");
                }
                return Ok(pe);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult AddProjectEmployee(Guid projectId, [FromBody] ProjectEmployeeDTO model)
        {
            try
            {
                if (model is null)
                {
                    return BadRequest("Body can't be null");
                }
                var pe = _repository.AddProjectEmployee(projectId, model);
                return Ok(pe);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error in adding data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("employee")]
        public IActionResult UpdateProjectEmployee(Guid projectId, [FromBody] ProjectEmployeeDTO model)
        {
            try
            {
                if (model is null)
                {
                    return BadRequest("Body can't be null");
                }
                var pe = _repository.UpdateProjectEmployee(projectId, model);
                return Ok(pe);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error in updating data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("employee")]
        public IActionResult DeleteProjectEmployee(Guid projectId, [FromQuery] Guid empId)
        {
            try
            {
                var result = _repository.DeleteProjectEmployee(projectId, empId);
                if (result)
                {
                    return NoContent();
                }
                return BadRequest($"There is no project employee has employee id: {empId}");
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error in deleting data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
