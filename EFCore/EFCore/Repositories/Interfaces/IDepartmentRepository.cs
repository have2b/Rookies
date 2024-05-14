using EFCore.DTOs;
using EFCore.Models;

namespace EFCore.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        public List<Department> GetDepartments();
        public Department? GetDepartment(Guid id);
        public Department AddDepartment(DepartmentDTO department);
        public Department? UpdateDepartment(Guid id, DepartmentDTO model);
        public bool DeleteDepartment(Guid id);
    }
}
