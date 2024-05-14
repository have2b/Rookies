using EFCore.DTOs;
using EFCore.Models;
using EFCore.Repositories.Interfaces;

namespace EFCore.Repositories
{
    public class ProjectEmployeeRepository : IProjectEmployeeRepository
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ApplicationDbContext _context;

        public ProjectEmployeeRepository(
            ApplicationDbContext context,
            IProjectRepository projectRepository
        )
        {
            _projectRepository = projectRepository;
            _context = context;
        }

        public ProjectEmployee? AddProjectEmployee(Guid projectId, ProjectEmployeeDTO model)
        {
            _ =
                _projectRepository.GetProject(projectId)
                ?? throw new Exception($"There is no project with id: {projectId}");

            var pe = new ProjectEmployee()
            {
                ProjectId = projectId,
                EmployeeId = model.EmployeeId,
                Enable = model.Enable
            };

            _context.ProjectEmployees.Add(pe);
            _context.SaveChanges();

            return pe;
        }

        public bool DeleteProjectEmployee(Guid projectId, Guid empId)
        {
            var pe = GetProjectEmployee(projectId, empId);
            if (pe is null)
            {
                return false;
            }
            _context.ProjectEmployees.Remove(pe);
            _context.SaveChanges();
            return true;
        }

        public ProjectEmployee? GetProjectEmployee(Guid projectId, Guid empId)
        {
            var project =
                _projectRepository.GetProject(projectId)
                ?? throw new Exception($"There is no project with id: {projectId}");

            return project.ProjectEmployees?.Where(pe => pe.EmployeeId == empId).FirstOrDefault();
        }

        public List<ProjectEmployee>? GetProjectEmployeesForProject(Guid projectId)
        {
            var project =
                _projectRepository.GetProject(projectId)
                ?? throw new Exception($"There is no project with id: {projectId}");

            return [.. project.ProjectEmployees];
        }

        public ProjectEmployee? UpdateProjectEmployee(Guid projectId, ProjectEmployeeDTO model)
        {
            _ =
                _projectRepository.GetProject(projectId)
                ?? throw new Exception($"There is no project with id: {projectId}");

            var pe = GetProjectEmployee(projectId, model.EmployeeId);
            if (pe is null)
            {
                return null;
            }
            pe.Enable = model.Enable;
            _context.Update(pe);
            _context.SaveChanges();
            return pe;
        }
    }
}
