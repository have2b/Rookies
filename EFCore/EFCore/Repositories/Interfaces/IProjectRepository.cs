using EFCore.DTOs;
using EFCore.Models;

namespace EFCore.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        public List<Project> GetProjects();
        public Project? GetProject(Guid id);
        public Project AddProject(ProjectDTO department);
        public Project? UpdateProject(Guid id, ProjectDTO model);
        public bool DeleteProject(Guid id);
    }
}
