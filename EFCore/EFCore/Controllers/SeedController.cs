using System.Globalization;
using CsvHelper;
using EFCore.Data.Models;
using EFCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SeedController(ApplicationDbContext context)
        {
            _context = context;
        }

        private List<T> LoadDataFromCsv<T>(string fileName)
            where T : class
        {
            try
            {
                using (var reader = new StreamReader($"./Data/{fileName}.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<T>().ToList();
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from CSV: {ex.Message}");
                throw;
            }
        }

        [HttpPut]
        public IActionResult Seed()
        {
            try
            {
                var departments = SeedDepartments();
                var departmentIds = _context.Departments.Select(d => d.Id).ToList();

                var employees = SeedEmployees(departmentIds);
                var employeeIds = _context.Employees.Select(e => e.Id).ToList();

                var projects = SeedProjects();
                var projectIds = _context.Projects.Select(p => p.Id).ToList();

                var projectEmployees = SeedProjectEmployees(projectIds, employeeIds);
                var salaries = SeedSalaries(employeeIds);

                _context.SaveChanges();

                var result = new
                {
                    DepartmentsAdded = departments,
                    EmployeesAdded = employees,
                    ProjectsAdded = projects,
                    ProjectEmployeesAdded = projectEmployees,
                    SalariesAdded = salaries
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during seeding: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private int SeedDepartments()
        {
            var departments = LoadDataFromCsv<Department>("Department_data");
            var existingDepartmentIds = _context.Departments.Select(d => d.Id).ToHashSet();

            var newDepartments = departments
                .Where(d => !existingDepartmentIds.Contains(d.Id))
                .ToList();
            _context.Departments.AddRange(newDepartments);
            _context.SaveChanges();
            return newDepartments.Count;
        }

        private int SeedEmployees(List<Guid> departmentIds)
        {
            var employeeDatas = LoadDataFromCsv<EmployeeData>("Employee_data");
            var existingEmployeeIds = _context.Employees.Select(e => e.Id).ToHashSet();
            var random = new Random();

            var newEmployees = employeeDatas
                .Where(e => !existingEmployeeIds.Contains(e.Id))
                .Select(employeeData => new Employee
                {
                    Id = employeeData.Id,
                    Name = employeeData.Name,
                    JoinedDate = employeeData.JoinedDate,
                    DepartmentId = departmentIds[random.Next(departmentIds.Count)]
                })
                .ToList();

            _context.Employees.AddRange(newEmployees);
            _context.SaveChanges();
            return newEmployees.Count;
        }

        private int SeedProjects()
        {
            var projects = LoadDataFromCsv<Project>("Project_data");
            var existingProjectIds = _context.Projects.Select(p => p.Id).ToHashSet();

            var newProjects = projects.Where(p => !existingProjectIds.Contains(p.Id)).ToList();
            _context.Projects.AddRange(newProjects);
            _context.SaveChanges();
            return newProjects.Count;
        }

        private int SeedProjectEmployees(List<Guid> projectIds, List<Guid> employeeIds)
        {
            var random = new Random();
            var projectEmployees = new List<ProjectEmployee>();

            for (int i = 0; i < 100; i++)
            {
                projectEmployees.Add(
                    new ProjectEmployee
                    {
                        ProjectId = projectIds[random.Next(projectIds.Count)],
                        EmployeeId = employeeIds[random.Next(employeeIds.Count)],
                        Enable = random.Next(2) == 0
                    }
                );
            }

            var newProjectEmployees = projectEmployees
                .Where(pe =>
                    !_context.ProjectEmployees.Any(e =>
                        e.ProjectId == pe.ProjectId && e.EmployeeId == pe.EmployeeId
                    )
                )
                .ToList();

            _context.ProjectEmployees.AddRange(newProjectEmployees);
            _context.SaveChanges();
            return newProjectEmployees.Count;
        }

        private int SeedSalaries(List<Guid> employeeIds)
        {
            var salaryDatas = LoadDataFromCsv<SalaryData>("Salary_data");
            var existingSalaryIds = _context.Salaries.Select(s => s.Id).ToHashSet();
            var random = new Random();

            var newSalaries = salaryDatas
                .Where(s => !existingSalaryIds.Contains(s.Id))
                .Select(salaryData => new Salary
                {
                    Id = salaryData.Id,
                    Amount = salaryData.Amount,
                    EmployeeId = employeeIds[random.Next(employeeIds.Count)]
                })
                .ToList();

            _context.Salaries.AddRange(newSalaries);
            _context.SaveChanges();
            return newSalaries.Count;
        }
    }
}
