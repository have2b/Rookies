using EFCore.DTOs;
using EFCore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _repository;
        private readonly ILogger<DepartmentsController> _logger;

        public DepartmentsController(IDepartmentRepository repository, ILogger<DepartmentsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var departments = _repository.GetDepartments();
                if (departments.Count == 0)
                {
                    return BadRequest("No data");
                }
                return Ok(departments);
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
                var result = _repository.GetDepartment(id);
                if (result is null)
                {
                    return BadRequest($"There is no department that has id: {id}");
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
        public IActionResult Add([FromBody] DepartmentDTO model)
        {
            try
            {
                var result = _repository.AddDepartment(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in adding data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] DepartmentDTO model)
        {
            try
            {
                var result = _repository.UpdateDepartment(id, model);
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
                var result = _repository.DeleteDepartment(id);
                if (result)
                {
                    return NoContent();
                }
                return BadRequest($"There is no department has id: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in deleting data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
