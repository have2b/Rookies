using EF.API.DTOs;
using EF.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentService _service;
        private readonly ILogger<DepartmentsController> _logger;

        public DepartmentsController(
            DepartmentService service,
            ILogger<DepartmentsController> logger
        )
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var departments = await _service.GetDepartments();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving departments");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetDepartment(Guid id)
        {
            try
            {
                var department = await _service.GetDepartment(id);
                if (department == null)
                {
                    return BadRequest($"Can't find user that have id: {id}");
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving department");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DepartmentDTO model)
        {
            try
            {
                if (model is null)
                {
                    return BadRequest("Body can't be null");
                }
                var department = _service.Add(model).Result;
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding department");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DepartmentDTO model)
        {
            try
            {
                if (model is null)
                {
                    return BadRequest("Body can't be null");
                }
                var department = _service.Update(id, model).Result;
                if (department is null)
                {
                    return BadRequest($"Can't find user that have id: {id}");
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating department");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = _service.Delete(id).Result;
                if (result)
                {
                    return NoContent();
                }
                return BadRequest($"Can't find user that have id: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting department");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
