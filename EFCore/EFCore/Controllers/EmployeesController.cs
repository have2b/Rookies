using System.Net;
using EFCore.DTOs;
using EFCore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Controllers
{
    [ApiController]
    [Route("api/departments/{departmentId}/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(
            IEmployeeRepository repository,
            ILogger<EmployeesController> logger
        )
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll(Guid departmentId)
        {
            try
            {
                var employees = _repository.GetEmployees(departmentId);
                if (employees is null)
                {
                    return BadRequest("No data");
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid departmentId, Guid id)
        {
            try
            {
                var employee = _repository.GetEmployee(departmentId, id);
                if (employee is null)
                {
                    return BadRequest($"There is no employee that has id: {id}");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Create(Guid departmentId, [FromBody] EmployeeDTO model)
        {
            try
            {
                if (model is null)
                {
                    return BadRequest("Body can't be null");
                }
                var employee = _repository.AddEmployee(departmentId, model);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in creating data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid departmentId, Guid id, [FromBody] EmployeeDTO model)
        {
            try
            {
                if (model is null)
                {
                    return BadRequest("Body can't be null");
                }
                var employee = _repository.UpdateEmployee(departmentId, id, model);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in updating data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid departmentId, Guid id)
        {
            try
            {
                var result = _repository.DeleteEmployee(departmentId, id);
                if (result)
                {
                    return NoContent();
                }
                return BadRequest($"There is no employee has id: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in deleting data");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("with-department-names")]
        public IActionResult GetEmployeesWithDepartmentNames()
        {
            try
            {
                var employees = _repository.GetEmployeesWithDepartmentNames();
                if (employees is null || !employees.Any())
                {
                    return NoContent();
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting employees with department names");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("with-projects")]
        public IActionResult GetEmployeesWithProjects()
        {
            try
            {
                var employees = _repository.GetEmployeesWithProjects();
                if (employees is null || !employees.Any())
                {
                    return NoContent();
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting employees with projects");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("with-salary-and-joineddate-filter")]
        public IActionResult GetEmployeesWithSalaryAndJoinedDateFilter()
        {
            try
            {
                var employees = _repository.GetEmployeesWithSalaryAndJoinedDateFilter();
                if (employees is null || !employees.Any())
                {
                    return NoContent();
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting employees with salary and joined date filter");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
