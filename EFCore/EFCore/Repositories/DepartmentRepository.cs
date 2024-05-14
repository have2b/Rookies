using AutoMapper;
using EFCore.DTOs;
using EFCore.Models;
using EFCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Department AddDepartment(DepartmentDTO model)
        {
            var department = _mapper.Map<Department>(model);
            _context.Departments.Add(department);
            _context.SaveChanges();
            return department;
        }

        public bool DeleteDepartment(Guid id)
        {
            var department = _context.Departments.Where(d => d.Id == id).FirstOrDefault();
            if (department is null)
            {
                return false;
            }
            _context.Departments.Remove(department);
            _context.SaveChanges();
            return true;
        }

        public Department? GetDepartment(Guid id)
        {
            var department = _context
                .Departments.Include(d => d.Employees)
                .Where(d => d.Id == id)
                .FirstOrDefault();
            return department;
        }

        public List<Department> GetDepartments()
        {
            var departments = _context.Departments.Include(d => d.Employees);

            return [.. departments];
        }

        public Department? UpdateDepartment(Guid id, DepartmentDTO model)
        {
            var department = _context.Departments.Where(d => d.Id == id).FirstOrDefault();
            if (department is null)
            {
                return null;
            }
            _mapper.Map(model, department);
            _context.SaveChanges();
            return department;
        }
    }
}
