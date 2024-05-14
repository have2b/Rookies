using System.Net;
using EFCore.DTOs;
using EFCore.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Controllers
{
    [Route("api/departments/{departmentId}/employees/{empId}/[controller]")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly ISalaryRepository _repository;
        private readonly ILogger<SalariesController> _logger;

        public SalariesController(ISalaryRepository repository, ILogger<SalariesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid departmentId, Guid empId, Guid id)
        {
            try
            {
                var salary = _repository.GetSalary(departmentId, empId, id);
                if (salary is null)
                {
                    return BadRequest($"There is no salary that has id: {id}");
                }
                return Ok(salary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Create(Guid departmentId, Guid empId, [FromBody] SalaryDTO model)
        {
            try
            {
                if (model is null)
                {
                    return BadRequest("Body can't be null");
                }
                var salary = _repository.AddSalary(departmentId, empId, model);
                return Ok(salary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in creating data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(
            Guid departmentId,
            Guid empId,
            Guid id,
            [FromBody] SalaryDTO model
        )
        {
            try
            {
                if (model is null)
                {
                    return BadRequest("Body can't be null");
                }
                var salary = _repository.UpdateSalary(departmentId, empId, id, model);
                return Ok(salary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in updating data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid departmentId, Guid empId, Guid id)
        {
            try
            {
                var result = _repository.DeleteSalary(departmentId, empId, id);
                if (result)
                {
                    return NoContent();
                }
                return BadRequest($"There is no salary has id: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in deleting data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
