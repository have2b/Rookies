using AutoMapper;
using EFCore.DTOs;
using EFCore.Models;
using EFCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeRepository(ApplicationDbContext context, IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _context = context;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }
        public Employee? AddEmployee(Guid departmentId, EmployeeDTO model)
        {
            _ = _departmentRepository.GetDepartment(departmentId)
            ?? throw new Exception($"There is no department with id: {departmentId}");

            var employee = _mapper.Map<Employee>(model);
            employee.DepartmentId = departmentId;
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public bool DeleteEmployee(Guid departmentId, Guid id)
        {
            _ = _departmentRepository.GetDepartment(departmentId)
            ?? throw new Exception($"There is no department with id: {departmentId}");

            var employee = _context.Employees.Where(e => e.Id == id).FirstOrDefault();
            if (employee is null)
            {
                return false;
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return true;
        }

        public Employee? GetEmployee(Guid departmentId, Guid id)
        {
            _ = _departmentRepository.GetDepartment(departmentId)
            ?? throw new Exception($"There is no department with id: {departmentId}");

            return _context.Employees.Where(e => e.Id == id).FirstOrDefault();
        }

        public List<Employee>? GetEmployees(Guid departmentId)
        {
            var department = _departmentRepository.GetDepartment(departmentId)
            ?? throw new Exception($"There is no department with id: {departmentId}");

            return [.. department.Employees];
        }

        public Employee? UpdateEmployee(Guid departmentId, Guid id, EmployeeDTO model)
        {
            _ = _departmentRepository.GetDepartment(departmentId)
            ?? throw new Exception($"There is no department with id: {departmentId}");

            var employee = _context.Employees.Where(e => e.Id == id).FirstOrDefault();
            if (employee is null)
            {
                return null;
            }
            _mapper.Map(model, employee);
            _context.SaveChanges();
            return employee;
        }

        public IEnumerable<object> GetEmployeesWithDepartmentNames()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var result = _context.Employees
                        .Join(_context.Departments,
                            employee => employee.DepartmentId,
                            department => department.Id,
                            (employee, department) => new
                            {
                                EmployeeId = employee.Id,
                                EmployeeName = employee.Name,
                                DepartmentName = department.Name
                            });

                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public IEnumerable<object> GetEmployeesWithProjects()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var result = _context.Employees
                        .Join(_context.ProjectEmployees.DefaultIfEmpty(),
                            e => e.Id,
                            pe => pe.EmployeeId,
                            (e, pe) => new
                            {
                                EmployeeId = e.Id,
                                EmployeeName = e.Name,
                                ProjectId = pe.ProjectId,
                                ProjectName = pe.Project.Name
                            });

                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public IEnumerable<Employee> GetEmployeesWithSalaryAndJoinedDateFilter()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var result = _context.Employees
                        .Join(_context.Salaries, e => e.Id, s => s.EmployeeId, (e, s) => new { Employee = e, Salary = s })
                        .Where(es => es.Salary.Amount > 100 && es.Employee.JoinedDate >= new DateTime(2024, 1, 1))
                        .Select(es => es.Employee)
                        .Include(e => e.Salary)
                        .AsNoTracking();


                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}