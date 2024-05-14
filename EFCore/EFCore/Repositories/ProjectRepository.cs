using AutoMapper;
using EFCore.DTOs;
using EFCore.Models;
using EFCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProjectRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Project AddProject(ProjectDTO model)
        {
            var project = _mapper.Map<Project>(model);
            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }

        public bool DeleteProject(Guid id)
        {
            var project = _context.Projects.Where(d => d.Id == id).FirstOrDefault();
            if (project is null)
            {
                return false;
            }
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return true;
        }

        public Project? GetProject(Guid id)
        {
            var project = _context
                .Projects
                .Include(p => p.ProjectEmployees)
                .Where(d => d.Id == id)
                .FirstOrDefault();
            return project;
        }

        public List<Project> GetProjects()
        {
            var projects = _context.Projects.Include(p => p.ProjectEmployees);

            return [.. projects];
        }

        public Project? UpdateProject(Guid id, ProjectDTO model)
        {
            var project = _context.Projects.Where(d => d.Id == id).FirstOrDefault();
            if (project is null)
            {
                return null;
            }
            _mapper.Map(model, project);
            _context.SaveChanges();
            return project;
        }
    }
}
