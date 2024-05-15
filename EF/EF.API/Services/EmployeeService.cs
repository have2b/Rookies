using EF.API.DTOs;
using EF.Domain.Entities;
using EF.Domain.Interfaces;

namespace EF.API.Services
{
    public class EmployeeService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IUnitOfWork unit, ILogger<EmployeeService> logger)
        {
            _unit = unit;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                return await _unit.Employees.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(GetEmployees)} at EmployeeService");
                throw;
            }
        }

        public async Task<Employee> GetEmployee(Guid id)
        {
            try
            {
                return await _unit.Employees.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(GetEmployee)} at EmployeeService");
                throw;
            }
        }

        public async Task<Employee> Add(EmployeeDTO model)
        {
            try
            {
                var department = new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    DepartmentId = model.DepartmentId,
                    Salary = new Salary { Id = Guid.NewGuid(), Amount = model.Amount }
                };
                await _unit.Employees.AddAsync(department);
                await _unit.SaveAsync();
                return department;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(Add)} at EmployeeService");
                throw;
            }
        }

        public async Task<Employee> Update(Guid id, EmployeeDTO model)
        {
            try
            {
                var department = await _unit.Employees.GetByIdAsync(id);
                if (department is null)
                {
                    return null;
                }

                department.Name = model.Name;
                _unit.Employees.Update(department);
                await _unit.SaveAsync();

                return department;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(Update)} at EmployeeService");
                throw;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var department = await _unit.Employees.GetByIdAsync(id);
                if (department is null)
                {
                    return false;
                }

                _unit.Employees.Remove(department);
                await _unit.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(Delete)} at EmployeeService");
                throw;
            }
        }
    }
}
