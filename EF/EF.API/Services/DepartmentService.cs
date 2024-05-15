using EF.API.DTOs;
using EF.Domain.Entities;
using EF.Domain.Interfaces;

namespace EF.API.Services
{
    public class DepartmentService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(IUnitOfWork unit, ILogger<DepartmentService> logger)
        {
            _unit = unit;
            _logger = logger;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            try
            {
                return await _unit.Departments.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(GetDepartments)} at DepartmentService");
                throw;
            }
        }

        public async Task<Department> GetDepartment(Guid id)
        {
            try
            {
                return await _unit.Departments.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(GetDepartment)} at DepartmentService");
                throw;
            }
        }

        public async Task<Department> Add(DepartmentDTO model)
        {
            try
            {
                var department = new Department() { Id = Guid.NewGuid(), Name = model.Name };
                await _unit.Departments.AddAsync(department);
                await _unit.SaveAsync();
                return department;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(Add)} at DepartmentService");
                throw;
            }
        }

        public async Task<Department> Update(Guid id, DepartmentDTO model)
        {
            try
            {
                var department = await _unit.Departments.GetByIdAsync(id);
                if (department is null)
                {
                    return null;
                }

                department.Name = model.Name;
                _unit.Departments.Update(department);
                await _unit.SaveAsync();

                return department;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(Update)} at DepartmentService");
                throw;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var department = await _unit.Departments.GetByIdAsync(id);
                if (department is null)
                {
                    return false;
                }

                _unit.Departments.Remove(department);
                await _unit.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(Delete)} at DepartmentService");
                throw;
            }
        }
    }
}
