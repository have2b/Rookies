using AutoMapper;
using EFCore.DTOs;
using EFCore.Models;
using EFCore.Repositories.Interfaces;

namespace EFCore.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public SalaryRepository(
            ApplicationDbContext context,
            IMapper mapper,
            IDepartmentRepository departmentRepository,
            IEmployeeRepository employeeRepository
        )
        {
            _context = context;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }

        public Salary? AddSalary(Guid departmentId, Guid empId, SalaryDTO model)
        {
            _ = _departmentRepository.GetDepartment(departmentId)
            ?? throw new Exception($"There is no department with id: {departmentId}");

            _ = _employeeRepository.GetEmployee(departmentId, empId)
                ?? throw new Exception($"There is no employee with id: {empId}");

            var salary = _mapper.Map<Salary>(model);
            salary.EmployeeId = empId;
            _context.Salaries.Add(salary);
            _context.SaveChanges();

            return salary;
        }

        public bool DeleteSalary(Guid departmentId, Guid empId, Guid id)
        {
            _ = _departmentRepository.GetDepartment(departmentId)
             ?? throw new Exception($"There is no department with id: {departmentId}");

            _ = _employeeRepository.GetEmployee(departmentId, empId)
                ?? throw new Exception($"There is no employee with id: {empId}");

            var salary = _context.Salaries.Where(e => e.Id == id).FirstOrDefault();
            if (salary is null)
            {
                return false;
            }
            _context.Salaries.Remove(salary);
            _context.SaveChanges();
            return true;
        }

        public Salary? GetSalary(Guid departmentId, Guid empId, Guid id)
        {
            _ = _departmentRepository.GetDepartment(departmentId)
            ?? throw new Exception($"There is no department with id: {departmentId}");

            var employee = _employeeRepository.GetEmployee(departmentId, empId)
                ?? throw new Exception($"There is no employee with id: {empId}");

            return employee.Salary;
        }

        public Salary? UpdateSalary(Guid departmentId, Guid empId, Guid id, SalaryDTO model)
        {
            _ = _departmentRepository.GetDepartment(departmentId)
             ?? throw new Exception($"There is no department with id: {departmentId}");

            _ = _employeeRepository.GetEmployee(departmentId, empId)
                ?? throw new Exception($"There is no employee with id: {empId}");

            var salary = _context.Salaries.Where(e => e.Id == id).FirstOrDefault();
            if (salary is null)
            {
                return null;
            }
            _mapper.Map(model, salary);
            _context.SaveChanges();
            return salary;
        }
    }
}
