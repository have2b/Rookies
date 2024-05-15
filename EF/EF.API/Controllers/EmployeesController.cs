using EF.API.DTOs;
using EF.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _service;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(
            EmployeeService service,
            ILogger<EmployeesController> logger
        )
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _service.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving employees");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            try
            {
                var employee = await _service.GetEmployee(id);
                if (employee == null)
                {
                    return BadRequest($"Can't find user that have id: {id}");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving employee");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] EmployeeDTO model)
        {
            try
            {
                if (model is null)
                {
                    return BadRequest("Body can't be null");
                }
                var employee = _service.Add(model).Result;
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding employee");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeDTO model)
        {
            try
            {
                if (model is null)
                {
                    return BadRequest("Body can't be null");
                }
                var employee = _service.Update(id, model).Result;
                if (employee is null)
                {
                    return BadRequest($"Can't find user that have id: {id}");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating employee");
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
                _logger.LogError(ex, "Error occurred while deleting employee");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
