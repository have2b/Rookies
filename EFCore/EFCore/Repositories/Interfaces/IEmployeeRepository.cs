using EFCore.DTOs;
using EFCore.Models;

namespace EFCore.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        public List<Employee>? GetEmployees(Guid departmentId);
        public Employee? GetEmployee(Guid departmentId, Guid id);
        public Employee? AddEmployee(Guid departmentId, EmployeeDTO model);
        public Employee? UpdateEmployee(Guid departmentId, Guid id, EmployeeDTO model);
        public bool DeleteEmployee(Guid departmentId, Guid id);

        IEnumerable<object> GetEmployeesWithDepartmentNames();
        IEnumerable<object> GetEmployeesWithProjects();
        IEnumerable<Employee> GetEmployeesWithSalaryAndJoinedDateFilter();
    }
}