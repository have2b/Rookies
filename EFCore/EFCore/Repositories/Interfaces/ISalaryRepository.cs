using EFCore.DTOs;
using EFCore.Models;

namespace EFCore.Repositories.Interfaces
{
    public interface ISalaryRepository
    {
        public Salary? GetSalary(Guid departmentId, Guid empId, Guid id);
        public Salary? AddSalary(Guid departmentId, Guid empId, SalaryDTO model);
        public Salary? UpdateSalary(Guid departmentId, Guid empId, Guid id, SalaryDTO model);
        public bool DeleteSalary(Guid departmentId, Guid empId, Guid id);
    }
}
