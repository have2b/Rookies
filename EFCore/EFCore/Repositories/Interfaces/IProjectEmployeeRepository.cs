using EFCore.DTOs;
using EFCore.Models;

namespace EFCore.Repositories.Interfaces
{
    public interface IProjectEmployeeRepository
    {
        public List<ProjectEmployee>? GetProjectEmployeesForProject(Guid projectId);
        public ProjectEmployee? GetProjectEmployee(Guid empId, Guid projectId);
        public ProjectEmployee? AddProjectEmployee(Guid projectId, ProjectEmployeeDTO model);
        public ProjectEmployee? UpdateProjectEmployee(Guid projectId, ProjectEmployeeDTO model);
        public bool DeleteProjectEmployee(Guid projectId, Guid empId);
    }
}
